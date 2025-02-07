using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section8ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanRemoveRows))]
        [NotifyPropertyChangedFor(nameof(AvailableCodes))]
        private ObservableCollection<CPTCodeRow> cptCodeRows;

        List<CPTCode> AllCodes =>
        [
            new CPTCode { Code = "97110 Therapeutic Procedure", Uid = 1 },
            new CPTCode { Code = "97112 Neuromuscular Reeducation", Uid = 2 },
            new CPTCode { Code = "97129 Therapeutic Interventions", Uid = 3 },
            new CPTCode { Code = "97530 Therapeutic Activities", Uid = 4 },
            new CPTCode { Code = "97533 Sensory Integrative Techniques", Uid = 5 },
            new CPTCode { Code = "97535 Self-Care/Home Management Training", Uid = 6 },
            new CPTCode { Code = "T1027 Family Training and Counseling", Uid = 7 }
        ];

        public List<CPTCode> AvailableCodes => AllCodes.Except(cptCodeRows.Select(x => x.CptCode)).ToList();
        public bool CanRemoveRows => CptCodeRows.Count > 1;

        public Section8ViewModel() : base("CPT Codes")
        {
            CptCodeRows = new ObservableCollection<CPTCodeRow>
            {

            };
        }

        [RelayCommand]
        private void AddRow(CPTCodeRow row = null)
        {
            // if there are AvailableCodes, add a new row with the first available code
            if (AvailableCodes.Any())
            {
                CptCodeRows.Add(new CPTCodeRow
                {
                    CptCode = AvailableCodes.First(),
                    Units = 1
                });
            }
        }

        [RelayCommand]
        private void RemoveRow(CPTCodeRow row)
        {
            if (CptCodeRows.Count > 1 && row != null)
            {
                CptCodeRows.Remove(row);
            }
        }

    }

    public partial class CPTCodeRow : ObservableObject
    {
        [ObservableProperty]
        private CPTCode cptCode;

        [ObservableProperty]
        private int? units;
    }

    public partial class CPTCode : ObservableObject
    {
        [ObservableProperty]
        private string code;

        [ObservableProperty]
        private int? uid;
    }
}
