using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatMensagem.Api.Features.UsuarioFeature
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController
        (
            IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
