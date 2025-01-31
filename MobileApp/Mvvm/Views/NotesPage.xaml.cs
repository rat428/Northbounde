using Northboundei.Mobile.Mvvm.ViewModels;

namespace Northboundei.Mobile.Mvvm.Views;

public partial class NotesPage : ContentPage
{
    public NotesPage(NotesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}