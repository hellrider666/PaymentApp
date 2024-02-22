using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentApp.Persistence.Classes.EntityConfiguration.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string DecimalFormat = "DECIMAL(18,2)";

        public static IndexBuilder SetUniqueIndex(this IndexBuilder index, string tableName, string columnName)
        {
            return index.HasDatabaseName(string.Format("{0}_{1}_Unique_Index", tableName, columnName)).IsUnique();
        }

        public static PropertyBuilder<Enum> HasEnumToString<Enum>(this PropertyBuilder<Enum> builder)
        {
            return builder.HasConversion<string>();
        }

        public static PropertyBuilder<decimal> AsDecimal(this PropertyBuilder<decimal> builder)
        {
            return builder.HasColumnType(DecimalFormat);
        }
    }
}
