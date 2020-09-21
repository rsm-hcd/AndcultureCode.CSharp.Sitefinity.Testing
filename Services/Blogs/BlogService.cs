using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Blogs;

namespace Andculture.Sitefinity.Testing.Services.Blogs
{
    public class BlogService : ODataService<Blog>
    {
        public BlogService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/blogs";
        }

    }
}
