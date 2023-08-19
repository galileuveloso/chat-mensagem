using Microsoft.Extensions.DependencyInjection;

namespace ChatMensagem.Dados.Extensions.Facades
{
    public static class ServiceCollectionExtensionsFacade
    {
        public static void AddPostgresDatabase
        (
            this IServiceCollection services,
            string connectionString
        )
        {
            services.AddDbContext(connectionString);
            services.AddRepositories();
        }
    }
}
