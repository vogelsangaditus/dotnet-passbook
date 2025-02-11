using Newtonsoft.Json;

namespace Passbook.Generator.Tags;

public class WifiNetwork(string ssid, string password)
{
    public void WriteValue(JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("ssid");
        writer.WriteValue(ssid);
        writer.WritePropertyName("password");
        writer.WriteValue(password);
        writer.WriteEndObject();
    }
}
