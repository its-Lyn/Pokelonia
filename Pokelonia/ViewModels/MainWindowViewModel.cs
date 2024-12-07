using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pokelonia.Models;
using Pokelonia.Services;

namespace Pokelonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly PokeAPI _pokeAPI = new PokeAPI();
    
    private Page _currentPage = null!;
    
    [ObservableProperty]
    private ObservableCollection<PageEntryViewModel> _results = new ObservableCollection<PageEntryViewModel>();

    [ObservableProperty] private bool _lastEnabled = true;
    [ObservableProperty] private bool _nextEnabled = true;
    [ObservableProperty] private bool _previousEnabled;
    [ObservableProperty] private bool _firstEnabled;

    [ObservableProperty] private int _currentPageIndex = 1;
    [ObservableProperty] private int _pageCount;
    
    [ObservableProperty] private string _pageText = null!;

    private const int Limit = 30;

    public MainWindowViewModel() => Initialise();

    // ReSharper disable once AsyncVoidMethod
    private async void Initialise()
    {
        _currentPage = await _pokeAPI.GetPageAsync(Limit);
        foreach (PageEntry entry in _currentPage.Results) 
            Results.Add(new PageEntryViewModel(entry));

        PageCount = (int)MathF.Ceiling((float)_currentPage.Count / Limit);
        PageText = $"{CurrentPageIndex}/{PageCount}";
    }

    private void Clear()
    {
        Results.Clear();
    }

    [RelayCommand]
    private async Task LoadFirstPage()
    {
        Clear();

        CurrentPageIndex = 1;
        PageText = $"{CurrentPageIndex}/{PageCount}";

        PreviousEnabled = false;
        FirstEnabled = false;
        LastEnabled = true;
        NextEnabled = true;
        
        _currentPage = await _pokeAPI.GetPageAsync(Limit, $"https://pokeapi.co/api/v2/pokemon?offset=0&limit={Limit}");
        foreach (PageEntry entry in _currentPage.Results) 
            Results.Add(new PageEntryViewModel(entry));
    }

    [RelayCommand]
    private async Task LoadLastPage()
    {
        Clear();

        CurrentPageIndex = PageCount;
        PageText = $"{CurrentPageIndex}/{PageCount}";

        PreviousEnabled = true;
        FirstEnabled = true;
        LastEnabled = false;
        NextEnabled = false;

        int count = _currentPage.Count;
        _currentPage = await _pokeAPI.GetPageAsync(Limit, $"https://pokeapi.co/api/v2/pokemon?offset={count - Limit}&limit={Limit}");
        foreach (PageEntry entry in _currentPage.Results) 
            Results.Add(new PageEntryViewModel(entry));
    }

    [RelayCommand]
    private async Task LoadPreviousPage()
    {
        Clear();

        CurrentPageIndex--;
        PageText = $"{CurrentPageIndex}/{PageCount}";

        if (CurrentPageIndex == 1)
            FirstEnabled = false;

        if (CurrentPageIndex != PageCount)
            LastEnabled = true;

        string? previous = _currentPage.Previous;
        _currentPage = await _pokeAPI.GetPageAsync(Limit, previous);
        foreach (PageEntry entry in _currentPage.Results) 
            Results.Add(new PageEntryViewModel(entry));
        
        if (_currentPage.Previous is null)
        {
            PreviousEnabled = false;
            return;
        }

        if (_currentPage.Next is not null)
            NextEnabled = true;
    }

    [RelayCommand]
    private async Task LoadNextPage()
    {
        Clear();

        CurrentPageIndex++;
        PageText = $"{CurrentPageIndex}/{PageCount}";

        if (CurrentPageIndex > 1)
            FirstEnabled = true;

        if (CurrentPageIndex == PageCount)
            LastEnabled = false;
        
        string? next = _currentPage.Next;
        _currentPage = await _pokeAPI.GetPageAsync(Limit, next);
        foreach (PageEntry entry in _currentPage.Results) 
            Results.Add(new PageEntryViewModel(entry));
        
        if (_currentPage.Next is null)
        {
            NextEnabled = false;
            return;
        }
        
        if (_currentPage.Previous is not null)
            PreviousEnabled = true;
    }
}
