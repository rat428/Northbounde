using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class HomeViewModel : ObservableObject
    {


        private SettingsModel _settings;
        IPermissionService _permissionService;
        IServiceProvider _serviceProvider;
        INoteService _noteService;
        IChildService _childService;

        private System.Timers.Timer _statusTimer;
        private bool _isBusy;

        [ObservableProperty]
        FirstSectionViewModel _firstSectionViewModel;
        [ObservableProperty]
        SecondSectionViewModel _secondSectionViewModel;
        [ObservableProperty]
        bool isHomeVisible = true;
        //[ObservableProperty]
        bool _isStartVisible = true;
        [ObservableProperty]
        bool _isContinueVisible = true;
        [ObservableProperty]
        bool _isSyncVisible = true;


        [ObservableProperty]
        bool _isSection1Visible = false;
        [ObservableProperty]
        bool _isSection2Visible = false;
        [ObservableProperty]
        bool _isSection3Visible = false;
        [ObservableProperty]
        bool _isExpanded1Visible = false;
        [ObservableProperty]
        bool _isExpanded2Visible = false;
        [ObservableProperty]
        bool _isExpanded3Visible = false;

        public ICommand HomeCommand => new Command(HomeLoad);
        public ICommand LoadCommand => new Command(OnLoad);
        public ICommand LogoutCommand => new Command(OnLogOut);
        public ICommand NotesPageCommand => new Command(OnNotesPageCommand);
        public ICommand DraftPageCommand => new Command(OnDraftPageCommand);
        public ICommand SyncPageCommand => new Command(OnSyncPageCommand);
        public ICommand Section1Command => new Command(OnSection1Command);
        public ICommand Section2Command => new Command(OnSection2Command);
        public ICommand Section3Command => new Command(OnSection3Command);



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
            _firstSectionViewModel = new FirstSectionViewModel();
            _secondSectionViewModel = new SecondSectionViewModel();
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
            await _firstSectionViewModel.InitilizeData();
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

        private void OnSection1Command(object obj)
        {
            HideMenuAll();
            IsSection2Visible = true;
            IsExpanded2Visible = true;
        }

        private void OnSection2Command(object obj)
        {
            HideMenuAll();
            IsSection3Visible = true;
            IsExpanded3Visible = true;
        }

        private void OnSection3Command(object obj)
        {
            HideMenuAll();
        }

        private void HomeLoad(object obj)
        {
            HideMenuAll();
            NavigateToShell("//ActionPage");
            IsStartVisible = true;
            IsContinueVisible = true;
            IsSyncVisible = true;
        }
        private void OnNotesPageCommand(object obj)
        {
            HideMenuAll();
            NavigateToShell("//BlankPage1");
        }
        private void OnDraftPageCommand(object obj)
        {
            HideMenuAll();
            NavigateToShell("//BlankPage2");
        }
        private void OnSyncPageCommand(object obj)
        {
            HideMenuAll();
            NavigateToShell("//SyncPage");
        }
        private async void OnLogOut(object obj)
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
            _isSection1Visible = false;
            _isSection2Visible = false;
            _isSection3Visible = false;
        }

        private void NavigateToShell(string url)
        {
            Shell.Current.GoToAsync(url);
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

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplaySnackbar(ex.Message);
            }

            _isBusy = false;
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

        private async void OnLoad(object obj)
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
