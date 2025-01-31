using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section3MakeUpViewModel : SectionViewModelBase
    {

        // Make-up sessions (fetched from the missed sessions list)
        [ObservableProperty]
        ObservableCollection<string> _missedSessions;

        [ObservableProperty]
        private string _selectedMakeUpSession;
        public Section3MakeUpViewModel() : base("Make-Up Attendance")
        {
            MissedSessions = new();
        }

        // Method to load the missed sessions
        public async Task LoadMissedSessions()
        {
            // Fetch the missed sessions from the database
            // MissedSessions = await _service.GetMissedSessions();
        }
    }
}
