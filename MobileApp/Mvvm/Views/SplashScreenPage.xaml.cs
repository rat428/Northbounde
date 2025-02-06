using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.DependencyInjection;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Mvvm.ViewModels;
using Northboundei.Mobile.Services;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Northboundei.Mobile.Mvvm.Views;

public partial class SplashScreenPage : ContentPage
{
    private readonly IUserService _userService;
    IPermissionService _locationPermissionService;
    IServiceProvider _serviceProvider;
    HomeViewModel _homeViewModel;
    public SplashScreenPage(IUserService userService, IPermissionService locationPermissionService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _userService = userService;
        _locationPermissionService = locationPermissionService;
        _serviceProvider = serviceProvider;
        _homeViewModel = _serviceProvider.GetService<HomeViewModel>()!;
    }

    public async Task OnStart()
    {
        while (!await CheckPermissions())
        {
            await RequestPermissions();
        }
        await NavigateToMainPage();
    }

    private async Task NavigateToMainPage()
    {
        if (await CanLogin())
        {
            App.Current!.Windows[0].Page = _serviceProvider.GetService<AppShell>();
        }
        else
        {
            var loginPage = _serviceProvider.GetService<LoginPage>();
            App.Current!.Windows[0].Page = loginPage;
        }
    }

    public async Task<bool> CanLogin()
    {
        string? storedDate = await SecureStorage.Default.GetAsync("storedDate");
        if (storedDate == null || !DateTime.TryParse(storedDate, out DateTime firstDate) || firstDate.Date != DateTime.Today)
        {
            await SecureStorage.Default.SetAsync("storedDate", DateTime.Today.ToString("o"));
            SessionManager.ClearSession();
            // todo: return false;
            return false;
        }

        IEnumerable<UserEntity> users = await DatabaseService.GetAllDataAsync<UserEntity>();
        if (!users.Any())
            return false;

        try
        {
            var userEntity = users.Last();
            if (userEntity == null)
                return false;

            if (!userEntity.IsLoggedIn)
                return false;

            if (!userEntity.KeepMeLoggedIn)
                return false;

            // Check if the token is expired or not
            if (userEntity.ExpirationTime < DateTime.Now.ToUniversalTime())
            {
                // Show alert that the token is expired
                await DisplayAlert("Token Expired", "Please login again", "OK");
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;

    }

    /// <summary>
    /// Check permission and request permission when needed
    /// </summary>
    /// <returns></returns>
    private async Task<bool> CheckPermissions()
    {
        _homeViewModel.Settings = new Models.SettingsModel();

        _homeViewModel!.Settings.LocationSharingAllowed = await _locationPermissionService.IsLocationPermissionGrantedAsync();
        _homeViewModel.Settings.AirplaneMode = await _locationPermissionService.IsAirplaneModeEnabled();
        _homeViewModel.Settings.GpsOn = await _locationPermissionService.IsGpsEnabled();
        _homeViewModel.Settings.DeveloperToolsEnabled = await _locationPermissionService.IsDeveloperModeEnabled();
        _homeViewModel.Settings.InternetAccessAvailable = await _locationPermissionService.IsInternetAvailable();
        _homeViewModel.Settings.AllowRunInBackground = await _locationPermissionService.CanRunInBackground();

        return _homeViewModel.IsValid();
    }

    private async Task RequestPermissions()
    {
        if (_homeViewModel.Settings.LocationSharingAllowed is false)
        {
            await DisplayAlert("Sharing Location must be enabled", "", "Done!");
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                PermissionStatus locationInUser = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                PermissionStatus locationAlways = await Permissions.RequestAsync<Permissions.LocationAlways>();
            });
        }

        if (_homeViewModel!.Settings.AirplaneMode is true)
            await DisplayAlert("", "Airplane mode must be turned off", "Done!");

        if (_homeViewModel!.Settings.GpsOn is false)
            await DisplayAlert("", "Location(GPS) must be turned on", "Done!");

#if !DEBUG
        if (_homeViewModel!.Settings.DeveloperToolsEnabled is true)
            await DisplayAlert("", "Developer mode must be turned off", "Done!");
#endif
        if (_homeViewModel!.Settings.AllowRunInBackground is false)
            await DisplayAlert("", "Running service in background must be turned On", "Done!");
    }
}