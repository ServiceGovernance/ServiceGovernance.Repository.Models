using FluentAssertions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Newtonsoft.Json;
using NUnit.Framework;
using ServiceGovernance.Repository.Models.Converter;
using ServiceGovernance.Repository.Models.Tests.Builder;

namespace ServiceGovernance.Repository.Models.Tests
{
    [TestFixture]
    public class OpenApiDocumentJsonConverterTests
    {
        [Test]
        public void Can_Read_OpenApiDocument_From_Json()
        {
            var json = new ApiDocumentBuilder().BuildAsString();

            var document = JsonConvert.DeserializeObject<OpenApiDocument>(json, new OpenApiDocumentJsonConverter());
            document.Should().NotBeNull();
            document.Info.Should().NotBeNull();
            document.Info.Title.Should().Be("My Api");
        }

        [Test]
        public void Can_Write_OpenApiDocument_To_Json()
        {
            var document = new ApiDocumentBuilder().Build();
            var json = JsonConvert.SerializeObject(document, new OpenApiDocumentJsonConverter());

            var reader = new OpenApiStringReader();
            var newDocument = reader.Read(json, out var diagnostic);
            diagnostic.Errors.Should().BeEmpty();

            newDocument.Should().NotBeNull();
            newDocument.Info.Should().NotBeNull();
            newDocument.Info.Title.Should().Be("My Api");
        }

        [Test]
        public void Can_Read_ServiceApiDescription_From_Json()
        {
            var json = new ApiDocumentBuilder().BuildAsString();

            json = $"{{\"serviceId\": \"Api1\", \"apiDocument\": {json} }}";

            var apiDescription = ServiceApiDescription.ReadFromJson(json);
            apiDescription.Should().NotBeNull();
            apiDescription.ServiceId.Should().Be("Api1");
            apiDescription.ApiDocument.Should().NotBeNull();
            apiDescription.ApiDocument.Info.Should().NotBeNull();
            apiDescription.ApiDocument.Info.Title.Should().Be("My Api");
        }

        [Test]
        public void Can_Write_ServiceApiDescription_To_Json()
        {
            var apiDescription = new ApiDescriptionBuilder().Build();
            var json = JsonConvert.SerializeObject(apiDescription, new OpenApiDocumentJsonConverter());

            var newApiDescription = ServiceApiDescription.ReadFromJson(json);
            newApiDescription.Should().NotBeNull();
            newApiDescription.ServiceId.Should().Be("Api1");
            newApiDescription.ApiDocument.Should().NotBeNull();
            newApiDescription.ApiDocument.Info.Should().NotBeNull();
            newApiDescription.ApiDocument.Info.Title.Should().Be("My Api");
        }
    }
}
