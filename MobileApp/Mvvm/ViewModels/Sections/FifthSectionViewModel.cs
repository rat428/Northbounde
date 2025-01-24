using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class FifthSectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime sessionStartTime;

        [ObservableProperty]
        private DateTime sessionEndTime;

        [ObservableProperty]
        private ObservableCollection<string> sessionSpans;

        [ObservableProperty]
        private string? selectedSessionSpan;

        public ICommand CalculateEndTimeCommand { get; }

        public FifthSectionViewModel()
        {
            SessionSpans = new ObservableCollection<string>
            {
                "15 Min",
                "30 Min",
                "45 Min",
                "60 Min"
            };
            SessionStartTime = DateTime.Now;
            SessionEndTime = SessionStartTime;

            CalculateEndTimeCommand = new RelayCommand(OnCalculateEndTime);
        }
        private void OnCalculateEndTime()
        {
            if (string.IsNullOrEmpty(SelectedSessionSpan)) return;

            TimeSpan sessionDuration = SelectedSessionSpan switch
            {
                "15 Min" => TimeSpan.FromMinutes(15),
                "30 Min" => TimeSpan.FromMinutes(30),
                "45 Min" => TimeSpan.FromMinutes(45),
                "60 Min" => TimeSpan.FromMinutes(60),
                _ => TimeSpan.Zero
            };
            SessionEndTime = SessionStartTime.Add(sessionDuration);
        }
    }
}
