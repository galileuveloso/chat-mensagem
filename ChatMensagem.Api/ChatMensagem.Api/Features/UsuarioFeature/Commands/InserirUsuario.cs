using ChatMensagem.Api.Extensions.Features;
using ChatMensagem.Api.Helpers;
using ChatMensagem.Domain;
using ChatMensagem.Repositories;
using MediatR;

namespace ChatMensagem.Api.Features.UsuarioFeature.Commands
{
    public class InserirUsuarioCommand : IRequest<InserirUsuarioResponse>
    {
        public Guid ConnectionId { get; set; }
    }

    public class InserirUsuarioResponse
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirUsuarioHandler : IRequestHandler<InserirUsuarioCommand, InserirUsuarioResponse>
    {
        private readonly IRepository<Usuario> _repository;

        public InserirUsuarioHandler
        (
            IRepository<Usuario> repository
        )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<InserirUsuarioResponse> Handle
        (
            InserirUsuarioCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirUsuarioCommand>());

            Usuario usuario = await AddAsync(request, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);

            return usuario.ToInserirUsuarioResponse();
        }

        private async Task<Usuario> AddAsync
        (
            InserirUsuarioCommand request,
            CancellationToken cancellationToken
        )
        {
            Usuario entity = request.ToDomain();
            await _repository.AddAsync(entity, cancellationToken);
            return entity;
        }
    }
}
