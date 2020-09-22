using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Interfaces
{
    public interface IComment
    {
        public DateTimeOffset DateCreated { get; set; }
        public string Key { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
