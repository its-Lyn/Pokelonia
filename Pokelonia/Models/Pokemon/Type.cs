using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokelonia.Models.Pokemon;

public class TypeData
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}

public class Type
{
    [JsonPropertyName("type")]
    public required TypeData TypeData { get; set; }
    
    [JsonPropertyName("slot")]
    public int Slot { get; set; }
}