using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Xunit;

namespace Passbook.Generator.Tests
{

    public class RelevanceKeysTests
    {
        [Fact]
        public void EnsureRelevantDatesGeneratedCorrectly()
        {
            PassGeneratorRequest request = new PassGeneratorRequest();
            var singleDate = new DateTimeOffset(2025, 2, 11, 9, 40, 0, new TimeSpan(2, 0, 0));
            request.RelevantDates.Add(new RelevantDate(singleDate));

            var startDate = new DateTimeOffset(2025, 10, 31, 6, 30, 0, new TimeSpan(4, 0, 0));
            var endDate = new DateTimeOffset(2025, 10, 31, 23, 00, 0, new TimeSpan(4, 0, 0));
            request.RelevantDates.Add(new RelevantDate(startDate, endDate));

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sr = new StreamWriter(ms))
                {
                    using (JsonWriter writer = new JsonTextWriter(sr))
                    {
                        writer.Formatting = Formatting.Indented;
                        request.Write(writer);
                    }

                    string jsonString = Encoding.UTF8.GetString(ms.ToArray());

                    using var doc = JsonDocument.Parse(jsonString);
                    var root = doc.RootElement;

                    if (!root.TryGetProperty("relevantDates", out JsonElement relevantDates))
                    {
                        Assert.True(false, "relevantDates not found");
                    }

                    var elements = new List<JsonElement>();
                    foreach (var element in relevantDates.EnumerateArray())
                    {
                        elements.Add(element);
                    }

                    Assert.Equal(2, elements.Count);

                    if (!elements[0].TryGetProperty("relevantDate", out JsonElement relevantDateElement))
                    {
                        Assert.True(false, "first relevantDate has not a reveventDate property");
                    }

                    Assert.Equal("2025-02-11T09:40:00+02:00", relevantDateElement.GetString());

                    if (!elements[1].TryGetProperty("startDate", out JsonElement startDateElement))
                    {
                        Assert.True(false, "second relevantDate has not a startDate property");
                    }

                    Assert.Equal("2025-10-31T06:30:00+04:00", startDateElement.GetString());

                    if (!elements[1].TryGetProperty("endDate", out JsonElement endDateElement))
                    {
                        Assert.True(false, "second relevantDate has not a endDate property");
                    }

                    Assert.Equal("2025-10-31T23:00:00+04:00", endDateElement.GetString());
                }
            }
        }
    } 
}
