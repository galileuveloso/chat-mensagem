using ChatMensagem.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatMensagem.Dados.Extensions.Tables
{
    internal static class UsuairoExtensions
    {
        public static void SetupUsuario(this ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Usuario>()
               .Property(item => item.ConnectionId)
               .HasMaxLength(200);
        }
    }
}
