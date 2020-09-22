using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Blogs;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Blogs
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
