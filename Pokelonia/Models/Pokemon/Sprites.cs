using System.Text.Json.Serialization;

namespace Pokelonia.Models.Pokemon;

public class Sprites
{
    [JsonPropertyName("front_default")]
    public required string FrontDefault { get; set; }
}