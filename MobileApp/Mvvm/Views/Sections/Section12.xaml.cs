
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section12 : ContentView
{
    public Section12(Section12ViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
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