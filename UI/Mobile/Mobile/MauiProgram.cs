using CommunityToolkit.Maui;
using Mobile.ViewModels;
using Mobile.ViewModels.Authentication;
using Mobile.ViewModels.Gaming;
using Mobile.Views;
using Mobile.Views.Authentication;
using Mobile.Views.Gaming;
using Services;
using Syncfusion.Maui.Core.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using ZXing.Net.Maui.Controls;
using Mobile.Views.Personal;
using Mobile.ViewModels.Personal;
using MAUI.Stats;

namespace Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var appSettingsStream = null as Stream;

#if DEBUG
            appSettingsStream = assembly.GetManifestResourceStream("Mobile.appsettings.development.json");

#else
            appSettingsStream = assembly.GetManifestResourceStream("Mobile.appsettings.json");
#endif

            var config = new ConfigurationBuilder()
                        .AddJsonStream(appSettingsStream)
                        .Build();

            var builder = MauiApp.CreateBuilder();
            builder.Services.SetupDeviceMetrics();
            
            builder.Configuration.AddConfiguration(config);
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .UsePrism(prism =>
                {

                    prism.ConfigureServices(services =>
                    {
                        services.AddHttpClient();
                    });

                    prism.RegisterTypes(containerRegistry =>
                    {
                        containerRegistry
                        .RegisterSingleton<HttpService>();

                        var blobStorageService = new BlobStorageService(config.GetSection("Azure").GetValue<string>("BlobStorageUrl"));

                        containerRegistry
                        .RegisterInstance(blobStorageService);

                        var realmService = new RealmService(InternalSettings.RealmDatabaseKey);

                        containerRegistry
                        .RegisterInstance(realmService);

                        containerRegistry
                        .RegisterForNavigation<NavigationPage>();

                        containerRegistry
                        .RegisterForNavigation<LoginPage, LoginPageViewModel>()
                        .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                        .RegisterForNavigation<AuthenticateEmailPage, AuthenticateEmailPageViewModel>()
                        .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                        .RegisterForNavigation<ValidateLoginPage, ValidateLoginPageViewModel>()
                        .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                        .RegisterForNavigation<MainPage, MainPageViewModel>()
                        .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                        .RegisterForNavigation<GamesPage, GamesPageViewModel>()
                        .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                        .RegisterForNavigation<GamesByConsolePage, GamesByConsolePageViewModel>()
                        .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                       .RegisterForNavigation<ConsolesPage, ConsolesPageViewModel>()
                       .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                       .RegisterForNavigation<UserDetailsPage, UserDetailsPageViewModel>()
                       .RegisterInstance(SemanticScreenReader.Default);

                        containerRegistry
                        .RegisterForNavigation<MyCeXWishlistPage, MyCeXWishlistPageViewModel>()
                        .RegisterInstance(SemanticScreenReader.Default);

                    });

                    prism.OnAppStart(async navigationService =>
                    {
                        // We have a stored token
                        if (!string.IsNullOrWhiteSpace(InternalSettings.UserToken))
                        {
                            // Is this user still authenticated?
                            await navigationService.NavigateAsync("ValidateLoginPage");

                            return;
                        }

                        // Take the user to a page where they can authenticate themselves
                        await navigationService.NavigateAsync("LoginPage");
                    });
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("ARLRDBD.TTF", "ArialRoundedMtBold");
                })
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit();

            return builder.Build();
        }
    }
}
