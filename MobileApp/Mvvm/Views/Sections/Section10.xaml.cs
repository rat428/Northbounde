using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section10 : ContentView
{
    Section10ViewModel _viewModel;
    public Section10(Section10ViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length < 100)
        {
            ValidationLabel.IsVisible = true;
        }
        else
        {
            ValidationLabel.IsVisible = false;
        }
    }
}