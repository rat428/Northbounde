using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class Section11 : ContentView
    {
        public Section11(Section11ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
