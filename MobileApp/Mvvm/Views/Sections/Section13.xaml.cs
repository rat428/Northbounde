using CommunityToolkit.Maui.Views;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections
{
    public partial class Section13 : ContentView
    {
        public Section13()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            AppShell._publichomeviewModel.Section13ViewModel._signaturedrawingView = myDrawingView;
            AppShell._publichomeviewModel.Section13ViewModel._signaturedrawingView.Clear(); //We can directly call the signaturedrawing view from xaml just for taking the code from vm it was done :) 
        }

        private void ClearUser_Clicked(object sender, EventArgs e)
        {
            AppShell._publichomeviewModel.Section13ViewModel._signaturedrawingView = myUserDrawingView;
            AppShell._publichomeviewModel.Section13ViewModel._signaturedrawingView.Clear();
        }
    }
}