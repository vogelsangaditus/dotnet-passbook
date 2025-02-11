using System;
using System.Text.Json;

namespace Passbook.Generator.Extensions
{
    public static class Utf8JsonWriterExtensions
    {
        public static void WriteDateTimeValue(this Utf8JsonWriter writer, DateTimeOffset dateTime)
        {
            writer.WriteStringValue(dateTime.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}
