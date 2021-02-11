using Newtonsoft.Json.Linq;
using RestSharp;
using Shouldly;
using System.Net;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Extensions
{
    public static class IRestResponseExtensions
    {
        /// <summary>
        /// Checks if the response returned from Sitefinity is the standard
        /// "Please wait a moment" response, indicating that the site is still 
        /// starting up.
        /// </summary>
        /// <param name="restResponse"></param>
        /// <returns>True if the response indicates Sitefinity is still starting up.</returns>
        public static bool IsWaitAMomentResponse(this IRestResponse restResponse)
        {
            return restResponse.Content.Contains(RestResponseContentData.PLEASE_WAIT_A_MOMENT);
        }

        public static void ShouldEqualPublishedContentResponse(this IRestResponse restResponse)
        {
            restResponse.ShouldEqualPublishedResponse("Content");
        }

        /// <summary>
        /// Asserts that the response recieved from Sitefinity indicates a
        /// successful publish of an item.
        /// </summary>
        /// <param name="restResponse"></param>
        /// <param name="itemName"></param>
        public static void ShouldEqualPublishedResponse(this IRestResponse restResponse, string itemName)
        {
            restResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            JObject content = JObject.Parse(restResponse.Content);
            var responseMessage = content.GetValue("Message");
            responseMessage.ShouldBe($"{itemName} has been published successfully.", $"Response content is: {restResponse.Content}");
        }
    }
}
