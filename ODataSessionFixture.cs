using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing
{
    public class ODataSessionFixture : IDisposable
    {
        public ODataSession Session { get; set; }

        public ODataSessionFixture()
        {
            Session = new ODataSession();
            Session.Authenticate();
        }

        public void Dispose()
        {
        }
    }
}
