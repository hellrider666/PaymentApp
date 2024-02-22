using PaymentApp.Application.Classes.DTOs.Enums;
using PaymentApp.Domain.Entities.Enums;

namespace PaymentApp.Application.Classes.Mapping.EnumMapping
{
    public static class EnumMappingHelper
    {
        public static TransactionTypeEnum ToDomainTransactionType(this TransactionTypeEnumDTO source)
        {
            switch (source)
            {
                case TransactionTypeEnumDTO.Replenishment:
                    return TransactionTypeEnum.Replenishment;
                case TransactionTypeEnumDTO.Withdrawal:
                    return TransactionTypeEnum.Withdrawal;
                default:
                    throw new ArgumentException($"Не найдено значение {nameof(source)}");
            }
        }

        public static TransactionTypeEnumDTO ToDTOTransactionType(this TransactionTypeEnum source) 
        {
            switch (source)
            {
                case TransactionTypeEnum.Replenishment:
                    return TransactionTypeEnumDTO.Replenishment;
                case TransactionTypeEnum.Withdrawal:
                    return TransactionTypeEnumDTO.Withdrawal;
                default:
                    throw new ArgumentException($"Не найдено значение {nameof(source)}");
            }
        }
    }
}
