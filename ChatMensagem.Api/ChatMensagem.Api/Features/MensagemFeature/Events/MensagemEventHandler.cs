using ChatMensagem.Api.Extensions.Features;
using ChatMensagem.Contracts;
using MassTransit;
using MediatR;

namespace ChatMensagem.Api.Features.MensagemFeature.Events
{
    public class MensagemEventHandler : IConsumer<MensagemContract>
    {
        private readonly IMediator _mediator;

        public MensagemEventHandler
        (
            IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Consume
        (
            ConsumeContext<MensagemContract> context
        )
        {
            await _mediator.Send(context.ToNotificarMensagemCommand());
        }
    }
}
