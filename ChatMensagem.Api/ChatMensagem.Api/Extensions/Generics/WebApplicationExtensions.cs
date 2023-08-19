using ChatMensagem.Dados.Extensions.Facades;

namespace ChatMensagem.Api.Extensions.Generics
{
    public static class WebApplicationExtensions
    {
        public static void UseRequestLocalizationDefault(this WebApplication app)
        {
            app
                .UseRequestLocalization(options =>
                {
                    options
                        .AddSupportedCultures("pt-BR")
                        .AddSupportedUICultures("pt-BR")
                        .SetDefaultCulture("pt-BR");
                });
        }

        public static void UseSecurity(this WebApplication app)
        {
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static void UseControllers(this WebApplication app)
        {
            app.UseRouting();
            app
                .MapControllers()
                .RequireAuthorization();
            app.UseHealthChecks("/health");
        }

        public static void UseSwaggerDefault(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
