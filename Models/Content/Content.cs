using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content
{
    public class Content
    {
        public DateTimeOffset? DateCreated { get; set; }
        public Guid? ID { get; set; }
        public DateTimeOffset? LastModified { get; }
        public string Provider { get; set; }
        public DateTimeOffset? PublicationDate { get; set; }
        public string UrlName { get; set; }
    }
}
