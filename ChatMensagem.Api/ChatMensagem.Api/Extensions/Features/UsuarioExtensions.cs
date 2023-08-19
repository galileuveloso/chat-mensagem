using ChatMensagem.Api.Features.UsuarioFeature.Commands;
using ChatMensagem.Domain;
using Microsoft.AspNetCore.SignalR;

namespace ChatMensagem.Api.Extensions.Features
{
    public static class UsuarioExtensions
    {
        public static Usuario ToDomain
        (
            this InserirUsuarioCommand request
        )
        {
            var entity = FeatureExtensions.ToDomain<InserirUsuarioCommand, Usuario>(request);
            return entity;
        }

        public static InserirUsuarioResponse ToInserirUsuarioResponse
        (
            this Usuario usuario
        )
        {
            var response = usuario.Map<InserirUsuarioResponse>();
            return response;
        }

        public static InserirUsuarioCommand ToInserirUsuarioCommand
        (
            this HubCallerContext hubContext
        )
        {
            var command = FeatureExtensions.Convert<HubCallerContext, InserirUsuarioCommand>(hubContext);
            return command;
        }
    }
}
