using ChatMensagem.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ChatMensagem.Dados
{
    public interface IChatMensagemDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbConnection Connection { get; }
    }
}
