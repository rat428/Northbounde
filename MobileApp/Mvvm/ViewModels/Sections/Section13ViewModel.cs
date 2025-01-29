using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System.Linq;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls.Shapes;
using Northboundei.Mobile.Services;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section13ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private ImageSource? signatureImage;

        [ObservableProperty]
        private bool isSignatureVisible;

        [ObservableProperty]
        private bool isRelationshipVisible;

        [ObservableProperty]
        private bool isOtherRelationshipVisible;

        [ObservableProperty]
        private ObservableCollection<string> relationshipOptions;

        [ObservableProperty]
        private ObservableCollection<IDrawingLine> _lines;

        [ObservableProperty]
        private string? selectedRelationship;

        [ObservableProperty]
        private string? otherRelationshipText;

        public DrawingView _signaturedrawingView;

        public DateTime SessionEndTime { get; set; } = DateTime.Now.AddHours(1);

        [ObservableProperty]
        private ImageSource? _userSignatureImage;

        [ObservableProperty]
        private ObservableCollection<IDrawingLine> _userLines;

        [ObservableProperty]
        private bool _isUserSignatureVisible;

        [ObservableProperty]
        private string? _gpsLocation;

        [ObservableProperty]
        private DateTime? _signDateTime;

        public bool Complete {get; private set;}

        public Section13ViewModel()
        {
            IsSignatureVisible = false;
            IsRelationshipVisible = false;
            IsOtherRelationshipVisible = false;

            RelationshipOptions = new ObservableCollection<string>
            {
                "Parent/Caregiver",
                "Teacher",
                "Other"
            };
            Lines = new ObservableCollection<IDrawingLine>();
        }

        [RelayCommand]
        private async void SubmitSignature()
        {
            if (DateTime.Now < SessionEndTime) 
            {
                await Shell.Current.DisplayAlert("Session Not Ended", "You cannot submit the form until the session has ended.", "OK");
                return;
            }
            if (SignatureImage is null || UserSignatureImage is null)
            {
                await Shell.Current.DisplayAlert("Missing Signatures", "Please sign both the parent/caregiver and the teacher.", "OK");
                return;
            }
            GpsLocation = GpsLocation ?? await GeoLocationService.GetGpsLocationAsync();
            SignDateTime = SignDateTime ?? DateTime.Now;
        }

        private void SaveSignaturesAsync()
        {
            Complete = true;
        }

        [RelayCommand]
        private void Sign()
        {
            IsSignatureVisible = true;
            IsRelationshipVisible = true;
        }

        [RelayCommand]
        private void Resign()
        {
            SignatureImage = null;
            IsSignatureVisible = false;
            IsRelationshipVisible = false;
        }

        [RelayCommand]
        async Task DrawLineCompleted(IDrawingLine line)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var stream = await line.GetImageStream(800, 800, Colors.Gray.AsPaint(), cts.Token);
            if (stream != null)
            {
                SignatureImage = ImageSource.FromStream(() => stream);
            }
        }

        [RelayCommand]
        private void SignUser()
        {
            IsUserSignatureVisible = true;
        }

        [RelayCommand]
        private void ResignUser()
        {
            UserSignatureImage = null;
            IsUserSignatureVisible = false;
        }

        [RelayCommand]
        async Task DrawLineCompletedUser(IDrawingLine line)
        {
            var stream = await line.GetImageStream(800, 800, Colors.Gray.AsPaint(), CancellationToken.None);
            if (stream != null)
            {
                UserSignatureImage = ImageSource.FromStream(() => stream);
            }
        }

        partial void OnSelectedRelationshipChanged(string? value)
        {
            IsOtherRelationshipVisible = value == "Other";
        }

        partial void OnUserSignatureImageChanged(ImageSource? value)
        {
            SaveIfBothSigned();
        }

        partial void OnSignatureImageChanged(ImageSource? value)
        {
            SaveIfBothSigned();
        }

        private async void SaveIfBothSigned()
        {
            if (UserSignatureImage != null && SignatureImage != null)
            {
                GpsLocation = await GeoLocationService.GetGpsLocationAsync();
                SignDateTime = DateTime.Now;
                SaveSignaturesAsync();
            }
        }
    }
}