using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;
using Northboundei.Mobile.Mvvm.Views.Sections;
using Northboundei.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public partial class NotesViewModel : BaseViewModel
    {
        // ViewModels
        [ObservableProperty]
        Section1ViewModel _Section1ViewModel;
        [ObservableProperty]
        Section2ViewModel _Section2ViewModel;
        [ObservableProperty]
        Section3CoVisitViewModel _Section3CoVisitViewModel;
        [ObservableProperty]
        Section3MakeUpViewModel _Section3MakeUpViewModel;
        [ObservableProperty]
        Section3MissedViewModel _Section3MissedViewModel;
        [ObservableProperty]
        Section4ViewModel _Section4ViewModel;
        [ObservableProperty]
        Section5ViewModel _Section5ViewModel;
        [ObservableProperty]
        Section6ViewModel _Section6ViewModel;
        [ObservableProperty]
        Section7ViewModel _Section7ViewModel;
        [ObservableProperty]
        Section8ViewModel _Section8ViewModel;
        [ObservableProperty]
        Section9ViewModel _Section9ViewModel;
        [ObservableProperty]
        Section10ViewModel _Section10ViewModel;
        [ObservableProperty]
        Section11ViewModel _Section11ViewModel;
        [ObservableProperty]
        Section12ViewModel _Section12ViewModel;
        [ObservableProperty]
        Section13ViewModel _Section13ViewModel;
        [ObservableProperty]
        Section14ViewModel _Section14ViewModel;


        [ObservableProperty]
        ContentView currentSectionPage;

        [ObservableProperty]
        private int _currentSection;
        public NotesViewModel()
        {
            InitializeViewModels();
            ShowSection(1);
        }

        public void InitializeViewModels()
        {
            _Section1ViewModel = new Section1ViewModel();
            _Section2ViewModel = new Section2ViewModel();

            _Section3CoVisitViewModel = new Section3CoVisitViewModel();
            _Section3MakeUpViewModel = new Section3MakeUpViewModel();
            _Section3MissedViewModel = new Section3MissedViewModel();

            _Section4ViewModel = new Section4ViewModel();
            _Section5ViewModel = new Section5ViewModel();
            _Section6ViewModel = new Section6ViewModel();
            _Section7ViewModel = new Section7ViewModel();
            _Section8ViewModel = new Section8ViewModel();
            _Section9ViewModel = new Section9ViewModel();
            _Section10ViewModel = new Section10ViewModel();
            _Section11ViewModel = new Section11ViewModel();
            _Section12ViewModel = new Section12ViewModel();
            _Section13ViewModel = new Section13ViewModel();
            _Section14ViewModel = new Section14ViewModel();
        }


        [RelayCommand]
        private void ShowSection(int Section)
        {
            CurrentSection = Section;
            var Page = IntToSection(Section);
            CurrentSectionPage = Page;
        }

        // Int to Section converter
        public ContentView IntToSection(int SectionID)
        {
            return SectionID switch
            {
                1 => new Section1(_Section1ViewModel),
                2 => new Section2(_Section2ViewModel),
                3 => (
                    // Depending on the Section2 AttendanceType, return the appropriate view and viewmodel
                    _Section2ViewModel.SelectedAttendanceType switch
                    {
                        "Covisit" => new Section3CoVisit(_Section3CoVisitViewModel),
                        "Make-up" => new Section3MakeUp(_Section3MakeUpViewModel),
                        "Missed" => new Section3Missed(_Section3MissedViewModel),
                        // Set CurrentSection to 4 then return the appropriate view and viewmodel
                        // Depends On:
                        // - Section2ViewModel.SelectedAttendanceType
                        // - CurrentSection
                        _ =>
                         CurrentSection switch
                         {
                             2 => Enumerable.Range(0, 1).Select((i) =>
                             {
                                 CurrentSection = 4;
                                 return new Section4(_Section4ViewModel);
                             }).ElementAt(0),
                             _ => new Section2(_Section2ViewModel)
                         }
                    }
                ),
                4 => new Section4(_Section4ViewModel),
                // Skip Section 5 if Telehealth is selected in Section 4 and return the appropriate view and viewmodel
                // Depends On:
                // - Section4ViewModel.IsTelehealthSelected
                // - CurrentSection
                5 => !_Section4ViewModel.IsTelehealthSelected ? new Section5(_Section5ViewModel) : CurrentSection switch
                {
                    4 => Enumerable.Range(0, 1).Select((i) =>
                    {
                        CurrentSection = 6;
                        return new Section6(_Section6ViewModel);
                    }).ElementAt(0),
                    _ => new Section4(_Section4ViewModel)
                },
                6 => new Section6(_Section6ViewModel),
                7 => new Section7(_Section7ViewModel),
                8 => new Section8(_Section8ViewModel),
                9 => new Section9(_Section9ViewModel),
                10 => new Section10(_Section10ViewModel),
                11 => new Section11(_Section11ViewModel),
                12 => new Section12(_Section12ViewModel),
                13 => new Section13(_Section13ViewModel),
                14 => new Section14(_Section14ViewModel),
                _ => new Section1(_Section1ViewModel),
            };
        }

        // Get Invalid or Incomplete Sections
        List<int> GetInvalidSections()
        {
            var allSections = EnumerateSectionVMs();
            var InvalidSections = new List<int>();
            for (int i = 0; i < allSections.Length; i++)
            {
                if (!allSections[i].Complete)
                {
                    InvalidSections.Add(i + 1);
                }
            }

            return InvalidSections;
        }
        [ObservableProperty]
        string _nextButtonText = "Next";
        [ObservableProperty]
        string _previousButtonText = "Cancel";

        [RelayCommand]
        private async Task NextSection()
        {
            // if the current section is 13, submit the form
            if (CurrentSection == 13)
            {
                await SubmitForm();
            }
            // if the current section is less than 13, show the next section
            else if (CurrentSection < 13)
            {
                ShowSection(CurrentSection + 1);
            }

            // Update the next button text
            if (CurrentSection == 13)
            {
                NextButtonText = "Submit Final";
            }
            else
            {
                NextButtonText = "Next";
                PreviousButtonText = "Previous";
            }


        }

        [RelayCommand]
        private async Task PreviousSection()
        {
            // if the current section is 1, cancel the form
            // Update the previous button text
            if (CurrentSection == 1)
            {
                // Ask the user if they want to cancel the form
                bool cancel = await Shell.Current.DisplayAlert("Cancel Form", "Are you sure you want to cancel the form?\nAll data will be lost.", "Yes", "No");

                // if the user wants to cancel the form, navigate back to the previous page
                if (cancel)
                {
                    InitializeViewModels();
                    await Shell.Current.GoToAsync("..");
                }
                return;
            }
            else if (CurrentSection > 1)
            {
                PreviousButtonText = "Previous";
                ShowSection(CurrentSection - 1);
            }
            else
            {
                PreviousButtonText = "Cancel";
            }



        }
        public SectionViewModelBase[] EnumerateSectionVMs()
        {
            return
            [
                _Section1ViewModel,
                _Section2ViewModel,
                _Section2ViewModel.SelectedAttendanceType switch
                {
                    "Covisit" => _Section3CoVisitViewModel,
                    "Make-up" => _Section3MakeUpViewModel,
                    "Missed" => _Section3MissedViewModel,
                    _ => _Section3CoVisitViewModel,
                },
                _Section5ViewModel,
                _Section6ViewModel,
                _Section7ViewModel,
                _Section8ViewModel,
                _Section9ViewModel,
                _Section10ViewModel,
                _Section11ViewModel,
                _Section12ViewModel,
                _Section13ViewModel,
                _Section14ViewModel
            ];
        }

        // Submit the form
        [RelayCommand]
        private async Task SubmitForm()
        {
            // If there are invalid sections, inform the user and navigate to the first invalid section
            var invalidSections = GetInvalidSections();
            if (invalidSections.Count > 0)
            {
                await Shell.Current.DisplayAlert("Invalid Sections",
                    "Please ensure all sections are complete and valid before submitting the form.\n" +
                    "The following sections are incomplete or have errors: " + string.Join("\n-", invalidSections.Select(section => $"Section {section}: {((SectionViewModelBase)IntToSection(section).BindingContext).SectionTitle}")),
                    "Goto Section " + invalidSections[0]);
                ShowSection(invalidSections[0]);
            }
            else
            {
                // Ask the user if they want to submit the form
                bool submit = await Shell.Current.DisplayAlert("Submit Form", "Are you sure you want to submit the form?", "Yes", "No");
                // If the user wants to submit the form, navigate back to the previous page
                if (submit)
                {
                    // Submit the form

                    // TODO: Submit the form data

                    await SubmitFormAsync();
                    await Shell.Current.GoToAsync("..");

                    await Shell.Current.DisplayAlert("Form Submitted", "The form has been submitted successfully.", "OK");
                    InitializeViewModels();
                }
            }
        }

        public SessionNoteData SessionNoteData(bool isDraft = false)
        {
            var sessionNoteData = new SessionNoteData
            {
                SessionId = $"{_Section1ViewModel.SelectedChild.NyeisId}-{_Section4ViewModel.ServiceLocations}",
                SessionNoteType = "App",
                TherapistImg = _Section14ViewModel.SignatureImage,
                SessionCancelled = _Section2ViewModel.IsMissed ? "Yes" : "No",
                EiNumber = _Section1ViewModel.SelectedChild.NyeisId,
                MakeUpSession = _Section2ViewModel.IsMakeUp ? "Yes" : "No",
                CoVisitPresent = _Section2ViewModel.IsCoVisit ? _Section3CoVisitViewModel.CovisitWhoElse : null,
                TherapistDate = _Section14ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt"),
                TherapistGps = _Section14ViewModel.GpsLocation,
            };

            sessionNoteData.SessionSection = sessionNoteData.AttendanceType = _Section2ViewModel.SelectedAttendanceType switch
            {
                "Attended" => "CV1",
                "Make-up" => "CV2",
                "Covisit" => "CV3",
                "Missed" => "Missed",
                _ => "",
            };

            sessionNoteData.Describe = _Section11ViewModel.SessionDescription;
            sessionNoteData.Relationship = _Section5ViewModel.IsOtherRelationship ? _Section5ViewModel.OtherRelationshipText : _Section5ViewModel.SelectedRelationship;
            sessionNoteData.ParentImg = _Section5ViewModel.SignatureImage;
            sessionNoteData.ServiceType = _Section1ViewModel.SelectedServiceType;
            sessionNoteData.SessionDate = DateOnly.FromDateTime(_Section1ViewModel.SessionDate);
            sessionNoteData.SessionMadeupBy = _Section2ViewModel.IsMissed ? DateOnly.FromDateTime(_Section1ViewModel.SessionDate).AddDays(14).ToString() : null;
            sessionNoteData.TherapistTimeStamp = _Section14ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt");

            sessionNoteData.ParentTimeStamp = _Section5ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt");
            sessionNoteData.ParentDate = _Section5ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt");
            sessionNoteData.ParentGps = _Section5ViewModel.GpsLocation;
            sessionNoteData.License = SessionManager.UserInfo.LicenseNo;
            sessionNoteData.DateNoteWritten = DateOnly.FromDateTime(DateTime.Now);
            sessionNoteData.Section6 = _Section13ViewModel.CarryOverDescription;
            sessionNoteData.TimeTo = TimeOnly.FromDateTime(_Section14ViewModel.SessionEndTime);
            sessionNoteData.MakeUpFor = _Section2ViewModel.IsMakeUp ? _Section3MakeUpViewModel.SelectedMakeUpSession.ToString("yyyy-MM-dd") : null;
            sessionNoteData.ServiceLogGps = _Section14ViewModel.GpsLocation;
            sessionNoteData.ServiceLogImg = _Section14ViewModel.SignatureImage;
            sessionNoteData.ServiceLogTimestamp = _Section14ViewModel.SignDateTime;
            sessionNoteData.HowWork = _Section12ViewModel.SelectedStrategies;
            sessionNoteData.HowWorkOther = _Section12ViewModel.OtherDescription;
            sessionNoteData.Npi = SessionManager.UserInfo.NpiNo;
            sessionNoteData.ServiceLocation = _Section4ViewModel.IsOtherLocationSelected ? _Section4ViewModel.OtherLocation : _Section4ViewModel.SelectedServiceLocation;
            sessionNoteData.DraftFinal = isDraft ? "Draft" : "Final";

            return sessionNoteData;
        }

        private Task SubmitFormAsync()
        {
            throw new NotImplementedException();
        }

        // Reset Form and Go To Home
        [RelayCommand]
        private async Task ResetForm()
        {
            // Ask the user if they want to reset the form
            bool reset = await Shell.Current.DisplayAlert("Reset Form", "Are you sure you want to reset the form?\nAll data will be lost.", "Yes", "No");
            // if the user wants to reset the form, navigate back to the previous page
            if (reset)
            {
                InitializeViewModels();
                await Shell.Current.GoToAsync("..");
            }
        }
    }

}
