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
    public partial class FirstSectionViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<ServiceAuthResponse> _childName;
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
        ServiceAuthResponse _selectedChild;

        public ICommand OnChildSelectedCommand => new Command(OnChildSelected);


        List<SessionNoteResponse> _sessionData;
        public FirstSectionViewModel()
        {
        }

        public async Task InitilizeData()
        {
            //todo add activity loade, refactor
            string? secureChildrenData = await SecureStorage.Default.GetAsync(SessionManager.UserContext.EncryptionKey + nameof(IChildService));
            if (secureChildrenData is not null)
            {
                List<ServiceAuthResponse>? childrenData = JsonConvert.DeserializeObject<List<ServiceAuthResponse>>(secureChildrenData);
                _childName = new ObservableCollection<ServiceAuthResponse>(childrenData);
                _sessionDate = DateTime.Now;
            }

            string? secureNotes = await SecureStorage.Default.GetAsync(SessionManager.UserContext.EncryptionKey + nameof(INoteService));

            if (secureNotes is not null)
            {
                List<SessionNoteResponse> _sessionData = JsonConvert.DeserializeObject<List<SessionNoteResponse>>(secureNotes);
            }
        }

         void OnChildSelected()
        {
            List<string?> list = _sessionData.Where(y => y.EiNumber == _selectedChild.NyeisId).Select(x => x.ServiceType).Distinct().ToList();
            _serviceType = new ObservableCollection<string>(list);
        }
    }
}
