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
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui.Devices.Sensors;
using Northboundei.Mobile.Services;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section5ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private string? signatureImage;

        [ObservableProperty]
        private bool isSignatureVisible;

        [ObservableProperty]
        private bool isRelationshipVisible;

        [ObservableProperty]
        private bool isOtherRelationship;

        [ObservableProperty]
        private ObservableCollection<string> relationshipOptions =
            [
                "Parent/Caregiver",
                "Teacher",
                "Other"
            ];

        [ObservableProperty]
        private ObservableCollection<IDrawingLine> _lines;

        [ObservableProperty]
        private string? selectedRelationship;

        [ObservableProperty]
        private string? otherRelationshipText;

        public DrawingView? _signatureDrawingView;

        [ObservableProperty]
        private string? _gpsLocation;

        [ObservableProperty]
        private DateTime? _signDateTime;

        public Section5ViewModel() : base("Attendance Signature")
        {
            IsSignatureVisible = false;
            IsRelationshipVisible = false;
            IsOtherRelationship = false;

            Lines = [];
        }

        [RelayCommand]
        private async Task SubmitSignature()
        {
            if (!IsSignatureVisible)
            {
                await Shell.Current.DisplayAlert("Missing Signature", "Please sign the form.", "OK");
                return;
            }
            GpsLocation = await GeoLocationService.GetGpsLocationAsync();
            SignDateTime = DateTime.Now;

            var stream = await _signatureDrawingView!.GetImageStream(800, 800);
            if (stream != null)
            {
                // Base64
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                SignatureImage = Convert.ToBase64String(memoryStream.ToArray());
            }
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
    }
}
