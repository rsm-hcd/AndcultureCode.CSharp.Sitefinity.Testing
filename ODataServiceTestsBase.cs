using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content;
using System;
using Xunit.Abstractions;

namespace AndcultureCode.CSharp.Sitefinity.Testing
{
    public class ODataServiceTestsBase<TService, TModel> where TModel : Content, new()
    {
        #region Members

        public static string SF_TEST_PREFIX = "sf_test_";

        public string AccessToken { get { return Fixture.Session.AccessToken; } }
        public ODataSessionFixture Fixture { get; }
        public TModel Model { get; set; }
        public TestingSettings ODataTestSettings { get { return Fixture.Session.ODataTestSettings; } }
        public ITestOutputHelper Output { get; set; }
        public TService Sut { get; set; }

        #endregion Members

        public ODataServiceTestsBase(
            ODataSessionFixture fixture,
            ITestOutputHelper output)
        {
            this.Fixture = fixture;
            this.Output = output;
        }

        #region Public Methods

        public string GetRandomString(string fieldName)
        {
            return $"{SF_TEST_PREFIX}_{fieldName}_{Guid.NewGuid()}";
        }

        public void StartNewSession()
        {
            this.Fixture.Session.Authenticate();
        }

        #endregion Public Methods
    }
}
