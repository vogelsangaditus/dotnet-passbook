using System.Collections.Generic;
using Newtonsoft.Json;

namespace Passbook.Generator.Tags;

public class WifiAccess(IEnumerable<WifiNetwork> networks) : SemanticTag("wifiAccess")
{
    public override void WriteValue(JsonWriter writer)
    {
        writer.WriteStartArray();

        foreach (var network in networks)
        {
            network.WriteValue(writer);
        }

        writer.WriteEndArray();
    }
}