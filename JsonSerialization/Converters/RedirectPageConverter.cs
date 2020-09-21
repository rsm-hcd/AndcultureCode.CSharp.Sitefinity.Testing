using Andculture.Sitefinity.Testing.Enumerations;
using Newtonsoft.Json;
using System;

namespace Andculture.Sitefinity.Testing.JsonSerialization.Converters
{
    /// <summary>
    /// Responsible for converting RedirectPage enums values into numeric values
    /// </summary>
    public class RedirectPageConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long convertedValue = ((RedirectPage)value).GetHashCode();

            writer.WriteValue(convertedValue.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }
            return Enum.Parse(typeof(RedirectPage), reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RedirectPage);
        }
    }
}