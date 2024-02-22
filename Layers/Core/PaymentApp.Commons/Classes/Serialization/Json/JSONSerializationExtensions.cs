namespace PaymentApp.Commons.Classes.Serialization.Json
{
    public  static class JSONSerializationExtensions
    {
        public static string SerializeToJSON<T>(this T source)
        {
            return new JSONSerializationHelper().Serialize(source);
        }

        public static string SerializeToJSON<T>(this T source, bool useFormating)
        {
            return new JSONSerializationHelper(useFormating).Serialize(source);
        }       
    }
}
