using System;
using Newtonsoft.Json;

namespace Passbook.Generator.Extensions
{
    public static class Utf8JsonWriterExtensions
    {
        public static void WriteDateTimeValue(this JsonWriter writer, DateTimeOffset dateTime)
        {
            writer.WriteValue(dateTime.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}
