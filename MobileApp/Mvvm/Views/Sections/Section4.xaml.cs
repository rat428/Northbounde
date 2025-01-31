using CommunityToolkit.Maui.Views;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class Section4 : ContentView
    {
        Section4ViewModel _viewModel;
        public Section4(Section4ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            _viewModel._signaturedrawingView = myDrawingView;
            _viewModel._signaturedrawingView.Clear(); //We can directly call the signaturedrawing view from xaml just for taking the code from vm it was done :) 
        }
    }
}
