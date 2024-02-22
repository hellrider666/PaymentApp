using System.Text.Encodings.Web;
using System.Text.Json;

namespace PaymentApp.Commons.Classes.Serialization.Json
{
    public class JSONSerializationHelper
    {
        public JsonSerializerOptions SerializerSettings { get; private set; }


        public JSONSerializationHelper()
        {
            SerializerSettings = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public JSONSerializationHelper(JsonSerializerOptions serializerSettings)
        {
            SerializerSettings = serializerSettings;
        }

        public JSONSerializationHelper(bool useFormatting) : this()
        {
            if (useFormatting)
                SerializerSettings.WriteIndented = useFormatting;
        }


        public string Serialize(object entity)
        {
            return JsonSerializer.Serialize(entity, SerializerSettings);
        }
    }
}
