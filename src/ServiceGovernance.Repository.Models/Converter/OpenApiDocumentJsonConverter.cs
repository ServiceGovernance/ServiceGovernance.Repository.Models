using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Text;

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

                var openApiReader = new OpenApiStringReader();
                var document = openApiReader.Read(item.ToString(), out var diagnostic);

                if (diagnostic.Errors.Count > 0)
                    throw new JsonReaderException("Error reading OpenApi document. " + string.Join(Environment.NewLine, diagnostic.Errors.Select(e => e.Message)));

                return document;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                return;

            if (!(value is OpenApiDocument document))
                throw new NotSupportedException($"OpenApiDocumentJsonConverter does not support converting the type '{value.GetType()}'!");

            var sb = new StringBuilder();
            using (var w = new StringWriter(sb))
            {
                var apiWriter = new OpenApiJsonWriter(w);
                document.SerializeAsV3(apiWriter);
                apiWriter.Flush();
                w.Flush();
            }

            writer.WriteRawValue(sb.ToString());
        }
    }
}
