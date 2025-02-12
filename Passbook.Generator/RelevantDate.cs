using System;
using System.Text.Json;
using Passbook.Generator.Extensions;

namespace Passbook.Generator
{
    public class RelevantDate
    {
        /// <summary>
        /// A single relevant date. This matches the existing behavior of 'relevantDate'
        /// </summary>
        public RelevantDate(DateTimeOffset relevantDate)
        {
            StartDate = relevantDate;
        }

        /// <summary>
        /// A relevant date time span. A relevancy interval can span at most 24 hours.
        /// </summary>
        public RelevantDate(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTimeOffset StartDate { get; }

        public DateTimeOffset? EndDate { get; }

        public void Write(Utf8JsonWriter writer)
        {
            Validate();

            writer.WriteStartObject();

            if (EndDate == null)
            {
                writer.WritePropertyName("relevantDate");
                writer.WriteDateTimeValue(StartDate);
            }
            else
            {
                writer.WritePropertyName("startDate");
                writer.WriteDateTimeValue(StartDate);

                writer.WritePropertyName("endDate");
                writer.WriteDateTimeValue(EndDate.Value);
            }

            writer.WriteEndObject();
        }

        private void Validate()
        {
            if (EndDate.HasValue && (EndDate.Value - StartDate).TotalHours > 24d)
            {
                throw new ArgumentOutOfRangeException("A relevancy interval can span at most 24 hours.");
            }
        }
    }
}
