using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Pages;

namespace Andculture.Sitefinity.Testing.Services.Pages
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
