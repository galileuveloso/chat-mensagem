using ChatMensagem.Api.Features.MensagemFeature.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        //TODO - Remover
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InserirMensagemCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}
