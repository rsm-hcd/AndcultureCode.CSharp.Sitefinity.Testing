using Andculture.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Core.Utilities.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;

namespace Andculture.Sitefinity.Testing
{
    public class ODataSession
    {
        #region Member Variables

        private string EnvironmentName = "Testing";

        private static IConfigurationRoot _cachedConfiguration;

        protected IConfigurationRoot Configuration
        {
            get
            {
                if (_cachedConfiguration != null)
                {
                    return _cachedConfiguration;
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

                _cachedConfiguration = builder.Build();
                ConfigurationUtils.SetConfiguration(_cachedConfiguration);

                return _cachedConfiguration;
            }
        }

        public TestingSettings ODataTestSettings
        {
            get { return Configuration.GetSection("SitefinityODataTestSettings").Get<TestingSettings>(); }
        }

        public string AccessToken { get; set; }

        private string AuthenticateUrl
        {
            get { return ODataTestSettings.BaseUrl + "/Sitefinity/Authenticate/OpenID/connect/token"; }
        }

        #endregion Member Variables

        #region Public Methods

        public void Authenticate()
        {
            var settings = ODataTestSettings;
            var client = new RestClient(AuthenticateUrl);
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            string authHeaderContent = string.Format("username={0}&password={1}&grant_type={2}&scope={3}&client_id={4}&client_secret={5}",
                settings.Username, settings.Password, settings.GrantType, settings.Scope, settings.ClientID, settings.ClientSecret);

            // Make sure you have add this client to the authentication config.
            request.AddParameter("auth", authHeaderContent, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if ((int)response.StatusCode == 200)
            {
                var results = JsonConvert.DeserializeObject<dynamic>(response.Content);
                AccessToken = results.access_token;
            }
            else
            {
                var settingsAsString = JsonConvert.SerializeObject(settings);
                var content = $"response.Content: { response.Content}";
                var errorMessage = $"response.ErrorMessage: { response.ErrorMessage}";
                throw new Exception($"Unable to authenticate to URL: '{AuthenticateUrl}' using settings '{settingsAsString}' --- {content} --- {errorMessage} ");
            }
        }

        #endregion Public Methods
    }
}
