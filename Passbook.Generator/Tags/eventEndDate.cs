using System;

namespace Passbook.Generator.Tags;

public class EventEndDate : SemanticTagBaseValue
{
    /// <summary>
    /// The date and time the event ends. Use this key for any type of event ticket.
    /// </summary>
    /// <param name="value">ISO 8601 date as string</param>
    public EventEndDate(string value) : base("eventEndDate", value)
    {
    }

    /// <summary>
    /// The date and time the event ends. Use this key for any type of event ticket.
    /// </summary>
    /// <param name="value">date and time with time zone</param>
    public EventEndDate(DateTimeOffset value) : base("eventEndDate", value)
    {
    }
}
