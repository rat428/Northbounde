using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
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
    [QueryProperty("Draft", nameof(Draft))]
    public partial class NotesViewModel : BaseViewModel
    {
        SessionNoteData Draft { set => LoadDraft(value); }

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
        private int _currentSectionIndex;

        [ObservableProperty]
        string _nextButtonText = "Next";
        [ObservableProperty]
        string _previousButtonText = "Cancel";

        [ObservableProperty]
        bool _canSaveDraft = true;

        private readonly IServiceAuthService childService;
        private readonly INoteService noteService;

        
        public NotesViewModel(IServiceAuthService childService, INoteService noteService)
        {
            this.childService = childService;
            this.noteService = noteService;
            InitializeViewModels();
            ShowSection(0);
        }

        public void InitializeViewModels()
        {
            Section1ViewModel = new Section1ViewModel(childService, noteService);
            Section2ViewModel = new Section2ViewModel();

            Section3CoVisitViewModel = new Section3CoVisitViewModel();
            Section3MakeUpViewModel = new Section3MakeUpViewModel();
            Section3MissedViewModel = new Section3MissedViewModel();

            Section4ViewModel = new Section4ViewModel();
            Section5ViewModel = new Section5ViewModel();
            Section6ViewModel = new Section6ViewModel();
            Section7ViewModel = new Section7ViewModel();
            Section8ViewModel = new Section8ViewModel();
            Section9ViewModel = new Section9ViewModel();
            Section10ViewModel = new Section10ViewModel();
            Section11ViewModel = new Section11ViewModel();
            Section12ViewModel = new Section12ViewModel();
            Section13ViewModel = new Section13ViewModel();
            Section14ViewModel = new Section14ViewModel();
        }


        [RelayCommand]
        private void ShowSection(int SectionIndex)
        {
            CurrentSectionIndex = SectionIndex;
            var Page = IndexToSection(SectionIndex);
            CurrentSectionPage = Page;
        }

        // Int to Section converter
        public ContentView IndexToSection(int CurrentStage)
        {
            return EnumerateSections()[CurrentStage];
        }

        // Get Invalid or Incomplete Sections
        List<int> GetInvalidSections()
        {
            var allSections = EnumerateSectionVMs();
            var InvalidSections = allSections.Where(x => !x.Complete).Select(x => allSections.ToList().IndexOf(x)).ToList();
            return [.. InvalidSections];
        }


        [RelayCommand]
        private async Task NextSection()
        {
            // if the current section is the last, submit the form
            if (CurrentSectionIndex == EnumerateSections().Length - 1)
            {
                await SubmitForm();
            }
            // if the current section is not the last, show the next section
            else if (CurrentSectionIndex < EnumerateSections().Length - 1)
            {
                ShowSection(CurrentSectionIndex + 1);
            }

            // Update the next button text if the current section is the last
            if (CurrentSectionIndex == EnumerateSections().Length - 1)
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
            // if the current section is the first, cancel the form
            // Update the previous button text
            if (CurrentSectionIndex == 0)
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
            // if the current section is not the first, show the previous section
            else if (CurrentSectionIndex > 0)
            {
                ShowSection(CurrentSectionIndex - 1);
                PreviousButtonText = "Previous";
                // Update the next button text if the current section is the first
                if (CurrentSectionIndex == 0)
                {
                    PreviousButtonText = "Cancel";
                }
            }
        }
        public ContentView[] EnumerateSections()
        {
            List<ContentView> sections =
            [
                new Section1(Section1ViewModel),
                new Section2(Section2ViewModel)
            ];
            switch (Section2ViewModel.SelectedAttendanceType)
            {
                case "Covisit":
                    sections.Add(new Section3CoVisit(Section3CoVisitViewModel));
                    break;
                case "Make-up":
                    sections.Add(new Section3MakeUp(Section3MakeUpViewModel));
                    break;
                case "Missed":
                    sections.Add(new Section3Missed(Section3MissedViewModel));
                    break;
                default:
                    break;
            }
            if (Section2ViewModel.SelectedAttendanceType != "Missed")
            {
                sections.Add(new Section4(Section4ViewModel));
                if (!Section4ViewModel.IsTelehealthSelected)
                {
                    sections.Add(new Section5(Section5ViewModel));
                }

                sections.Add(new Section6(Section6ViewModel));
                sections.Add(new Section7(Section7ViewModel));
                sections.Add(new Section8(Section8ViewModel));
                sections.Add(new Section9(Section9ViewModel));
                sections.Add(new Section10(Section10ViewModel));
                sections.Add(new Section11(Section11ViewModel));
                sections.Add(new Section12(Section12ViewModel));
                sections.Add(new Section13(Section13ViewModel));
            }

            sections.Add(new Section14(Section14ViewModel));

            return sections.ToArray();
        }
        public SectionViewModelBase[] EnumerateSectionVMs()
        {
            return EnumerateSections().Select(section => (SectionViewModelBase)section.BindingContext).ToArray();
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
                    "The following sections are incomplete or have errors: " + string.Join("\n-", invalidSections.Select(section => $"Section {section}: {((SectionViewModelBase)IndexToSection(section).BindingContext).SectionTitle}")),
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

        public void LoadDraft(SessionNoteData draft)
        {
            Section1ViewModel.SelectedChild = Section1ViewModel.ChildName.FirstOrDefault(x => x.NyeisId == draft.EiNumber);
            Section1ViewModel.SessionDate = DateTime.Parse(draft.SessionDate, System.Globalization.CultureInfo.InvariantCulture);
            Section1ViewModel.SelectedServiceType = draft.ServiceType;
            Section2ViewModel.SelectedAttendanceType = draft.SessionSection switch
            {
                "CV1" => "Attended",
                "CV2" => "Make-up",
                "CV3" => "Covisit",
                "Missed" => "Missed",
                _ => "",
            };
            Section3CoVisitViewModel.CovisitWhoElse = draft.CoVisitPresent;
            Section3MakeUpViewModel.SelectedMakeUpSession = draft.MakeUpFor;
            //Section3MissedViewModel.SelectedMissedReason = draft.Miss;
            Section4ViewModel.SelectedServiceLocation = draft.ServiceLocation;
            Section4ViewModel.SelectedServiceLocation = draft.ServiceLocation;
            Section4ViewModel.OtherLocation = draft.ServiceLocation;
            Section5ViewModel.SelectedRelationship = draft.Relationship;
            Section5ViewModel.OtherRelationshipText = draft.Relationship;
            Section5ViewModel.IsOtherRelationship = draft.Relationship == "Other";
            //Section6ViewModel.SessionStartTime = draft.TimeSpent;
            //Section10ViewModel.OtherDescription = draft.HowWorkOther;
            Section11ViewModel.SessionDescription = draft.Describe;
            //Section12ViewModel.SelectedStrategies = draft.HowWork;
            Section12ViewModel.OtherDescription = draft.HowWorkOther;
            Section13ViewModel.CarryOverDescription = draft.Section6;
            Section14ViewModel.SignatureImage = draft.TherapistImg;
            Section14ViewModel.SignDateTime = DateTime.Parse(draft.TherapistTimeStamp);
            Section14ViewModel.GpsLocation = draft.TherapistGps;
        }

        public SessionNoteData ToDataObject(bool isDraft = false)
        {
            var sessionNoteData = new SessionNoteData
            {
                SessionId = $"{_Section1ViewModel.SelectedChild.NyeisId}-{_Section4ViewModel.ServiceLocations}",
                SessionNoteType = "App",
                TherapistImg = Section14ViewModel.SignatureImage,
                SessionCancelled = Section2ViewModel.IsMissed ? "Yes" : "No",
                EiNumber = Section1ViewModel.SelectedChild.NyeisId,
                MakeUpSession = Section2ViewModel.IsMakeUp ? "Yes" : "No",
                CoVisitPresent = Section2ViewModel.IsCoVisit ? Section3CoVisitViewModel.CovisitWhoElse : null,
                TherapistDate = Section14ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt"),
                TherapistTimeStamp = Section14ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt"),
                TherapistGps = Section14ViewModel.GpsLocation,
                DraftFinal = isDraft ? "Draft" : "Final",
                Describe = Section11ViewModel.SessionDescription,
            };

            sessionNoteData.SessionSection = sessionNoteData.AttendanceType = Section2ViewModel.SelectedAttendanceType switch
            {
                "Attended" => "CV1",
                "Make-up" => "CV2",
                "Covisit" => "CV3",
                "Missed" => "Missed",
                 _ => "",
            };

            if (sessionNoteData.SessionCancelled == "No")
            {
                sessionNoteData.Relationship = Section5ViewModel.IsOtherRelationship ? Section5ViewModel.OtherRelationshipText : Section5ViewModel.SelectedRelationship;
                sessionNoteData.ParentImg = Section5ViewModel.SignatureImage;
                sessionNoteData.ServiceType = Section1ViewModel.SelectedServiceType;
                sessionNoteData.SessionDate = Section1ViewModel.SessionDate.ToString("yyyy-MM-dd");
                sessionNoteData.SessionMadeupBy = Section2ViewModel.IsMissed ? Section1ViewModel.SessionDate.AddDays(14).ToString("yyyy-MM-dd") : null;
                sessionNoteData.ParentTimeStamp = Section5ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt");
                sessionNoteData.ParentDate = Section5ViewModel.SignDateTime?.ToString("MM-dd-yyyy HH:mm:ss tt");
                sessionNoteData.ParentGps = Section5ViewModel.GpsLocation;
                sessionNoteData.License = SessionManager.UserInfo.LicenseNo;
                sessionNoteData.DateNoteWritten = DateTime.Now.ToString("yyyy-MM-dd");
                sessionNoteData.Section6 = Section13ViewModel.CarryOverDescription;
                sessionNoteData.TimeTo = Section14ViewModel.SessionEndTime.ToString("HH:mm");
                sessionNoteData.MakeUpFor = Section2ViewModel.IsMakeUp ? Section3MakeUpViewModel.SelectedMakeUpSession : null;
                sessionNoteData.ServiceLogGps = Section14ViewModel.GpsLocation;
                sessionNoteData.ServiceLogImg = Section14ViewModel.SignatureImage;
                sessionNoteData.ServiceLogTimestamp = Section14ViewModel.SignDateTime;
                sessionNoteData.HowWork = Section12ViewModel.SelectedStrategies;
                sessionNoteData.HowWorkOther = Section12ViewModel.OtherDescription;
                sessionNoteData.Npi = SessionManager.UserInfo.NpiNo;
                sessionNoteData.ServiceLocation = Section4ViewModel.IsOtherLocationSelected ? Section4ViewModel.OtherLocation : Section4ViewModel.SelectedServiceLocation;
            }

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
