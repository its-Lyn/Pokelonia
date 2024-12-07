using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokelonia.Models.Pokemon;

public class Pokemon
{
    [JsonPropertyName("sprites")]
    public required Sprites Sprites { get; set; }
    
    [JsonPropertyName("types")]
    public required List<Type> Types { get; set; }
}