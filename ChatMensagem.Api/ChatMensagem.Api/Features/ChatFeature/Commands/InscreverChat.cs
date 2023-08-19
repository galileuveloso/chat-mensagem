using ChatMensagem.Api.Extensions.Features;
using ChatMensagem.Api.Features.ChatFeature.Hubs;
using ChatMensagem.Api.Helpers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatMensagem.Api.Features.ChatFeature.Commands
{
    public class InscreverChatCommand : IRequest<InscreverChatResponse>
    {
        public Guid ChatId { get; set; }
        internal Guid UsuarioId { get; set; }
    }

    public class InscreverChatResponse
    {
        public Guid ChatId { get; set; }
    }

    public class InscreverChatHandler : IRequestHandler<InscreverChatCommand, InscreverChatResponse>
    {
        private readonly IHubContext<ChatHub> _chat;

        public InscreverChatHandler
        (
            IHubContext<ChatHub> chat
        )
        {
            _chat = chat ?? throw new ArgumentNullException(nameof(chat));
        }

        public async Task<InscreverChatResponse> Handle
        (
            InscreverChatCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InscreverChatCommand>());

            //TODO - implementar logica para pegar o Id de Conexao do Usuario
            string connectionId = "Connection do Usuario";

            await InserverChat(request, connectionId, cancellationToken);

            return request.ToInscreverChatResponse();
        }

        private async Task InserverChat
        (
            InscreverChatCommand request,
            string connectionId,
            CancellationToken cancellationToken
        )
        {
            await _chat.Groups.AddToGroupAsync(connectionId, request.ChatId.ToString(), cancellationToken);
        }
    }
}
