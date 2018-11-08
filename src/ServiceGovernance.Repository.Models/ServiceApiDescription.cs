using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ServiceGovernance.Repository.Models.Converter;
using System;

namespace ServiceGovernance.Repository.Models
{
    /// <summary>
    /// Models an Api description for a service
    /// </summary>
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
    }
}
