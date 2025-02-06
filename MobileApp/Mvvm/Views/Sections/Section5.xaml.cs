using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;
public partial class Section5 : ContentView
{
    Section5ViewModel _viewModel;
    public Section5(Section5ViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        _viewModel._signatureDrawingView = myDrawingView;
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        // TODO: Alternatively, we can directly clear the Lines property in the ViewModel
        _viewModel._signatureDrawingView = myDrawingView;
        _viewModel._signatureDrawingView.Clear();
    }
}
