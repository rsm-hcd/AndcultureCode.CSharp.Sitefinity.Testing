using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Documents;

namespace Andculture.Sitefinity.Testing.Services.Documents
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
