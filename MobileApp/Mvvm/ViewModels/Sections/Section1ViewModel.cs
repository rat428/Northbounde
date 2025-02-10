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
        string _sessionID;

        [ObservableProperty]
        ObservableCollection<ServiceAuthData> _childName;
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
        ServiceAuthData _selectedChild;

        [ObservableProperty]
        string _selectedServiceType;

        private readonly IServiceAuthService _childService;
        private readonly INoteService _noteService;

        List<SessionNoteData> _sessionData;
        public Section1ViewModel(IServiceAuthService childService, INoteService noteService) : base("Setup")
        {
            _childService = childService;
            _noteService = noteService;
            SessionDate = DateTime.Now;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await InitilizeData();
            });
        }

        public async Task InitilizeData()
        {
            try
            {
                IsBusy = true;

                ChildName = [.. await _childService.GetServiceAuthDataAsync()];
                _sessionData = [.. await _noteService.GetNotesAsync()];
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK!");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        void OnChildSelected()
        {
            var remaining = _selectedChild.UnitsAuthorized;
            List<string?> list = _sessionData.Where(y => y.EiNumber == _selectedChild.NyeisId).Select(x => x.ServiceType).Distinct().ToList();
            ServiceType = new ObservableCollection<string>(list);

            RemainingUnits = ChildName.Where(x => x.NyeisId == _selectedChild.NyeisId).Select(x => int.Parse(x.UnitsAuthorized??"0")).First();
        }
    }
}
