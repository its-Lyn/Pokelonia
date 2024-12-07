using System.Text.Json.Serialization;

namespace Pokelonia.Models;

public class PageEntry
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}