using CommunityToolkit.Mvvm.ComponentModel;
using Northboundei.Mobile.Services;
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
        ObservableCollection<DateOnly> _missedSessions = [];

        [ObservableProperty]
        private DateOnly _selectedMakeUpSession;

        NoteService NoteService { get; set; }

        public Section3MakeUpViewModel() : base("Make-Up Attendance")
        {

        }

        // Method to load the missed sessions
        public async Task LoadMissedSessions()
        {
            var missedNotes = (await NoteService.GetNotesAsync()).Where(s => s.AttendanceType?.ToLower() == "missed");

            MissedSessions.Clear();
            foreach (var missedNote in missedNotes)
            {
                // Pattern Matched
                if (missedNote is { } h && h.SessionDate != null)
                {
                    MissedSessions.Add(missedNote.SessionDate!.Value);
                }
            }
        }
    }
}
