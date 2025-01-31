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
        private string _covisitWhoElse;
        [ObservableProperty]
        private string _selectedMakeUpSession;
        [ObservableProperty]
        private string _selectedMissedReason;
        [ObservableProperty]
        private string _remainingUnitsAuthorization;
        [ObservableProperty]
        private string _remainingUnitsWeek;

        public event PropertyChangedEventHandler PropertyChanged;

        // Attendance Types
        [ObservableProperty]
        public List<string> _AttendanceTypeList = [ "Attended", "Make-up", "Covisit", "Missed" ];

        // Make-up sessions (fetched from the missed sessions list)
        public List<string> MissedSessions { get; set; }

        // Missed reasons for missed attendance
        [ObservableProperty]
        public List<string> MissedReasons = ["Illness", "Emergency", "Other"];

        // Boolean flags for visibility of the sections
        public bool IsMakeUpSectionVisible => SelectedAttendanceType == "Make-up";
        public bool IsCovisitSectionVisible => SelectedAttendanceType == "Covisit";
        public bool IsMissedSectionVisible => SelectedAttendanceType == "Missed";

        [RelayCommand]
        private void OnChildSelected()
        {
            IsDraftEnabled = true;
        }

        // Constructor to initialize the lists and defaults
        public Section2ViewModel() : base("Attendance")
        {
            // Initialize dropdown lists with example data
            MissedSessions = new List<string>(); // Populate with actual missed sessions data from your database
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(SelectedAttendanceType))
            {
                OnAttendanceTypeChanged();
            }
        }

        // Method that gets triggered when AttendanceType changes
        private void OnAttendanceTypeChanged()
        {
            // Logic to handle what happens when AttendanceType changes
            // For example, reset any selected sessions or reasons
            if (SelectedAttendanceType == "Make-up")
            {
                // Logic to fetch available missed sessions and make-ups
            }
            else if (SelectedAttendanceType == "Covisit")
            {
                // Logic to fetch Covisit-related details
            }
            else if (SelectedAttendanceType == "Missed")
            {
                // Reset reason selection for missed attendance
                SelectedMissedReason = null;
            }

            // You can add additional logic to update Remaining Units calculations here as well

            UpdateMenuVisibility();
            UpdateSectionCompletionStatus();
        }

        // INotifyPropertyChanged implementation for binding updates
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Adjust menu visibility based on the "Attendance Type" selected
        private void UpdateMenuVisibility()
        {
            // Logic to adjust menu visibility based on the "Attendance Type" selected
        }

        // Change background colors of sections based on completion status
        private void UpdateSectionCompletionStatus()
        {
            // Logic to change background colors of sections based on completion status
        }
    }
}
