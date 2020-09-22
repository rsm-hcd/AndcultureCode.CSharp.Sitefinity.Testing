using AndcultureCode.CSharp.Sitefinity.Testing.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.News
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class NewsItem : Content
    {
        public bool AllowComments { get; set; }
        public string Author { get; set; }
        public List<Guid> Category { get; set; }
        public List<IComment> Comments { get; set; }
        public string Description { get; set; }
        public List<Guid> Tags { get; set; }
        public string SourceName { get; set; }
        public string SourceSite { get; set; }
        public string Title { get; set; }
    }
}
