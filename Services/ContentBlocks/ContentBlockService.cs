using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.ContentBlocks;

namespace Andculture.Sitefinity.Testing.Services.ContentBlocks
{
    public class ContentBlockService : ODataService<ContentBlock>
    {
        public ContentBlockService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/contentitems";
        }

    }
}
