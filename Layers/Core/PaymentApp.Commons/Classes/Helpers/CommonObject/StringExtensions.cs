namespace PaymentApp.Commons.Classes.Helpers.CommonObject
{
    public static class StringExtensions
    {
        public static bool IsDigit(this string value)
        {
            return value.All(char.IsDigit) && value.All(x => x != '.' && x != ',');
        }
    }
}
