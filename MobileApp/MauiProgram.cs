using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Northboundei.Mobile.APIs;
using Northboundei.Mobile.Controls;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Mvvm.ViewModels;
using Northboundei.Mobile.Mvvm.Views;
using System.Security;
using Microsoft.Extensions.DependencyInjection;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using Syncfusion.Maui.Core.Hosting;
using Refit;


#if ANDROID
using Northboundei.Mobile.Platforms.Android;
using Xamarin.Android.Net;
#endif
using Northboundei.Mobile.Services;

namespace Northboundei.Mobile
{
    public static class MauiProgram
    {
        static Uri _apiUri = new Uri("https://10.0.2.2:7254");
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .RegisterViews()
                .RegisterViewModels()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddTransient<UserService>();

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<INoteService, NoteService>();
            builder.Services.AddSingleton<IChildService, ChildService>();
            builder.Services.AddSingleton<IUserInfoService, UserInfoService>();

#if ANDROID
            builder.Services.AddSingleton<ISettingsService, SettingsService>();
            builder.Services.AddSingleton<IPermissionService, PermissionService>();
#else
            builder.Services.AddSingleton<IPermissionService, PermissionService>();
#endif
            
            builder.Services.AddSingleton<AuthHttpDelegatingHandler>();


            builder.Services.AddRefitClient<INoteAPI>()
                    .ConfigureHttpClient(c => c.BaseAddress = _apiUri)
                    .ConfigurePrimaryHttpMessageHandler(ConfigureHandler);

             builder.Services.AddRefitClient<ServiceAuthAPI>()
                    .ConfigureHttpClient(c => c.BaseAddress = _apiUri)
                    .ConfigurePrimaryHttpMessageHandler(ConfigureHandler);
 

            builder.Services.AddHttpClient(nameof(AuthAPI), client =>
            {
                client.BaseAddress = _apiUri;
            })
            .ConfigurePrimaryHttpMessageHandler(ConfigureHandler);

            // Register Routes
            RegisterRoutes();

            builder.Services.AddSingleton<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var serviceProvider = builder.Services.BuildServiceProvider();
            builder.Services.AddSingleton(serviceProvider);

            return builder.Build();
        }
        static void RegisterRoutes()
        {
            Routing.RegisterRoute("NotesPage", typeof(NotesPage));
            Routing.RegisterRoute("NotesDraftPage", typeof(NotesDraftPage));
            Routing.RegisterRoute("SyncPage", typeof(SyncPage));
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            // Pages
            builder.Services.AddTransient<SplashScreenPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<NotesPage>();
            builder.Services.AddTransient<NotesDraftPage>();
            builder.Services.AddTransient<SyncPage>();

            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            // ViewModels
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<NotesViewModel>();
            builder.Services.AddSingleton<SyncViewModel>();

            builder.Services.AddTransient<LoginViewModel>();
            return builder;
        }

        private static HttpMessageHandler ConfigureHandler()
        {
#if ANDROID
            var primaryHandler =new AndroidMessageHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                AllowAutoRedirect = true
            };
#else
            var primaryHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                AllowAutoRedirect = true
            };
#endif

            var authenticatedHandler = new AuthenticatedHttpClientHandler(async () =>
            {
                IEnumerable<UserEntity> users = await DatabaseService.GetAllDataAsync<UserEntity>();
                if (users.Any())
                {
                    var currentUser = users.Last();
                    return currentUser?.Token;
                }
                return null;
            });

            // Chain the primary handler
            authenticatedHandler.InnerHandler = primaryHandler;
            return authenticatedHandler;
        }

        private static void RegisterCustomRenderers(MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                handlers.AddHandler(typeof(CustomEntry), typeof(CustomEntryRenderer));
#endif
            });
        }
    }
}
