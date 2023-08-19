using ChatMensagem.Api.Features.ChatFeature.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatMensagem.Api.Features.ChatFeature
{
    [Route("chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController
        (
            IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //TODO - Remover
        [AllowAnonymous]
        [HttpPost("inscrever")]
        public async Task<IActionResult> Post([FromBody] InscreverChatCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}
