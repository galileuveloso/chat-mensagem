namespace ChatMensagem.Api.Extensions.Generics
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(builder.Configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddCorsDefault();
        }

        public static void AddControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHealthChecks();
            builder.Services.AddControllersDefault();
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwagger(builder.Configuration);
        }

        public static void AddMediatR(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatRDefault();
        }

        public static void AddMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(builder.Configuration);
        }

        public static void AddMemoryCacheService(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCacheService();
        }

        public static void AddSignalR(this WebApplicationBuilder builder)
        {
            builder.Services.AddSignalR();
        }
    }
}
