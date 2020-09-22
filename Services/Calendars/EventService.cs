using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Calendars;

namespace Andculture.Sitefinity.Testing.Services.Calendars
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
