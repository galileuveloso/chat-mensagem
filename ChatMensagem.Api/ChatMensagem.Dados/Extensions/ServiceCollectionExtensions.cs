using ChatMensagem.Dados.Repositories;
using ChatMensagem.Domain;
using ChatMensagem.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatMensagem.Dados.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories
        (
            this IServiceCollection services
        )
        {
            services.AddScoped(typeof(IRepository<Usuario>), typeof(Repository<Usuario>));
        }

        public static void AddDbContext
        (
            this IServiceCollection services,
            string connectionString
        )
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ChatMensagemDbContext>(options =>
                    options
                        .EnableSensitiveDataLogging()
                        .UseNpgsql(connectionString)
                );
        }
    }
}
