using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class FirstSectionViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> _childName;
        [ObservableProperty]
        ObservableCollection<string> _sessionDate;
        [ObservableProperty]
        ObservableCollection<string> _serviceType;
        [ObservableProperty]
        int _remainingUnits;
        [ObservableProperty]
        int _remainingUnitsWeek;
        [ObservableProperty]
        double childAge;

    }
}
