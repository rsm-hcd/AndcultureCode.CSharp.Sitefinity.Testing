using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Images;

namespace Andculture.Sitefinity.Testing.Services.Images
{
    public class AlbumService : ODataService<Album>
    {
        public AlbumService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/albums";
        }

    }
}
