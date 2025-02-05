using CommunityToolkit.Maui.Views;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class Section14 : ContentView
    {
        Section14ViewModel _viewModel;
        public Section14(Section14ViewModel viewModel, bool TelehealthSelected)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            _viewModel.IsTelehealthSelected = TelehealthSelected;
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            _viewModel._signaturedrawingView = myDrawingView;
            _viewModel._signaturedrawingView.Clear(); //We can directly call the signaturedrawing view from xaml just for taking the code from vm it was done :) 
        }

        private void ClearUser_Clicked(object sender, EventArgs e)
        {
            _viewModel._signaturedrawingView = myUserDrawingView;
            _viewModel._signaturedrawingView.Clear();
        }
    }
}