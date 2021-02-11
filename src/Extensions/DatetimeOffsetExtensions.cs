using Shouldly;
using System;

namespace AndcultureCode.CSharp.Sitefinity.Testing.Extensions
{
    public static class DatetimeOffsetExtensions
    {
        /// <summary>
        /// When creating datetimeoffsets through OData, a segment of the time data gets truncated
        /// in the request and therefore persisted inaccurately, albeit at a very small scale.
        /// That truncated piece needs to be ignored when asserting.
        /// </summary>
        /// <param name="thisDatetimeOffset"></param>
        /// <param name="datetimeOffsetToEqual"></param>
        public static void ShouldBeWithinMarginOfError(this DateTimeOffset? thisDatetimeOffset, DateTimeOffset? datetimeOffsetToEqual)
        {
            thisDatetimeOffset.Value.ShouldBeWithinMarginOfError(datetimeOffsetToEqual.Value);
        }

        /// <summary>
        /// When creating datetimeoffsets through OData, a segment of the time data gets truncated
        /// in the request and therefore persisted inaccurately, albeit at a very small scale.
        /// That truncated piece needs to be ignored when asserting.
        /// </summary>
        /// <param name="thisDatetimeOffset"></param>
        /// <param name="datetimeOffsetToEqual"></param>
        public static void ShouldBeWithinMarginOfError(this DateTimeOffset thisDatetimeOffset, DateTimeOffset datetimeOffsetToEqual)
        {
            thisDatetimeOffset.ShouldBe(datetimeOffsetToEqual, new TimeSpan(0, 0, 0, 0, 999));
        }
    }
}
