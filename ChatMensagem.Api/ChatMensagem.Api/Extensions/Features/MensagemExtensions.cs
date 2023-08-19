using ChatMensagem.Api.Features.MensagemFeature.Commands;
using ChatMensagem.Contracts;
using MassTransit;

namespace ChatMensagem.Api.Extensions.Features
{
    public static class MensagemExtensions
    {
        public static MensagemContract ToMensagemContract
        (
            this InserirMensagemCommand request
        )
        {
            var contract = FeatureExtensions.ToContract<InserirMensagemCommand, MensagemContract>(request);
            return contract;
        }

        public static NotificarMensagemCommand ToNotificarMensagemCommand
        (
            this ConsumeContext<MensagemContract> context
        )
        {
            if (context is null || context.Message is null)
                return default;

            var request = context.Message.Map<NotificarMensagemCommand>();
            return request;
        }
    }
}
