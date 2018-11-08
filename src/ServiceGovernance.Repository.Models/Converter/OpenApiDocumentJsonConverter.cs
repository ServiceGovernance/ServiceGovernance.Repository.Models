using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ServiceGovernance.Repository.Models.Converter
{
    /// <summary>
    /// Json converter to read and write OpenApiDocuments
    /// </summary>
    public class OpenApiDocumentJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(OpenApiDocument);
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);

                return OpenApiDocumentHelper.ReadFromJson(item.ToString());
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                return;

            if (!(value is OpenApiDocument document))
                throw new NotSupportedException($"OpenApiDocumentJsonConverter does not support converting the type '{value.GetType()}'!");

            writer.WriteRawValue(document.ToJson());
        }
    }
}
