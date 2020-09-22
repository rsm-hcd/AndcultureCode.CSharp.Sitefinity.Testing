using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Documents
{
    public class DocumentService : ODataService<Document>
    {
        public DocumentService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/documents";
        }

        public IRestResponse Upload(Document model)
        {
            string documentTitle = "digital_marketing";
            string documentExtension = ".pdf";
            string docFullName = string.Concat(documentTitle, documentExtension);
            string docFilePath = string.Concat(Directory.GetCurrentDirectory(), @"\Media\", documentTitle, documentExtension);
            var jObject = JObject.FromObject(model);
            jObject.Add("DirectUpload", true);

            var url = Settings.BaseUrl + EndpointUrl;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "Bearer " + AccessToken);
            request.AddHeader("Content-Type", "application/pdf");
            request.AddHeader("X-File-Name", docFullName);
            request.AddHeader("X-Sf-Properties", jObject.ToString(Formatting.None));
            request.AddFile("test", docFilePath);

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
