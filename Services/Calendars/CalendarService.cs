using AndcultureCode.CSharp.Sitefinity.Testing.Models.Configuration;
using AndcultureCode.CSharp.Sitefinity.Testing.Models.Content.Calendars;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Services.Calendars
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
