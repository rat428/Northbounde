using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section1 : ContentView
{
	public Section1(Section1ViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}