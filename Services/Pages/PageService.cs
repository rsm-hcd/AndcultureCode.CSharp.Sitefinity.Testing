using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Pages;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Pages
{
    public class PageService : ODataService<Page>
    {
        public PageService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system-live/pages";
        }

    }
}
