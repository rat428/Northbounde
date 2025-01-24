using CommunityToolkit.Maui.Views;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class ForthSection : ContentView
    {
        public ForthSection()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            AppShell._publichomeviewModel.ForthSectionViewModel._signaturedrawingView = myDrawingView;
            AppShell._publichomeviewModel.ForthSectionViewModel._signaturedrawingView.Clear(); //We can directly call the signaturedrawing view from xaml just for taking the code from vm it was done :) 
        }
    }
}
