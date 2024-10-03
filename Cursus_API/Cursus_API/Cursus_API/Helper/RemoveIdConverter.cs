using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Cursus_API.Helper
{
    public class RemoveIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // This converter will be used for all types
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer), "JsonSerializer cannot be null.");
            }

            try
            {
                // Ensure 'value' is of a type that can be converted
                if (value is JObject)
                {
                    ((JObject)value).WriteTo(writer);
                }
                else
                {
                    // Create JObject from value using the provided serializer
                    var jObject = JObject.FromObject(value, serializer);

                    // Recursively remove $id properties
                    RemoveIdProperties(jObject);

                    jObject.WriteTo(writer);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new JsonSerializationException("Error serializing object", ex);
            }
        }

        private void RemoveIdProperties(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                var jObject = (JObject)token;
                foreach (var property in jObject.Properties().ToList())
                {
                    if (property.Name == "$id")
                    {
                        property.Remove();
                    }
                    else
                    {
                        RemoveIdProperties(property.Value);
                    }
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                foreach (var item in token.Children().ToList())
                {
                    RemoveIdProperties(item);
                }
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Implement ReadJson if you need to handle deserialization
            throw new NotImplementedException("ReadJson is not implemented.");
        }
    }
}
