using CommunityToolkit.Maui.Views;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class Section4 : ContentView
    {
        public Section4()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            AppShell._publichomeviewModel.Section4ViewModel._signaturedrawingView = myDrawingView;
            AppShell._publichomeviewModel.Section4ViewModel._signaturedrawingView.Clear(); //We can directly call the signaturedrawing view from xaml just for taking the code from vm it was done :) 
        }
    }
}
