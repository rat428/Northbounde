using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class Section12 : ContentView
    {
        public Section12(Section12ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
