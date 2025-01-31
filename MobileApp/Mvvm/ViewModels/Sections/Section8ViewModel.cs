using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section8ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<CPTCodeRow> cptCodeRows;

        public Section8ViewModel() : base("CPT Codes")
        {
            CptCodeRows = new ObservableCollection<CPTCodeRow>
            {
                new CPTCodeRow
                {
                    CptCode = "97110 Therapeutic Procedure",
                    Units = 1,
                    AvailableCPTCodes = new List<AvailableUnit>
                    {
                        new AvailableUnit { Unitname = "97110 Therapeutic Procedure", Uid = 1 },
                        new AvailableUnit { Unitname = "97530 Therapeutic Activities", Uid = 2 },
                        new AvailableUnit { Unitname = "97012 Mechanical Traction", Uid = 3 }
                    },
                    AvailableUnits = new List<int> { 1, 2, 3, 4 }
                }
            };
        }

        [RelayCommand]
        private void AddRow(CPTCodeRow row = null)
        {
            CptCodeRows.Add(new CPTCodeRow
            {
                CptCode = "97110 Therapeutic Procedure",
                Units = 1,
                AvailableCPTCodes = new List<AvailableUnit>
                {
                    new AvailableUnit { Unitname = "97110 Therapeutic Procedure", Uid = 1 },
                    new AvailableUnit { Unitname = "97530 Therapeutic Activities", Uid = 2 },
                    new AvailableUnit { Unitname = "97012 Mechanical Traction", Uid = 3 }
                },
                AvailableUnits = new List<int> { 1, 2, 3, 4 }
            });

            OnPropertyChanged(nameof(CanRemoveRows));
        }

        [RelayCommand]
        private void RemoveRow(CPTCodeRow row)
        {
            if (CptCodeRows.Count > 1 && row != null)
            {
                CptCodeRows.Remove(row);
                OnPropertyChanged(nameof(CanRemoveRows));
            }
        }

        public bool CanRemoveRows => CptCodeRows.Count > 1;
    }

    public partial class CPTCodeRow : ObservableObject
    {
        [ObservableProperty]
        private string cptCode;

        [ObservableProperty]
        private int? units;

        [ObservableProperty]
        private List<AvailableUnit> availableCPTCodes;

        [ObservableProperty]
        private List<int> availableUnits; // Now inside CPTCodeRow
    }

    public partial class AvailableUnit : ObservableObject
    {
        [ObservableProperty]
        private string unitname;

        [ObservableProperty]
        private int? uid;
    }
}
