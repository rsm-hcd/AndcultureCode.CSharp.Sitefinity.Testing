using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.News;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.News
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
