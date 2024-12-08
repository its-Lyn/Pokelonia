using Avalonia.Controls;
using Avalonia.Interactivity;
using Pokelonia.ViewModels;

namespace Pokelonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        About about = new About
        {
            DataContext = new ViewModelBase()
        };

        about.ShowDialog(this);
    }
}