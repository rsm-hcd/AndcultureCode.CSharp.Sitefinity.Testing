using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Calendars;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Calendars
{
    public class EventService : ODataService<Event>
    {
        public EventService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/events";
        }

    }
}
