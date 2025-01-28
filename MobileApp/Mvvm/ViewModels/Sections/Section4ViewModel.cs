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

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section4ViewModel : SectionViewModelBase
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

        public Section4ViewModel()
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
        private void SubmitSignature()
        {

            if (SignatureImage != null)
            {

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

        partial void OnSelectedRelationshipChanged(string? value)
        {
            IsOtherRelationshipVisible = value == "Other";
        }
    }
}
