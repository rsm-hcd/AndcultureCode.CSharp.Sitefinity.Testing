using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Blogs;

namespace Andculture.Sitefinity.Testing.Services.Blogs
{
    public class BlogPostService : ODataService<BlogPost>
    {
        public BlogPostService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/blogposts";
        }

    }
}
