namespace PaymentApp.Commons.Classes.Helpers.CommonObject
{
    public static class DecimalExtensions
    {
        public const int DEFAULT_ROUND_DECIMALS = 2;
        public const MidpointRounding DEFAULT_ROUND_MODE = MidpointRounding.AwayFromZero;

        public static decimal ToRounded(this decimal source)
        {
            return source.ToRounded(DEFAULT_ROUND_DECIMALS, DEFAULT_ROUND_MODE);
        }

        public static decimal ToRounded(this decimal source, int decimals)
        {
            return source.ToRounded(decimals, DEFAULT_ROUND_MODE);
        }

        public static decimal ToRounded(this decimal source, int decimals, MidpointRounding mode)
        {
            return Decimal.Round(source, decimals, mode);
        }
    }
}
