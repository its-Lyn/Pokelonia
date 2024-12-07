using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokelonia.Models;

public class Page
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("next")]
    public string? Next { get; set; }
    
    [JsonPropertyName("previous")]
    public string? Previous { get; set; }
    
    [JsonPropertyName("results")]
    public required List<PageEntry> Results { get; set; }
}