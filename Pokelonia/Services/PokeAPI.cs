using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Pokelonia.Models;
using Pokelonia.Models.Pokemon;

namespace Pokelonia.Services;

public class PokeAPI
{
    private readonly HttpClient _client = new HttpClient();

    public async Task<Page> GetPageAsync(int limit = 20, string? pageUrl = null)
    {
        HttpResponseMessage response = await _client.GetAsync(pageUrl ?? $"https://pokeapi.co/api/v2/pokemon?limit={limit}");
        response.EnsureSuccessStatusCode();
        
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Page>(json) ?? throw new NullReferenceException();
    }

    public async Task<Pokemon> GetPokemonAsync(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Pokemon>(json) ?? throw new NullReferenceException();
    }
}