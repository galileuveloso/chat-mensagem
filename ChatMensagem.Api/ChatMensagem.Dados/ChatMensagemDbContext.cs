using ChatMensagem.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ChatMensagem.Dados
{
    public class ChatMensagemDbContext : DbContext, IChatMensagemDbContext
    {
        public ChatMensagemDbContext(DbContextOptions<ChatMensagemDbContext> options) : base(options) { }

        public DbSet<Usuario> Favorito { get; set; }
        public DbConnection Connection => base.Database.GetDbConnection();
    }
}
