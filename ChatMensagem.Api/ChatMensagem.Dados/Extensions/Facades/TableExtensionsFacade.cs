using ChatMensagem.Dados.Extensions.Tables;
using Microsoft.EntityFrameworkCore;

namespace ChatMensagem.Dados.Extensions.Facades
{
    internal static class TableExtensionsFacade
    {
        internal static void SetupTables(this ModelBuilder modelBuilder)
        {
            modelBuilder.SetupUsuario();
        }
    }
}
