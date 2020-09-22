using Newtonsoft.Json.Linq;
using RestSharp;
using Shouldly;
using System.Net;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Extensions
{
    public static class IRestResponseExtensions
    {
        public static void ShouldEqualPublishedContentResponse(this IRestResponse restResponse)
        {
            restResponse.ShouldEqualPublishedResponse("Content");
        }

        public static void ShouldEqualPublishedResponse(this IRestResponse restResponse, string itemName)
        {
            restResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            JObject content = JObject.Parse(restResponse.Content);
            var responseMessage = content.GetValue("Message");
            responseMessage.ShouldBe($"{itemName} has been published successfully.", $"Response content is: {restResponse.Content}");
        }
    }
}
