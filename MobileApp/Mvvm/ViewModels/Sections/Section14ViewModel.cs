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
    public partial class Section14ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private string? signatureImage;

        [ObservableProperty]
        private bool isSignatureVisible;

        [ObservableProperty]
        private ObservableCollection<IDrawingLine> _lines;


        public DrawingView _signaturedrawingView;

        public DateTime SessionEndTime { get; set; } = DateTime.Now.AddHours(1);

        [ObservableProperty]
        private string? _gpsLocation;

        [ObservableProperty]
        private DateTime? _signDateTime;

        public Section14ViewModel() : base("Session Signature")
        {
            IsSignatureVisible = false;

            Lines = [];
        }

        [RelayCommand]
        private async void SubmitSignature()
        {
            if (!IsSignatureVisible)
            {
                await Shell.Current.DisplayAlert("Missing Signature", "Please sign the form.", "OK");
                return;
            }
            GpsLocation = await GeoLocationService.GetGpsLocationAsync();
            SignDateTime = DateTime.Now;
            SessionEndTime = DateTime.Now;

            var stream = await _signaturedrawingView.GetImageStream(800, 800);
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
        }

        [RelayCommand]
        private void Resign()
        {
            IsSignatureVisible = false;
            SignatureImage = null;
            _signaturedrawingView.Clear();
        }
    }
}