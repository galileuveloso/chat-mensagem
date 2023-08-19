using ChatMensagem.Api.Extensions.Generics;

namespace ChatMensagem.Api.Extensions.Facades
{
    public static class WebApplicationBuilderFacadeExtensions
    {
        public static void AddSetups(this WebApplicationBuilder builder)
        {
            builder.AddSecurity();
            builder.AddControllers();
            builder.AddSwagger();
            builder.AddMediatR();
            builder.AddMassTransit();
            builder.AddMemoryCacheService();
            builder.AddSignalR();
            builder.AddDatabase();
        }
    }
}
