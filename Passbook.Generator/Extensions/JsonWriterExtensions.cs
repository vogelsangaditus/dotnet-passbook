using System;
using Newtonsoft.Json;

namespace Passbook.Generator.Extensions
{
    public static class sonWriterExtensions
    {
        public static void WriteDateTimeValue(this JsonWriter writer, DateTimeOffset dateTime)
        {
            writer.WriteValue(dateTime.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }

        public static void WritePropertyIfNotNullOrEmpty(this JsonWriter writer, string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void WritePropertyIfNotNull(this JsonWriter writer, string name, int? value)
        {
            if (value != null)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
        }

        public static void WritePropertyIfNotNull(this JsonWriter writer, string name, bool? value)
        {
            if (value != null)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value.Value);
            }
        }
    }
}
