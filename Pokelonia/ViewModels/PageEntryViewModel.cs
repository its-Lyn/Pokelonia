using System;
using System.Collections.ObjectModel;
using System.Globalization;
using AsyncImageLoader;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using Pokelonia.Models;
using Pokelonia.Models.Pokemon;
using Pokelonia.Services;
using Type = Pokelonia.Models.Pokemon.Type;

namespace Pokelonia.ViewModels;

public partial class PageEntryViewModel : ViewModelBase
{
    private readonly PokeAPI _pokeAPI = new PokeAPI();
    
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _url;
    
    [ObservableProperty] private string? _sprite;
    [ObservableProperty] private Bitmap? _image;
    [ObservableProperty] private bool _loaded;
    
    [ObservableProperty]
    private ObservableCollection<TypeViewModel> _pokeTypes = new ObservableCollection<TypeViewModel>();
    
    public PageEntryViewModel(PageEntry entry)
    {
        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entry.Name.ToLower());
        Url = entry.Url;
        
        GetPokemonData();
    }

    // ReSharper disable once AsyncVoidMethod
    private async void GetPokemonData()
    {
        Pokemon pokemon = await _pokeAPI.GetPokemonAsync(Url);
        Sprite = pokemon.Sprites.FrontDefault;

        if (Sprite is not null) Image = await ImageLoader.AsyncImageLoader.ProvideImageAsync(Sprite);
        else Image = new Bitmap(AssetLoader.Open(new Uri("avares://Pokelonia/Assets/not_found.png")));
        
        Loaded = true;
        
        foreach (Type pokeType in pokemon.Types)
            PokeTypes.Add(new TypeViewModel(pokeType));
    }
}