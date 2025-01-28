using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Mvvm.Models;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;
using Northboundei.Mobile.Mvvm.Views;
using Northboundei.Mobile.Services;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private SettingsModel _settings;
        IPermissionService _permissionService;
        IServiceProvider _serviceProvider;
        INoteService _noteService;
        IChildService _childService;

        private System.Timers.Timer _statusTimer;
        private bool _isBusy;

        [ObservableProperty]
        Section1ViewModel _Section1ViewModel;
        [ObservableProperty]
        Section2ViewModel _Section2ViewModel;
        [ObservableProperty]
        Section3ViewModel _Section3ViewModel;
        [ObservableProperty]
        Section4ViewModel _Section4ViewModel;
        [ObservableProperty]
        Section5ViewModel _Section5ViewModel;
        [ObservableProperty]
        Section6ViewModel _Section6ViewModel;
        [ObservableProperty]
        Section7ViewModel _Section7ViewModel;
        [ObservableProperty]
        Section8ViewModel _Section8ViewModel;
        [ObservableProperty]
        Section9ViewModel _Section9ViewModel;
        [ObservableProperty]
        Section10ViewModel _Section10ViewModel;
        [ObservableProperty]
        Section11ViewModel _Section11ViewModel;
        [ObservableProperty]
        Section12ViewModel _Section12ViewModel;
        [ObservableProperty]
        Section13ViewModel _Section13ViewModel;

        [ObservableProperty]
        bool isHomeVisible = true;
        //[ObservableProperty]
        bool _isStartVisible = true;
        [ObservableProperty]
        bool _isContinueVisible = true;
        [ObservableProperty]
        bool _isSyncVisible = true;

        [ObservableProperty]
        public string _lastSyncDate = "FETCHING....";

        public SettingsModel Settings { get => _settings; set => SetProperty(ref _settings, value); }
        public bool IsStartVisible
        {
            get => _isStartVisible;
            set => SetProperty(ref _isStartVisible, value);
        }

        public HomeViewModel(
            IPermissionService permissionService,
            INoteService noteService,
            IChildService childService,
            IServiceProvider serviceProvider)
        {
            _permissionService = permissionService;
            _serviceProvider = serviceProvider;
            _statusTimer = new System.Timers.Timer(1000); // Check every 5 seconds
            _statusTimer.Elapsed += (sender, e) => CheckStatus();
            _permissionService.AirplaneModeChanged += _permissionService_AirplaneModeChanged;
            _permissionService.GpsStatusChanged += _permissionService_GpsStatusChanged;
            _permissionService.TimeZoneChanged += _permissionService_TimeZoneChanged;
            _permissionService.AutoDateChanged += _permissionService_AutoDateChanged;
            _Section1ViewModel = new Section1ViewModel();
            _Section2ViewModel = new Section2ViewModel();
            _Section3ViewModel = new Section3ViewModel();
            _Section4ViewModel = new Section4ViewModel();
            _Section5ViewModel = new Section5ViewModel();
            _Section6ViewModel = new Section6ViewModel();
            _Section7ViewModel = new Section7ViewModel();
            _Section8ViewModel = new Section8ViewModel();
            _Section9ViewModel = new Section9ViewModel();
            _Section10ViewModel = new Section10ViewModel();
            _Section11ViewModel = new Section11ViewModel();
            _Section12ViewModel = new Section12ViewModel();
            _Section13ViewModel = new Section13ViewModel();
            _noteService = noteService;
            _childService = childService;
            InitializeData().ConfigureAwait(false);
        }

        private async Task InitializeData()
        {
            if (!SessionManager.IsNotesSync)
            {
                var notes = await _noteService.GetNotesAsync();
                await SecureStorage.Default.SetAsync(SessionManager.UserContext.EncryptionKey + nameof(INoteService), JsonConvert.SerializeObject(notes)).ConfigureAwait(false); ;
                SessionManager.IsNotesSync = true;
            }
            if (!SessionManager.IsAuthSync)
            {
                var children = await _childService.GetChildrenAsync();
                await SecureStorage.Default.SetAsync(SessionManager.UserContext.EncryptionKey + nameof(IChildService), JsonConvert.SerializeObject(children)).ConfigureAwait(false);
                SessionManager.IsAuthSync = true;
            }
            await _Section1ViewModel.InitilizeData();
        }

        private void _permissionService_AutoDateChanged(object? sender, bool? e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Settings.AutoDateTime = e;
            });
        }

        private void _permissionService_TimeZoneChanged(object? sender, bool? e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Settings.AutoDateTime = e;
            });
        }

        [RelayCommand]
        private void ShowSection(int Section)
        {
            HideMenuAll();
            switch (Section)
            {
                case 1:
                    Section1ViewModel.IsOpen = true;
                    break;
                case 2:
                    Section2ViewModel.IsOpen = true;
                    break;
                case 3:
                    Section3ViewModel.IsOpen = true;
                    break;
                case 4:
                    Section4ViewModel.IsOpen = true;
                    break;
                case 5:
                    Section5ViewModel.IsOpen = true;
                    break;
                case 6:
                    Section6ViewModel.IsOpen = true;
                    break;
                case 7:
                    Section7ViewModel.IsOpen = true;
                    break;
                case 8:
                    Section8ViewModel.IsOpen = true;
                    break;
                case 9:
                    Section9ViewModel.IsOpen = true;
                    break;
                case 10:
                    Section10ViewModel.IsOpen = true;
                    break;
                case 11:
                    Section11ViewModel.IsOpen = true;
                    break;
                case 12:
                    Section12ViewModel.IsOpen = true;
                    break;
                case 13:
                    Section13ViewModel.IsOpen = true;
                    break;
            }
        }

        [RelayCommand]
        private void Home(object obj)
        {
            HideMenuAll();
            NavigateToShell("//ActionPage");
            IsStartVisible = true;
            IsContinueVisible = true;
            IsSyncVisible = true;
        }
        [RelayCommand]
        private void NotesPage(object obj)
        {
            HideMenuAll();
            NavigateToShell("//NotesPage");
        }
        [RelayCommand]
        private void DraftPage(object obj)
        {
            HideMenuAll();
            NavigateToShell("//BlankPage2");
        }
        [RelayCommand]
        private void SyncPage(object obj)
        {
            HideMenuAll();
            NavigateToShell("//SyncPage");
        }
        [RelayCommand]
        private async void LogOut(object obj)
        {
            try
            {
                IEnumerable<UserEntity> users = await DatabaseService.GetAllDataAsync<UserEntity>();
                var userEntity = users.Last();
                userEntity.KeepMeLoggedIn = false;
                userEntity.IsLoggedIn = false;

                await DatabaseService.UpdateDataAsync(userEntity);

                await PerformLogout();
            }
            catch (Exception ex)
            {
                var xx = ex.Message;
                // add  logs
            }

        }

        public async Task PerformLogout()
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                var viewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
                Application.Current.MainPage = new LoginPage(viewModel);
            });
        }

        private void HideMenuAll()
        {
            IsStartVisible = false;
            IsContinueVisible = false;
            IsSyncVisible = false;
            Section1ViewModel.IsOpen = false;
            Section2ViewModel.IsOpen = false;
            Section3ViewModel.IsOpen = false;
            Section4ViewModel.IsOpen = false;
            Section5ViewModel.IsOpen = false;
            Section6ViewModel.IsOpen = false;
            Section7ViewModel.IsOpen = false;
            Section8ViewModel.IsOpen = false;
            Section9ViewModel.IsOpen = false;
            
        }

        private void NavigateToShell(string url,Dictionary<string, object>? parameters = null)
        {
            Shell.Current.GoToAsync(url, parameters);
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void CheckStatus()
        {
            if (_isBusy)
                return;

            _isBusy = true;
            try
            {
                var gPSOn = await _permissionService.IsGpsEnabled();
                var internetAccessAvailable = await _permissionService.IsInternetAvailable();
                var developerToolsEnabled = await _permissionService.IsDeveloperModeEnabled();
                var allowRunInBackground = await _permissionService.CanRunInBackground();
                var locationSharingAllowed = await _permissionService.IsLocationPermissionGrantedAsync();
                var airplane = await _permissionService.IsAirplaneModeEnabled();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    Settings.GPSOn = gPSOn;
                    Settings.InternetAccessAvailable = internetAccessAvailable;
                    Settings.DeveloperToolsEnabled = developerToolsEnabled;
                    Settings.AllowRunInBackground = allowRunInBackground;
                    Settings.LocationSharingAllowed = locationSharingAllowed;
                    Settings.AirplaneMode = airplane;
                });
                if (!IsValid())
                {
                    // Rerun the checks assuming the user has changed the settings
                    CheckStatus();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplaySnackbar(ex.Message);
            }

            _isBusy = false;
        }
        [RelayCommand]
        async Task UpdateLastSyncTime()
        {
            var SyncHistory = await DatabaseService.GetAllDataAsync<SyncRecord>();
            if (SyncHistory.Count() > 0)
            {
                _lastSyncDate = SyncHistory.Last().SyncDate.ToString();
            }
            else
            {
                _lastSyncDate = "Never";
            }
        }
        private void _permissionService_GpsStatusChanged(object? sender, bool? e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Settings.GPSOn = e;
            });
        }

        private void _permissionService_AirplaneModeChanged(object? sender, bool? e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Settings.AirplaneMode = e;
            });
        }
        [RelayCommand]
        private async void Load(object obj)
        {
        }

        public void StartTimer()
        {
            _statusTimer.Start();
        }
        public void StopTimer()
        {
            _statusTimer.Stop();
        }

        public bool IsValid()
        {
            if (Settings.LocationSharingAllowed is false)
                return false;

            if (Settings.AirplaneMode is true)
                return false;

            if (Settings.GPSOn is false)
                return false;

            if (Settings.DeveloperToolsEnabled is false)
                return false;


            if (Settings.AllowRunInBackground is false)
                return false;

            if (Settings.AutoDateTime is false)
                return false;

            if (Settings.AutoTimeZone is false)
                return false;

            return true;
        }

    }
}
