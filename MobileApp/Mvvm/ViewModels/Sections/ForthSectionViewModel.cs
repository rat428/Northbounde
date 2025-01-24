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
    public partial class ForthSectionViewModel : ObservableObject
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



        public ICommand SignCommand { get; }
        public ICommand ResignCommand { get; }
        public ICommand SubmitSignatureCommand { get; }
        public ICommand DrawingLineCompletedCommand { get; }


        public DrawingView _signaturedrawingView;

        public ForthSectionViewModel()
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
            SignCommand = new RelayCommand(OnSign);
            ResignCommand = new RelayCommand(OnResign);
            SubmitSignatureCommand = new RelayCommand(OnSubmitSignature);
            DrawingLineCompletedCommand = new Command<IDrawingLine>(async (line) =>
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

                var stream = await line.GetImageStream(800, 800, Colors.Gray.AsPaint(), cts.Token);
                if (stream!=null)
                {
                    SignatureImage = ImageSource.FromStream(() => stream);
                }
            });
        }

        private void OnSubmitSignature()
        {

            if (SignatureImage!=null)
            {

            }
        }

        private void OnSign()
        {
            IsSignatureVisible = true;
            IsRelationshipVisible = true;
        }

        private void OnResign()
        {
            SignatureImage = null;
            IsSignatureVisible = false;
            IsRelationshipVisible = false; 
        }



        partial void OnSelectedRelationshipChanged(string? value)
        {
            IsOtherRelationshipVisible = value == "Other";
        }
    }
}
