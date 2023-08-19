using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatMensagem.Api.Features.MensagemFeature
{
    [Route("mensagem")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MensagemController
        (
            IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
