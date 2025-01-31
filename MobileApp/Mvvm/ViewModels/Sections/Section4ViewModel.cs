using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System.Linq;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls.Shapes;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section4ViewModel : SectionViewModelBase
    {
        public Section4ViewModel() : base("Service Location")
        {

        }

        [ObservableProperty]
        private ObservableCollection<string> serviceLocations = new()
        {
            "Home", "Community", "Daycare", "Other", "Telehealth"
        };

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOtherLocationSelected))]
        [NotifyPropertyChangedFor(nameof(IsTelehealthSelected))]
        [NotifyPropertyChangedFor(nameof(IsNonTelehealthSelected))]
        private string selectedServiceLocation;

        [ObservableProperty]
        private string otherLocation;

        public bool IsOtherLocationSelected => SelectedServiceLocation == "Other";
        public bool IsTelehealthSelected => SelectedServiceLocation == "Telehealth";
        public bool IsNonTelehealthSelected => !IsTelehealthSelected && !string.IsNullOrEmpty(SelectedServiceLocation);
    }
}
