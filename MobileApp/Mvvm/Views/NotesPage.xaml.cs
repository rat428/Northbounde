using Northboundei.Mobile.Mvvm.ViewModels;

namespace Northboundei.Mobile.Mvvm.Views;

public partial class NotesPage : ContentPage
{
    HomeViewModel _viewmodel;
    public NotesPage()
    {
        InitializeComponent();
        BindingContext = _viewmodel = AppShell._publichomeviewModel;
    }
}