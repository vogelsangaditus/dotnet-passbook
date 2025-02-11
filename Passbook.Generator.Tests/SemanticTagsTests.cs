using System.Text;
using System.Text.Json;
using Passbook.Generator.Tags;
using Xunit;

namespace Passbook.Generator.Tests;

public class SemanticTagsTests
{
    [Fact]
    public void EnsureSemanticFieldsIsGeneratedCorrectly()
    {
        PassGeneratorRequest request = new PassGeneratorRequest();
        request.SemanticTags.Add(new AirlineCode("EX"));
        request.SemanticTags.Add(new Balance("1000", "GBP"));
        request.SemanticTags.Add(new VenueCloseDate(new DateTimeOffset(2025, 8, 10, 9, 10, 0, new TimeSpan(3, 0, 0))));

        using var ms = new MemoryStream();
        var options = new JsonWriterOptions { Indented = true };
        using (var writer = new Utf8JsonWriter(ms, options))

            request.Write(writer);

        string jsonString = Encoding.UTF8.GetString(ms.ToArray());

        using var doc = JsonDocument.Parse(jsonString);
        var root = doc.RootElement;

        if (!root.TryGetProperty("semantics", out JsonElement semantics))
        {
            Assert.Fail("semantics not found");
        }

        if (!semantics.TryGetProperty("airlineCode", out JsonElement airlineCode))
        {
            Assert.Fail("airlineCode not found");
        }
        Assert.Equal("EX", airlineCode.GetString());

        if (!semantics.TryGetProperty("balance", out JsonElement balance))
        {
            Assert.Fail("balance not found");
        }

        if (!balance.TryGetProperty("amount", out JsonElement amount))
        {
            Assert.Fail("amount not found");
        }
        Assert.Equal("1000", amount.GetString());
    }

    [Fact]
    public void EnsureVenueCloseDateIsGeneratedCorrectly()
    {
        PassGeneratorRequest request = new PassGeneratorRequest();
        request.SemanticTags.Add(new VenueCloseDate(new DateTimeOffset(2025, 8, 10, 9, 10, 0, new TimeSpan(3, 0, 0))));

        using var ms = new MemoryStream();
        var options = new JsonWriterOptions { Indented = true };
        using (var writer = new Utf8JsonWriter(ms, options))

            request.Write(writer);

        string jsonString = Encoding.UTF8.GetString(ms.ToArray());

        using var doc = JsonDocument.Parse(jsonString);
        var root = doc.RootElement;

        if (!root.TryGetProperty("semantics", out JsonElement semantics))
        {
            Assert.Fail("semantics not found");
        }

        if (!semantics.TryGetProperty("venueCloseDate", out JsonElement closeDate))
        {
            Assert.Fail("closeDate not found");
        }

        Assert.Equal("2025-08-10T09:10:00+03:00", closeDate.GetString());
    }

    [Fact]
    public void EnsureWifiAccessIsGeneratedCorrectly()
    {
        PassGeneratorRequest request = new PassGeneratorRequest();
        request.SemanticTags.Add(new WifiAccess([
            new WifiNetwork("wifi-entrance", "834AX5?15"),
            new WifiNetwork("wifi-main-hall", "187r!16iK")
            ]));

        using var ms = new MemoryStream();
        var options = new JsonWriterOptions { Indented = true };
        using (var writer = new Utf8JsonWriter(ms, options))

            request.Write(writer);

        string jsonString = Encoding.UTF8.GetString(ms.ToArray());

        using var doc = JsonDocument.Parse(jsonString);
        var root = doc.RootElement;

        if (!root.TryGetProperty("semantics", out JsonElement semantics))
        {
            Assert.Fail("semantics not found");
        }

        if (!semantics.TryGetProperty("wifiAccess", out JsonElement wifiAccess))
        {
            Assert.Fail("closeDate not found");
        }

        var networks = new List<JsonElement>();
        foreach (var network in wifiAccess.EnumerateArray())
        {
            networks.Add(network);
        }

        Assert.Equal(2, networks.Count);

        if (!networks[0].TryGetProperty("ssid", out JsonElement ssid))
        {
            Assert.Fail("first wifi network has not a ssid property");
        }

        Assert.Equal("wifi-entrance", ssid.GetString());

        if (!networks[0].TryGetProperty("password", out JsonElement password))
        {
            Assert.Fail("first wifi network has not a password property");
        }

        Assert.Equal("834AX5?15", password.GetString());
    }
}
