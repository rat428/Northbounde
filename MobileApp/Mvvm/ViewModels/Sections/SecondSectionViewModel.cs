using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class SecondSectionViewModel:ObservableObject
    {
        [ObservableProperty]
        bool _isDraftEnabled;

        private string _selectedAttendanceType;
        private string _covisitWhoElse;
        private string _selectedMakeUpSession;
        private string _selectedMissedReason;
        private string _remainingUnitsAuthorization;
        private string _remainingUnitsWeek;

        public event PropertyChangedEventHandler PropertyChanged;

        // Attendance Types
        public List<string> AttendanceTypeList { get; set; }

        // Make-up sessions (fetched from the missed sessions list)
        public List<string> MissedSessions { get; set; }

        // Missed reasons for missed attendance
        public List<string> MissedReasons { get; set; }

        // Boolean flags for visibility of the sections
        public bool IsAttendedSectionVisible => SelectedAttendanceType == "Attended";
        public bool IsMakeUpSectionVisible => SelectedAttendanceType == "Make-up";
        public bool IsCovisitSectionVisible => SelectedAttendanceType == "Covisit";
        public bool IsMissedSectionVisible => SelectedAttendanceType == "Missed";
        public ICommand OnChildSelectedCommand => new Command(OnChildSelected);

        private void OnChildSelected(object obj)
        {
            IsDraftEnabled = true;
        }


        // Properties for the text fields and dropdown selections
        public string SelectedAttendanceType
        {
            get => _selectedAttendanceType;
            set
            {
                if (_selectedAttendanceType != value)
                {
                    _selectedAttendanceType = value;
                    OnPropertyChanged();
                    OnAttendanceTypeChanged();
                }
            }
        }

        public string CovisitWhoElse
        {
            get => _covisitWhoElse;
            set
            {
                if (_covisitWhoElse != value)
                {
                    _covisitWhoElse = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedMakeUpSession
        {
            get => _selectedMakeUpSession;
            set
            {
                if (_selectedMakeUpSession != value)
                {
                    _selectedMakeUpSession = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedMissedReason
        {
            get => _selectedMissedReason;
            set
            {
                if (_selectedMissedReason != value)
                {
                    _selectedMissedReason = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RemainingUnitsAuthorization
        {
            get => _remainingUnitsAuthorization;
            set
            {
                if (_remainingUnitsAuthorization != value)
                {
                    _remainingUnitsAuthorization = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RemainingUnitsWeek
        {
            get => _remainingUnitsWeek;
            set
            {
                if (_remainingUnitsWeek != value)
                {
                    _remainingUnitsWeek = value;
                    OnPropertyChanged();
                }
            }
        }

        // Constructor to initialize the lists and defaults
        public SecondSectionViewModel()
        {
            // Initialize dropdown lists with example data
            AttendanceTypeList = new List<string> { "Attended", "Make-up", "Covisit", "Missed" };
            MissedSessions = new List<string>(); // Populate with actual missed sessions data from your database
            MissedReasons = new List<string> { "Illness", "Emergency", "Other" }; // Example missed reasons
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
        }

        // INotifyPropertyChanged implementation for binding updates
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

