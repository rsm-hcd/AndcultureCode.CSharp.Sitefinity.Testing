using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.ContentBlocks;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.ContentBlocks
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
