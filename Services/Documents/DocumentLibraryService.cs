using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Documents;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Documents
{
    public class DocumentLibraryService : ODataService<DocumentLibrary>
    {
        public DocumentLibraryService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/documentlibraries";
        }

    }
}
