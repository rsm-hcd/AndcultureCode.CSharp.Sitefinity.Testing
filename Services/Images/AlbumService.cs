using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Images;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Images
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
