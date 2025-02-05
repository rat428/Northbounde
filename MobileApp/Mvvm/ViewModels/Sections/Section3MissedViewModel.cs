using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section3MissedViewModel : SectionViewModelBase
    {
        // Missed reasons for missed attendance
        [ObservableProperty]
        List<string> _missedReasons = ["Illness", "Emergency", "Other"];
        [ObservableProperty]
        string _selectedMissedReason;
        public Section3MissedViewModel() : base("Missed Attendance")
        {

        }
    }
}
