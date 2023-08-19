using ChatMensagem.Api.Settings;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace ChatMensagem.Api.Extensions.Generics
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMemoryCacheService(this IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        public static void AddMassTransit(this IServiceCollection services, ConfigurationManager configuration)
        {
            var settings = configuration.GetSection(MassTransitSettings.ConfigurationSection).Get<MassTransitSettings>();
            services.AddMassTransit(masstransit =>
            {
                masstransit.AddConsumers(typeof(Program).Assembly);
                masstransit.UsingRabbitMq((context, rabbit) =>
                {
                    rabbit.MessageTopology.SetEntityNameFormatter(new EntityNameFormatter(rabbit.MessageTopology.EntityNameFormatter, settings.Prefix));
                    rabbit.Host(settings.Server, settings.VirtualHost, host =>
                    {
                        host.Username(settings.User);
                        host.Password(settings.Password);
                    });
                    rabbit.ConfigureEndpoints(context, settings.GetKebabCaseEndpointNameFormatter());
                });
            });
        }

        public static void AddSwagger(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddSwaggerGen(options =>
                {
                    options.MapType<FileResult>(() => new OpenApiSchema { Type = "file" });
                    options.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = configuration.GetValue<string>("swagger:title"),
                            Version = configuration.GetValue<string>("swagger:version")
                        });
                    options.AddSecurityDefinition("Bearer",
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Por favor insira o token JWT: Bearer <jwtToken>",
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey
                        });
                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                Array.Empty<string>()
                            }
                        });
                });
        }

        public static void AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    GetTokenValidationParameters(configuration, options);
                    options.Events = GetJwtBearerEvents();
                });
        }

        private static JwtBearerEvents GetJwtBearerEvents()
        {
            return new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hub")))
                        context.Token = accessToken;

                    return Task.CompletedTask;
                }
            };
        }

        public static void AddControllersDefault(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status500InternalServerError));
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
            })
            .AddNewtonsoftJson(options =>
            {
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false
                    }
                };
                options.SerializerSettings.ContractResolver = contractResolver;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
        }

        public static void AddMediatRDefault(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
        }

        public static void AddCorsDefault(this IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options
                        .AddDefaultPolicy(builder =>
                        {
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
        }

        private static void GetTokenValidationParameters(ConfigurationManager configuration, JwtBearerOptions options)
        {
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidIssuer = configuration["authentication:issuer"],
                    ValidAudience = configuration["authentication:audience"],
                    IssuerSigningKey = GetSymmetricSecurityKey(configuration),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey(ConfigurationManager configuration)
        {
            return new SymmetricSecurityKey(GetSecret(configuration));
        }

        private static byte[] GetSecret(ConfigurationManager configuration)
        {
            return Encoding.UTF8.GetBytes(configuration.GetValue<string>("authentication:secret"));
        }
    }
}
