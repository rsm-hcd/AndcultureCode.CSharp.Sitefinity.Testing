using Newtonsoft.Json;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Calendars
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Calendar : Content
    {
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string Title { get; set; }
    }
}
