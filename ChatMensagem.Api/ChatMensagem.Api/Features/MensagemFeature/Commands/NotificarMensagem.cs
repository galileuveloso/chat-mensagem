using ChatMensagem.Api.Features.ChatFeature.Hubs;
using ChatMensagem.Api.Helpers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatMensagem.Api.Features.MensagemFeature.Commands
{
    public class NotificarMensagemCommand : IRequest<NotificarMensagemResponse>
    {
        public string Mensagem { get; set; }
        public string Usuario { get; set; }
        public Guid ChatId { get; set; }
    }

    public class NotificarMensagemResponse { }

    public class NotificarMensagemHandler : IRequestHandler<NotificarMensagemCommand, NotificarMensagemResponse>
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificarMensagemHandler
        (
            IHubContext<ChatHub> hubContext
        )
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task<NotificarMensagemResponse> Handle
        (
            NotificarMensagemCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<NotificarMensagemCommand>());
            
            //TODO - colocar o identificar do hub num lugar centralizado
            await _hubContext.Clients.Group(request.ChatId.ToString()).SendAsync("hub/chat", request, request);

            return new();
        }
    }
}
