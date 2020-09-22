using Andculture.Sitefinity.Testing.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Andculture.Sitefinity.Testing.Models.Content.Blogs
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BlogPost : Content
    {
        public bool AllowComments { get; set; }
        public List<Guid> Category { get; set; }
        public List<IComment> Comments { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public Guid? ParentId { get; set; }
        public List<Guid> Tags { get; set; }
        public string Title { get; set; }
    }
}