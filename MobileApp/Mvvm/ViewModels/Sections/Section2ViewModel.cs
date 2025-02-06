using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section2ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        bool _isDraftEnabled;

        [ObservableProperty]
        private string _selectedAttendanceType;
        
        
        [ObservableProperty]
        private string _remainingUnitsAuthorization;
        [ObservableProperty]
        private string _remainingUnitsWeek;

        // Attendance Types
        [ObservableProperty]
        public List<string> _AttendanceTypeList = [ "Attended", "Make-up", "Covisit", "Missed" ];

        public bool IsAttended => SelectedAttendanceType == "Attended";
        public bool IsMakeUp => SelectedAttendanceType == "Make-up";
        public bool IsCoVisit => SelectedAttendanceType == "Covisit";
        public bool IsMissed => SelectedAttendanceType == "Missed";

        // Constructor to initialize the lists and defaults
        public Section2ViewModel() : base("Attendance")
        {

        }
    }
}
