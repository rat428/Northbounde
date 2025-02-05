using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section11 : ContentView
{
    Section11ViewModel _viewModel;
    public Section11(Section11ViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {

    }
}