using Newtonsoft.Json;
using Passbook.Generator.Tags;
using System.IO;
using System.Text;
using Passbook.Generator.Tags;
using Xunit;
using System;
using System.Collections.Generic;
using System.Text.Json;

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

        using MemoryStream ms = new MemoryStream();
        using StreamWriter sr = new StreamWriter(ms);
        using JsonWriter writer = new JsonTextWriter(sr);
        writer.Formatting = Formatting.Indented;
        request.Write(writer);

        string jsonString = Encoding.UTF8.GetString(ms.ToArray());

        var settings = new JsonSerializerSettings { DateParseHandling = DateParseHandling.None };

        dynamic json = JsonConvert.DeserializeObject(jsonString, settings);

        var semantics = json["semantics"];

        var airlineCode = semantics.airlineCode;
        Assert.Equal("EX", (string)airlineCode);

        var balance = semantics.balance;
        Assert.Equal("1000", (string)balance.amount);
        Assert.Equal("GBP", (string)balance.currencyCode);
    }

    [Fact]
    public void EnsureVenueCloseDateIsGeneratedCorrectly()
    {
        PassGeneratorRequest request = new PassGeneratorRequest();
        request.SemanticTags.Add(new VenueCloseDate(new DateTimeOffset(2025, 8, 10, 9, 10, 0, new TimeSpan(3, 0, 0))));

        using MemoryStream ms = new MemoryStream();
        using StreamWriter sr = new StreamWriter(ms);
        using JsonWriter writer = new JsonTextWriter(sr);
        writer.Formatting = Formatting.Indented;
        request.Write(writer);

        string jsonString = Encoding.UTF8.GetString(ms.ToArray());

        using var doc = JsonDocument.Parse(jsonString);
        var root = doc.RootElement;

        if (!root.TryGetProperty("semantics", out JsonElement semantics))
        {
            Assert.True(false, "semantics not found");
        }

        if (!semantics.TryGetProperty("venueCloseDate", out JsonElement closeDate))
        {
            Assert.True(false, "closeDate not found");
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

        using MemoryStream ms = new MemoryStream();
        using StreamWriter sr = new StreamWriter(ms);
        using JsonWriter writer = new JsonTextWriter(sr);
        writer.Formatting = Formatting.Indented;

        request.Write(writer);

        string jsonString = Encoding.UTF8.GetString(ms.ToArray());

        using var doc = JsonDocument.Parse(jsonString);
        var root = doc.RootElement;

        if (!root.TryGetProperty("semantics", out JsonElement semantics))
        {
            Assert.True(true, "semantics not found");
        }

        if (!semantics.TryGetProperty("wifiAccess", out JsonElement wifiAccess))
        {
            Assert.True(true, "closeDate not found");
        }

        var networks = new List<JsonElement>();
        foreach (var network in wifiAccess.EnumerateArray())
        {
            networks.Add(network);
        }

        Assert.Equal(2, networks.Count);

        if (!networks[0].TryGetProperty("ssid", out JsonElement ssid))
        {
            Assert.True(false, "first wifi network has not a ssid property");
        }

        Assert.Equal("wifi-entrance", ssid.GetString());

        if (!networks[0].TryGetProperty("password", out JsonElement password))
        {
            Assert.True(false, "first wifi network has not a password property");
        }

        Assert.Equal("834AX5?15", password.GetString());
    }

}
