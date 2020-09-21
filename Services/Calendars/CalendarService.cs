using Andculture.Sitefinity.Testing.Models.Configuration;
using Andculture.Sitefinity.Testing.Models.Content.Calendars;

namespace Andculture.Sitefinity.Testing.Services.Calendars
{
    public class CalendarService : ODataService<Calendar>
    {
        public CalendarService(TestingSettings testingSettings, string accessToken)
            : base(testingSettings, accessToken)
        {
            EndpointUrl = "/sf/system/calendars";
        }

    }
}
