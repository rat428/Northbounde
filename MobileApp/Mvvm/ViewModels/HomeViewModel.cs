using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.Helpers.Converters;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Mvvm.Models;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;
using Northboundei.Mobile.Mvvm.Views;
using Northboundei.Mobile.Mvvm.Views.Sections;
using Northboundei.Mobile.Services;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        [ObservableProperty]
        SettingsModel _settings;

        IPermissionService _permissionService;
        IServiceProvider _serviceProvider;
        INoteService _noteService;
        IChildService _childService;
        SyncViewModel _syncViewModel;

        private System.Timers.Timer _statusTimer;
        private bool _isBusy;

        [ObservableProperty]
        bool isHomeVisible = true;
        [ObservableProperty]
        bool _isStartVisible = true;
        [ObservableProperty]
        bool _isContinueVisible = true;
        [ObservableProperty]
        bool _isSyncVisible = true;

        [ObservableProperty]
        public string _lastSyncDate = "FETCHING....";


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

            
            _noteService = noteService;
            _childService = childService;
            _syncViewModel = _serviceProvider.GetService<SyncViewModel>()!;
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
        }

        private void _permissionService_AutoDateChanged(object? sender, bool? e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Settings.AutoDateTime = e;
            });
        }

        private void _permissionService_TimeZoneChanged(object? sender, bool? e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Settings.AutoDateTime = e;
            });
        }


        

        [RelayCommand]
        private void Home(object obj)
        {
            HideFlyoutAll();
            NavigateToShell("//ActionPage");
            IsStartVisible = true;
            IsContinueVisible = true;
            IsSyncVisible = true;
        }
        [RelayCommand]
        private void NotesPage(object obj)
        {
            HideFlyoutAll();
            NavigateToShell("//NotesPage");
        }
        [RelayCommand]
        private void SyncPage(object obj)
        {
            HideFlyoutAll();
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
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }

        public async Task PerformLogout()
        {
            await _syncViewModel.SyncNow();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                var viewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
                Application.Current.MainPage = new LoginPage(viewModel);
            });
        }

        private void HideFlyoutAll()
        {
            IsStartVisible = false;
            IsContinueVisible = false;
            IsSyncVisible = false;
        }

        private void NavigateToShell(string url,Dictionary<string, object>? parameters = null)
        {
            if (parameters == null)
                Shell.Current.GoToAsync(url);
            else
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
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Settings.GpsOn = gPSOn;
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
                Settings.GpsOn = e;
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

            if (Settings.GpsOn is false)
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

        [RelayCommand]
        private void Discard()
        {
            // TODO: Discard any NoteActionsToolbar changes
        }

        [RelayCommand]
        private async Task SaveDraftAsync()
        {
            // TODO: Save draft logic for NoteActionsToolbar
        }

        [RelayCommand]
        private async Task Submit()
        {
            // TODO: Validate NoteActionsToolbar fields then submit
        }

        [RelayCommand]
        private void SaveAsDraft()
        {
            // Logic to save the current state as a draft
        }

        // Add an indicator on each page to show the current stage
        private void AddStageIndicator()
        {
            // Logic to add an indicator on each page to show the current stage
        }
    }
}
