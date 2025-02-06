using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section1ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        ObservableCollection<ChildDataResponse> _childName;
        [ObservableProperty]
        DateTime _sessionDate;
        [ObservableProperty]
        ObservableCollection<string> _serviceType;
        [ObservableProperty]
        int _remainingUnits;
        [ObservableProperty]
        int _remainingUnitsWeek;
        [ObservableProperty]
        double childAge;

        [ObservableProperty]
        ChildDataResponse _selectedChild;

        [ObservableProperty]
        string _selectedServiceType;
        public ICommand OnChildSelectedCommand => new Command(OnChildSelected);


        List<SessionNoteData> _sessionData;
        public Section1ViewModel() : base("Setup")
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await InitilizeData();
            });
        }

        public async Task InitilizeData()
        {
            await LoadChildrenData();
            await LoadSessionData();
        }

        private async Task LoadChildrenData()
        {
            string? secureChildrenData = await SecureStorage.Default.GetAsync(SessionManager.UserContext.EncryptionKey + nameof(IChildService));
            if (secureChildrenData is not null)
            {
                List<ChildDataResponse>? childrenData = JsonConvert.DeserializeObject<List<ChildDataResponse>>(secureChildrenData);
                ChildName = new ObservableCollection<ChildDataResponse>(childrenData);
                SessionDate = DateTime.Now;
            }
        }

        private async Task LoadSessionData()
        {
            string? secureNotes = await SecureStorage.Default.GetAsync(SessionManager.UserContext.EncryptionKey + nameof(INoteService));
            if (secureNotes is not null)
            {
                _sessionData = JsonConvert.DeserializeObject<List<SessionNoteData>>(secureNotes);
            }
        }

        void OnChildSelected()
        {
            var remaining = _selectedChild.UnitsAuthorized;
            List<string?> list = _sessionData.Where(y => y.EiNumber == _selectedChild.NyeisId).Select(x => x.ServiceType).Distinct().ToList();
            ServiceType = new ObservableCollection<string>(list);
        }
    }
}
