using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using Pokelonia.Models.Pokemon;

namespace Pokelonia.ViewModels;

public partial class TypeViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _typeName;
    
    public TypeViewModel(Type type)
    {
        TypeName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type.TypeData.Name.ToLower());
    }
}