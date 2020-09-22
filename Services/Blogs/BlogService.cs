using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Blogs;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Blogs
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
