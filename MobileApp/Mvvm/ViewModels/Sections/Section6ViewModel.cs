using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section6ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SessionEndTime))]
        private TimeSpan sessionStartTime;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SessionEndTime))]
        private string? selectedSessionSpan;
        
        public TimeSpan SessionEndTime => SessionStartTime.Add(TimeSpan.FromMinutes(int.Parse(SelectedSessionSpan?.Split(' ')[0] ?? "0")));


        [ObservableProperty]
        private ObservableCollection<string> sessionSpans = [
                "15 Min",
                "30 Min",
                "45 Min",
                "60 Min"
            ];
        public Section6ViewModel() : base("Session Span")
        {
            SessionStartTime = DateTime.Now.TimeOfDay;
        }
    }
}
