using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Images;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;

namespace Andculture.Sitefinity.Testing.Services.Images
{
    public class ImageService : ODataService<Image>
    {
        public ImageService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/images";
        }

        public IRestResponse Upload(Image model)
        {
            string documentTitle = "mobile-marketing";
            string documentExtension = ".jpg";
            string docFilePath = string.Concat(Directory.GetCurrentDirectory(), @"\Media\", documentTitle, documentExtension);
            var jObject = JObject.FromObject(model);
            jObject.Add("DirectUpload", true);

            var url = Settings.BaseUrl + EndpointUrl;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "Bearer " + AccessToken);
            request.AddHeader("X-Sf-Properties", jObject.ToString(Formatting.None));
            request.AddParameter("image/jpeg", File.ReadAllBytes(docFilePath), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
