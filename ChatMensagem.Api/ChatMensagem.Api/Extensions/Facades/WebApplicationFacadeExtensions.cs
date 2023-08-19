using ChatMensagem.Api.Extensions.Generics;

namespace ChatMensagem.Api.Extensions.Facades
{
    public static class WebApplicationFacadeExtensions
    {
        public static void UseSetups(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwaggerDefault();
            app.UseControllers();
            app.UseSecurity();
            app.UseRequestLocalizationDefault();
        }

        public static void MapHubs(this WebApplication app)
        {
           //mapear Hubs
        }
    }
}
