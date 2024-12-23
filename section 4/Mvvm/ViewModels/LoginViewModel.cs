#if ANDROID
using Android.Window;
#endif
using CommunityToolkit.Mvvm.ComponentModel;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Services;
using Refit;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        IPermissionService _locationPermissionService;
        ISettingsService _settingsService;
        IServiceProvider _serviceProvider;
        HomeViewModel _homeViewModel;

        [ObservableProperty]
        bool _isLoading;

        private IList<string> _errors = new List<string>();
        private string _username;
        private string _password;
        private bool _keepMeLogged;
        public ICommand LoginCommand { get; }
        public ICommand LoadCommand { get; }
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public bool KeepMeLogged { get => _keepMeLogged; set => SetProperty(ref _keepMeLogged, value); }

        public LoginViewModel(
            INavigationService navigationService,
            IUserService userService,
            IPermissionService locationPermissionService,
            ISettingsService settingsService,
            IServiceProvider serviceProvider)
        {
            _navigationService = navigationService;
            _userService = userService;
            _settingsService = settingsService;
            _serviceProvider = serviceProvider;
            _locationPermissionService = locationPermissionService;
            _homeViewModel = _serviceProvider.GetService<HomeViewModel>();

            LoginCommand = new Command(OnLogin);
        }

        private async Task RequestPermissions()
        {
            if (_homeViewModel.Settings.LocationSharingAllowed is false)
            {
                await App.Current.MainPage.DisplayAlert("Sharing Location must be enabled", "", "OK");
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    PermissionStatus locationInUser = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    PermissionStatus locationAlways = await Permissions.RequestAsync<Permissions.LocationAlways>();
                });
            }

            if (_homeViewModel.Settings.AirplaneMode is true)
            {
                await App.Current.MainPage.DisplayAlert("", "Airplane mode must be turned off", "Ok");
                await _settingsService.OpenAirplaneModeSettings();
            }

            if (_homeViewModel.Settings.GPSOn is false)
            {
                await App.Current.MainPage.DisplayAlert("", "Gps must be turned on", "OK");
                await _settingsService.CurrentAppSettingsAsync();
            }

            if (_homeViewModel.Settings.DeveloperToolsEnabled is false)
            {
                await App.Current.MainPage.DisplayAlert("", "Developer mode must be turned off", "OK");
                await _settingsService.CurrentAppSettingsAsync();
            }

            if (_homeViewModel.Settings.AllowRunInBackground is false)
            {
                await App.Current.MainPage.DisplayAlert("", "Running service in background must be turned On", "OK");
                await _settingsService.CurrentAppSettingsAsync();
            }

            if (_homeViewModel.Settings.AutoTimeZone is false || _homeViewModel.Settings.AutoDateTime is false)
            {
                await App.Current.MainPage.DisplayAlert("", "Timezone auto must be turned on", "OK");
                await _settingsService.CurrentAppSettingsAsync();
            }

        }
        private async void OnLogin()
        {
            IsLoading= true;
            try
            {
                var login = new LoginRequest
                {
                    Username = Username,
                    Password = Password
                };

                if (!_homeViewModel!.IsValid())
                {
                    await RequestPermissions();
                    await App.Current.MainPage.DisplayAlert("Permission", "You need to meet all permission requirements", "OK");

                    return;
                }

                LoginResponse client = await _userService.LoginAsync(login);

                if (!await CheckIfAnotherUserLoggedInBefore(login.Username))
                {
                    if (!await App.Current.MainPage.DisplayAlert("A different user detected", "Continuing with login will delete all previous data", "OK", "Cancel"))
                        return;
                }


                await StoreUserAsync(client);

                App.Current.MainPage = _serviceProvider.GetService<AppShell>();

            }
            catch (ApiException ex)
            {
                await App.Current.MainPage.DisplayAlert("api", "Username / Password combination is either Incorrect or Expired", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("api", ex.Message, "OK");
            }
            IsLoading = false;

        }

        private async Task<bool> CheckIfAnotherUserLoggedInBefore(string username)
        {
            IEnumerable<UserEntity> users = await DatabaseService.GetAllDataAsync<UserEntity>();
            if (users.Any())
            {
                var lastLoggedInUser = users.OrderByDescending(x => x.CreatedAt).First();
                if (!lastLoggedInUser.UserName.Equals(username))
                {
                    return false;
                }
            }
            return true;
        }

        private async Task StoreUserAsync(LoginResponse user)
        {
            var userEntity = new UserEntity();
            userEntity.UserName = user.Username;
            userEntity.Password = Password;
            userEntity.Token = user.Token;
            userEntity.CreatedAt = DateTime.Now;
            userEntity.ExpirationTime = user.ExpirationDate;
            userEntity.DeviceInfo = DeviceInfo.Name;
            userEntity.KeepMeLoggedIn = KeepMeLogged;
            userEntity.IsLoggedIn = true;
            userEntity.EncryptionKey = user.Key ?? " DebugKey";
            SessionManager.UserContext = userEntity;
            try
            {
                IEnumerable<UserEntity> users = await DatabaseService.GetAllDataAsync<UserEntity>();
                var result = users.FirstOrDefault(x => user.Username.Equals(x.UserName));


                if (result != null)
                {
                    userEntity.Id = result.Id;
                    await DatabaseService.UpdateDataAsync(userEntity);
                }
                else
                {
                    await DatabaseService.AddDataAsync(userEntity);
                }
            }
            catch
            {
                await DatabaseService.AddDataAsync(userEntity);
            }

        }


    }
}