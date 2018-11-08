using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ServiceGovernance.Repository.Models
{
    /// <summary>
    /// Little helper methods for <see cref="OpenApiDocument"/>
    /// </summary>
    public static class OpenApiDocumentHelper
    {
        /// <summary>
        /// Deserializes the json to a<see cref="OpenApiDocument"/>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static OpenApiDocument ReadFromJson(string json)
        {
            var openApiReader = new OpenApiStringReader();
            var document = openApiReader.Read(json, out var diagnostic);

            if (diagnostic.Errors.Count > 0)
                throw new JsonReaderException("Error reading OpenApi document. " + string.Join(Environment.NewLine, diagnostic.Errors.Select(e => e.Message)));

            return document;
        }

        /// <summary>
        /// Serializes the Api document as json (v3)
        /// </summary>
        /// <param name="document">The document to be serialized as json</param>
        /// <returns></returns>
        public static string ToJson(this OpenApiDocument document)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                var apiWriter = new OpenApiJsonWriter(writer);
                document.SerializeAsV3(apiWriter);
                apiWriter.Flush();
                writer.Flush();
            }

            return sb.ToString();
        }
    }
}
