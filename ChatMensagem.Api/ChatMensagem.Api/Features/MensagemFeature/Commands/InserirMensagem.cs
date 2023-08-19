using ChatMensagem.Api.Extensions.Features;
using ChatMensagem.Api.Helpers;
using ChatMensagem.Contracts;
using MassTransit;
using MediatR;

namespace ChatMensagem.Api.Features.MensagemFeature.Commands
{
    public class InserirMensagemCommand : IRequest<InserirMensagemResponse>
    {
        public string Mensagem { get; set; }
        public string Usuario { get; set; }
        public Guid ChatId { get; set; }
    }

    public class InserirMensagemResponse
    {
        public Guid TraceId { get; set; }
    }

    public class InserirMensagemHandler : IRequestHandler<InserirMensagemCommand, InserirMensagemResponse>
    {
        private readonly IPublishEndpoint _publisher;

        public InserirMensagemHandler
        (
            IPublishEndpoint publisher
        )
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<InserirMensagemResponse> Handle
        (
            InserirMensagemCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirMensagemCommand>());

            var response = await PublicarMensagem(request, cancellationToken);

            return response.Map<InserirMensagemResponse>();
        }

        private async Task<MensagemContract> PublicarMensagem
        (
            InserirMensagemCommand request,
            CancellationToken cancellationToken
        )
        {
            var contract = request.ToMensagemContract();
            await _publisher.Publish(contract, cancellationToken);
            return contract;
        }
    }
}
