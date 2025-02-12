using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
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

            using var stringWriter = new StringWriter(CultureInfo.InvariantCulture);
            using var jsonWriter = new JsonTextWriter(stringWriter);
            jsonWriter.Formatting = Formatting.Indented;
    
            request.Write(jsonWriter);

            using var doc = JsonDocument.Parse(stringWriter.ToString());
            var root = doc.RootElement;

            if (!root.TryGetProperty("preferredStyleSchemes", out JsonElement preferredStyleSchemes))
            {
                Assert.True(false, "preferredStyleSchemes not found");
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
