using System.Text;
using System.Text.Json;
using Passbook.Generator.Fields;
using Xunit;

namespace Passbook.Generator.Tests
{
    public class StylingKeysTests
    {
        [Fact]
        public void EnsureGeneratedCorrectly()
        {
            PassGeneratorRequest request = new PassGeneratorRequest();
            request.PreferredStyleSchemes.Add(PreferredStyleScheme.EventTicket);
            request.PreferredStyleSchemes.Add(PreferredStyleScheme.PosterEventTicket);

            using var ms = new MemoryStream();
            var options = new JsonWriterOptions { Indented = true };
            using (var writer = new Utf8JsonWriter(ms, options))

                request.Write(writer);

            string jsonString = Encoding.UTF8.GetString(ms.ToArray());

            using var doc = JsonDocument.Parse(jsonString);
            var root = doc.RootElement;

            if (!root.TryGetProperty("preferredStyleSchemes", out JsonElement preferredStyleSchemes))
            {
                Assert.Fail("preferredStyleSchemes not found");
            }

            var elements = new List<JsonElement>();
            foreach (var element in preferredStyleSchemes.EnumerateArray())
            {
                elements.Add(element);
            }

            Assert.Equal(2, elements.Count);

            Assert.Equal("eventTicket", elements[0].ToString());
            Assert.Equal("posterEventTicket", elements[1].ToString());
        }
    }
}
