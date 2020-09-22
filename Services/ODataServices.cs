using AndcultureCode.CSharp.Sitefinity.Testing.JsonSerialization;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services
{
    public class ODataService<TContent> where TContent : Content
    {
        public string EndpointUrl { get; set; }

        public TestingSettings Settings { get; set; }
        public string AccessToken { get; set; }

        public ODataService(TestingSettings settings, string accessToken)
        {
            AccessToken = accessToken;
            Settings = settings;
        }

        /// <summary>
        /// Create a draft content item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns the response of the request.<see cref="IRestResponse"/></returns>
        public IRestResponse CreateDraft(TContent model)
        {
            var body = JsonConvert.SerializeObject(model);
            var requestUrl = Settings.BaseUrl + EndpointUrl;

            var restResponse = this.ExecuteSitefinityRequest(Method.POST, requestUrl, body);

            return restResponse;
        }

        /// <summary>
        /// Publish an existing content item.
        /// </summary>
        /// <param name="id">The item's ID</param>
        /// <returns></returns>
        public IRestResponse Publish(Guid id)
        {
            var requestUrl = Settings.BaseUrl + EndpointUrl + "(" + id + ")" + "/operation";
            var body = "{\"action\": \"Publish\",\"actionParameters\": {}}";

            return this.ExecuteSitefinityRequest(Method.POST, requestUrl, body);
        }

        /// <summary>
        /// Modify existing item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The response of the request.</returns>
        public IRestResponse Modify(TContent model)
        {
            var requestUrl = Settings.BaseUrl + EndpointUrl + "(" + model.ID + ")";
            var body = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ContractResolver = new ShouldSerializeContractResolver() });
            return this.ExecuteSitefinityRequest(Method.PATCH, requestUrl, body);
        }

        /// <summary>
        /// Delete existing item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns the response of the request.</returns>
        public IRestResponse Delete(Guid id)
        {
            var requestUrl = Settings.BaseUrl + EndpointUrl + "(" + id + ")";

            return this.ExecuteSitefinityRequest(Method.DELETE, requestUrl, null);
        }

        /// <summary>
        /// Gets an existing item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns the response of the request.</returns>
        public IRestResponse GetItem(Guid id)
        {
            var requestUrl = Settings.BaseUrl + EndpointUrl + "(" + id + ")";

            return this.ExecuteSitefinityRequest(Method.GET, requestUrl, null);
        }

        /// <summary>
        /// Performs a get request against the root endpoint of an item.
        /// </summary>
        /// <example>
        /// Get Metadata
        /// </example>
        /// <param name="item">The item.</param>
        /// <returns>The response of the request.</returns>
        public IRestResponse Get()
        {
            var requestUrl = Settings.BaseUrl + EndpointUrl;

            return this.ExecuteSitefinityRequest(Method.GET, requestUrl, null);
        }

        /// <summary>
        /// Executes rest request with the default headers required for Sitefinity.
        /// </summary>
        /// <param name="method">The Method of the request.</param>
        /// <param name="url">The url.</param>
        /// <param name="body">The body.</param>
        /// <returns>The response of the executed request.</returns>
        private IRestResponse ExecuteSitefinityRequest(Method method, string url, object body)
        {
            var client = new RestClient(url);

            var request = new RestRequest(method);

            request.AddHeader("authorization", "Bearer " + AccessToken);
            request.AddHeader("x-sf-service-request", "true");
            request.AddHeader("content-type", "application/json");

            if (body != null)
            {
                request.AddParameter("application/json", body.ToString(), ParameterType.RequestBody);
            }

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
