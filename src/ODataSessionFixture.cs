using AndcultureCode.CSharp.Sitefinity.Core.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Core.Services;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing
{
    public class ODataSessionFixture : IDisposable
    {
        public ODataSession Session { get; set; }

        public ODataSessionFixture()
        {
            var oDataConnectionSettings = new ODataConnectionSettings();
            Session = new ODataSession(oDataConnectionSettings);
            Session.Authenticate();
        }

        public void Dispose()
        {
        }
    }
}
