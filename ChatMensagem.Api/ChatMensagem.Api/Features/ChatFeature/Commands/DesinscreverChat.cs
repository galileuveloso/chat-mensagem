using ChatMensagem.Api.Extensions.Features;
using ChatMensagem.Api.Features.ChatFeature.Hubs;
using ChatMensagem.Api.Helpers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatMensagem.Api.Features.ChatFeature.Commands
{
    public class DesinscreverChatCommand : IRequest<DesinscreverChatResponse>
    {
        public Guid ChatId { get; set; }
        internal Guid UsuarioId { get; set; }
    }

    public class DesinscreverChatResponse
    {
        public Guid ChatId { get; set; }
    }

    public class DesinscreverChatHandler : IRequestHandler<DesinscreverChatCommand, DesinscreverChatResponse>
    {
        private readonly IHubContext<ChatHub> _chat;

        public DesinscreverChatHandler
        (
            IHubContext<ChatHub> chat
        )
        {
            _chat = chat ?? throw new ArgumentNullException(nameof(chat));
        }

        public async Task<DesinscreverChatResponse> Handle
        (
            DesinscreverChatCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<DesinscreverChatCommand>());

            //TODO - implementar logica para pegar o Id de Conexao do Usuario
            string connectionId = "Connection do Usuario";

            await DesinscreverChat(request, connectionId, cancellationToken);

            return request.ToDesinscreverChatResponse();
        }

        private async Task DesinscreverChat
        (
            DesinscreverChatCommand request,
            string connectionId,
            CancellationToken cancellationToken
        )
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, request.ChatId.ToString(), cancellationToken);
        }
    }
}
