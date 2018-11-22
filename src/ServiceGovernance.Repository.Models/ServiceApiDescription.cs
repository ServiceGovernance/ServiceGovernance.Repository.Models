using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ServiceGovernance.Repository.Models.Converter;
using System.Collections.Generic;
using System.Diagnostics;

namespace ServiceGovernance.Repository.Models
{
    /// <summary>
    /// Models an Api description for a service
    /// </summary>
    [DebuggerDisplay("{ServiceId}")]
    public class ServiceApiDescription
    {
        /// <summary>
        /// Gets or sets a unique service identifier
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the Api description as OpenApi document
        /// </summary>
        public OpenApiDocument ApiDocument { get; set; }

        /// <summary>
        /// Reads an api description from json
        /// </summary>
        /// <param name="json">the json string</param>
        /// <returns></returns>
        public static ServiceApiDescription ReadFromJson(string json)
        {
            return JsonConvert.DeserializeObject<ServiceApiDescription>(json, new OpenApiDocumentJsonConverter());
        }

        /// <summary>
        /// Reads a list of api descriptions from json
        /// </summary>
        /// <param name="json">the json string</param>
        /// <returns></returns>
        public static List<ServiceApiDescription> ReadListFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<ServiceApiDescription>>(json, new OpenApiDocumentJsonConverter());
        }
    }
}
