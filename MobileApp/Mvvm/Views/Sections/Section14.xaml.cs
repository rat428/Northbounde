using CommunityToolkit.Maui.Views;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class Section14 : ContentView
    {
        Section14ViewModel _viewModel;
        public Section14(Section14ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            // TODO: Alternatively, we can directly clear the Lines property in the ViewModel
            _viewModel._signaturedrawingView = myDrawingView;
            _viewModel._signaturedrawingView.Clear(); 
        }
    }
}