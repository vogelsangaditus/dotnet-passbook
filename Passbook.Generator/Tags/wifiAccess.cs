using System.Collections.Generic;
using System.Text.Json;

namespace Passbook.Generator.Tags;

public class WifiAccess(IEnumerable<WifiNetwork> networks) : SemanticTag("wifiAccess")
{
    public override void WriteValue(Utf8JsonWriter writer)
    {
        writer.WriteStartArray();

        foreach (var network in networks)
        {
            network.WriteValue(writer);
        }

        writer.WriteEndArray();
    }
}
