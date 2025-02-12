using System;

namespace Passbook.Generator.Tags;

public class EventStartDate : SemanticTagBaseValue
{
    /// <summary>
    /// The date and time the event starts. Use this key for any type of event ticket.
    /// </summary>
    /// <param name="value">ISO 8601 date as string</param>
    public EventStartDate(string value) : base("eventStartDate", value)
    {
    }

    /// <summary>
    /// The date and time the event starts. Use this key for any type of event ticket.
    /// </summary>
    /// <param name="value">date and time with time zone</param>
    public EventStartDate(DateTimeOffset value) : base("eventStartDate", value)
    {
    }
}
