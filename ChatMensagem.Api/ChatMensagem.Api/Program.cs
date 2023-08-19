using ChatMensagem.Api.Extensions.Facades;

var builder = WebApplication.CreateBuilder(args);

builder.AddSetups();

var app = builder.Build();

app.UseSetups();
app.MapHubs();
app.UseWebSockets();

app.Run();
