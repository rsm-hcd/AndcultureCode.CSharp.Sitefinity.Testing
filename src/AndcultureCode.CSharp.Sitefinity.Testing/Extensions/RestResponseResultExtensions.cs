using AndcultureCode.CSharp.Sitefinity.Core.Models.Services;
using AndcultureCode.CSharp.Sitefinity.Testing.Constants;
using Shouldly;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Extensions
{
    public static class RestResponseResultExtensions
    {
        public static void ShouldBeExpectedStatusCode<T>(this RestResponseResult<T> restResponseResult)
        {
            restResponseResult.WasExpectedStatusCode.ShouldBeTrue(
                GetUnexpectedStatusCodeErrorMessage(restResponseResult));
        }

        public static string GetUnexpectedStatusCodeErrorMessage<T>(this RestResponseResult<T> restResponseResult)
        {
            var baseErrorMessage = $"Expected status code is '{restResponseResult.ExpectedStatusCode}' but actual status was code was '{restResponseResult.RestResponse.StatusCode}'.";
            var errorMessage = $"{baseErrorMessage}'.  REST response's content is: '{restResponseResult.RestResponse.Content}'";

            if (restResponseResult.RestResponse.IsWaitAMomentResponse())
            {
                /*
                 * Sitefinity sometimes returns the "Please wait a moment" response instead of the expected status code indicating something is happening
                 * like Sitefinity is rebooting, upgrading, etc. Instead of outputting the entire HTML content including the CSS and HTML markup,
                 * return a simple message so it's easier to understand when failures occur
                */
                errorMessage = $"{baseErrorMessage}'.  Sitefinity is returning the '{RestResponseContentData.PLEASE_WAIT_A_MOMENT}' message in it's content response";
            }

            return errorMessage;
        }
    }
}
