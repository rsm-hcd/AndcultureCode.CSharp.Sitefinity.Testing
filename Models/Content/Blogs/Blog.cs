using Newtonsoft.Json;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Blogs
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Blog : Content
    {
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
