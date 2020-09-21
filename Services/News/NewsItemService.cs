using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.News;

namespace Andculture.Sitefinity.Testing.Services.News
{
    public class NewsItemService : ODataService<NewsItem>
    {
        public NewsItemService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/newsitems";
        }

    }
}
