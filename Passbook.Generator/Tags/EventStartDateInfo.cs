using System;
using Newtonsoft.Json;
using Passbook.Generator.Extensions;

namespace Passbook.Generator.Tags;

public class EventStartDateInfo(
    DateTimeOffset date,
    bool ignoreTimeComponents,
    string timeZone = null) : SemanticTag("eventStartDateInfo")
{
    public override void WriteValue(JsonWriter writer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("date");
        writer.WriteDateTimeValue(date);

        if (ignoreTimeComponents)
        {
            writer.WritePropertyName("ignoreTimeComponents");
            writer.WriteValue(ignoreTimeComponents);
        }

        writer.WritePropertyIfNotNullOrEmpty("timeZone", timeZone);

        writer.WriteEndObject();
    }
}