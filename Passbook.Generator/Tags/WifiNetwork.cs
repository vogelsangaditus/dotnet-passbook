using System.Text.Json;

namespace Passbook.Generator.Tags;

public class WifiNetwork(string ssid, string password)
{
    public void WriteValue(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("ssid");
        writer.WriteStringValue(ssid);
        writer.WritePropertyName("password");
        writer.WriteStringValue(password);
        writer.WriteEndObject();
    }
}
