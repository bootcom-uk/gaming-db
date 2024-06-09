using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Extensions;
using Gaming.RealmObjects;
using Microsoft.Extensions.Configuration;
using Mobile.Common.Config;
using Mobile.LocalModels;
using Services;
using Services.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModels.Gaming
{
    public partial class ConsolesPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        IQueryable<ConsoleType> dataSource;

        internal ConsoleService _consoleService { get; }

        internal HttpService _httpService { get; }

        internal BlobStorageService _blobStorageService { get; }

        [ObservableProperty]
        IGDB.IGDB iGDBClient;

        internal AppSettings _appSettings { get; }

        public ConsolesPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService, ConsoleService consoleService, HttpService httpService, IConfiguration configuration, BlobStorageService blobStorageService) : base(screenReader, navigationService, realmService)
        {
            Title = "My Consoles";
            _consoleService = consoleService;
            _httpService = httpService;
            _appSettings = configuration.Get<AppSettings>();
            _blobStorageService = blobStorageService;
            IGDBClient = new IGDB.IGDB(_appSettings.IGDB.ClientId, _appSettings.IGDB.ClientSecret);
        }

        [RelayCommand]
        void RefreshView()
        {
            IsRefreshing = false; 
        }

        #region "Modify Console Popup"

        [ObservableProperty]
        string popupTitle;

        [ObservableProperty]
        bool isModifyConsolePopupOpen;

        [ObservableProperty]
        ModifyConsoleDataModel popupDataSource;

        [RelayCommand]
        async Task PopupSave()
        {
            var console = await _consoleService.GetConsoleById(PopupDataSource.Id);

            var internalGameCoverUri = null as Uri;

            if (!string.IsNullOrWhiteSpace(console.ImageUrl) && console.ImageUrl != PopupDataSource.CoverURL)
            {

                var lastPart = PopupDataSource.CoverURL.Split("/").Last();

                internalGameCoverUri = await _blobStorageService.SaveFile("consoles", InternalSettings.UserId.Value, lastPart, PopupDataSource.CoverURL);

            }

            await console.Realm.WriteAsync(async () =>
            {
                
                if(internalGameCoverUri != null)
                {
                    console.ImageUrl = internalGameCoverUri.ToString();
                }
                
                console.Name = PopupDataSource.Name;
                console.Enabled = PopupDataSource.Enabled;
                console.CeXConsoleId = PopupDataSource.CeXConsoleId;

                await _consoleService.Update(console);
            });
                           
            IsModifyConsolePopupOpen = false;
        }

        [RelayCommand]
        async Task SelectConsole(object selectedItem)
        {
            var console = (selectedItem as ConsoleType);
            if(console is null) { return;  }


            PopupDataSource = new()
            {
                IGDBId = console.IgdbId,
                CoverURL = console.ImageUrl,
                Enabled = console.Enabled,
                Id = console.Id,
                Name = console.Name,
                CeXConsoleId = console.CeXConsoleId
            };

            if (!await _httpService.IsValidUrl(console.ImageUrl))
            {
                var platformCoverUrl = await IGDBClient.GetPlatformCover(console.IgdbId);
                if (!string.IsNullOrWhiteSpace(platformCoverUrl))
                {
                    PopupDataSource.CoverURL = platformCoverUrl;
                    if (PopupDataSource.CoverURL.StartsWith("//")) PopupDataSource.CoverURL = $"https:{PopupDataSource.CoverURL}";
                }                
            }

            PopupTitle = $"Modifying {console.Name}";
            IsModifyConsolePopupOpen = true;
            
        }

        #endregion

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            DataSource = await _consoleService.GetConsoles();
        }
    }
}
