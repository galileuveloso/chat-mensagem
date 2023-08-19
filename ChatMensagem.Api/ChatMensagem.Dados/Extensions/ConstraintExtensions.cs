using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatMensagem.Dados.Extensions
{
    public static class ConstraintExtensions
    {
        private const int MaximumNameSize = 150;

        public static void SetupConstraints(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (tableName is not null)
                    entity.SetTableName(tableName.ToUpper());

                foreach (var key in entity.GetKeys())
                    key.SetName(PrimaryKeyName(entity).ToUpper());

                foreach (var key in entity.GetForeignKeys())
                    key.SetConstraintName(ForeignKeyName(entity, key).ToUpper());

                foreach (var key in entity.GetIndexes())
                    key.SetDatabaseName(IndexName(entity, key).ToUpper());
            }
        }

        private static string PrimaryKeyName(IMutableEntityType entity)
        {
            string tableName = entity.GetTableName();
            string constraintName = $"{tableName}_PK";

            if (constraintName.Length > MaximumNameSize)
                constraintName = $"{tableName?.Substring(0, 27)}_PK";

            return constraintName;
        }

        private static string ForeignKeyName(IMutableEntityType entity, IMutableForeignKey key)
        {
            string tableNameP = key.PrincipalEntityType.GetTableName();
            string tableNameS = entity.GetTableName();
            string fieldName = (key.Properties == null || key.Properties.Count == 0 ? "Id" : key.Properties[0].Name).Trim();
            string constraintName = $"{tableNameP}_{tableNameS}_{fieldName}_FK";

            if (constraintName.Length > MaximumNameSize)
                constraintName = SplitForeignKeyName(tableNameP, tableNameS, fieldName);

            return constraintName;
        }

        private static string IndexName(IMutableEntityType entity, IMutableIndex key)
        {
            string tableName = entity.GetTableName();
            string fieldName = (key.Properties == null || key.Properties.Count == 0 ? "Id" : key.Properties[0].Name).Trim();
            string indexName = $"{tableName}_{fieldName}_IX";

            if (indexName.Length > MaximumNameSize)
                indexName = SplitIndexName(tableName, fieldName);

            return indexName;
        }

        private static string SplitIndexName(string tableName, string fieldName)
        {
            if (fieldName.Length > 12)
            {
                if (fieldName.Substring(fieldName.Length - 2, 2).ToUpper().Equals("ID"))
                    fieldName = fieldName.Substring(0, 10).Trim() + "Id";
                else
                    fieldName = fieldName.Substring(0, 12).Trim();
            }
            int maxTableName = (MaximumNameSize - 4) - fieldName.Length;
            tableName = tableName.PadRight(maxTableName).Substring(0, maxTableName).TrimEnd();
            return $"{tableName}_{fieldName}_IX";
        }

        private static string SplitForeignKeyName(string tableNameP, string tableNameS, string fieldName)
        {
            if (fieldName.Length > 9)
            {
                if (fieldName.Substring(fieldName.Length - 2, 2).ToUpper().Equals("ID"))
                    fieldName = fieldName.Substring(0, 7).Trim() + "Id";
                else
                    fieldName = fieldName.Substring(0, 9).Trim();
            }

            tableNameS = tableNameS.Replace(tableNameP, string.Empty).PadRight(8).Substring(0, 8).TrimEnd();
            tableNameP = tableNameP.PadRight(8).Substring(0, 8).TrimEnd();

            return $"{tableNameP}_{tableNameS}_{fieldName}_FK";
        }
    }
}
