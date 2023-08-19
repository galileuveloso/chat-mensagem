using ChatMensagem.Api.Features.MensagemFeature.Commands;
using ChatMensagem.Contracts;

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
    }
}
