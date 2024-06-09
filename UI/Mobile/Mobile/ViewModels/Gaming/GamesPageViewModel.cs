using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gaming.RealmObjects;
using IGDB.Models;
using Microsoft.Extensions.Configuration;
using Mobile.Common.Config;
using Mobile.LocalModels;
using MongoDB.Bson;
using Services;
using Services.DataServices;
using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;

namespace Mobile.ViewModels.Gaming
{
    public partial class GamesPageViewModel : ViewModelBase
    {
        internal GamesService _gamesService { get; }

        internal ConsoleService _consoleService { get; }

        [ObservableProperty]
        IQueryable<Game> dataSource;

        [ObservableProperty]
        IQueryable<ConsoleType> consoleTypesDataSource;

        [ObservableProperty]
        IGDB.IGDB iGDBClient;

        internal AppSettings _appSettings { get; }

        internal BlobStorageService _blobStorageService { get; }

        internal CeXService _cexService { get; }

        public GamesPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService, GamesService gamesService, ConsoleService consoleService, IConfiguration configuration, BlobStorageService blobStorageService, CeXService cexService) : base(screenReader, navigationService, realmService)
        {
            _appSettings = configuration.Get<AppSettings>();
            IGDBClient = new IGDB.IGDB(_appSettings.IGDB.ClientId, _appSettings.IGDB.ClientSecret);
            _gamesService = gamesService;
            _consoleService = consoleService;
            _blobStorageService = blobStorageService;
            _cexService = cexService;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        internal ObjectId? _consoleCategoryId { get; set; }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("console")) {
                _consoleCategoryId = parameters["console"] as ObjectId?;
            };

            await RefreshViewCommand.ExecuteAsync(_consoleCategoryId);

            ConsoleTypesDataSource = await _consoleService.GetActiveConsoles();

            AddGamePopupTabHeaderSearchText = "Search";
            AddGamePopupHeaderText = "Add New Game";
        }

        #region "Add/Modification of games"

        [ObservableProperty]
        bool isModifyGamePopupOpen;

        [ObservableProperty]
        ModifyGameDataModel modifyGameDataSource;

        [ObservableProperty]
        int gameSearchResultCount;

        [ObservableProperty]
        IEnumerable<GameSearchResult> gameSearchResults;

        [ObservableProperty]
        bool isUpdate;

        [ObservableProperty]
        string addGamePopupTabHeaderSearchText;

        [ObservableProperty]
        string addGamePopupHeaderText;


        #region "View Game Information"

        [ObservableProperty]
        ViewGameDataModel viewGameDataSource;

        [ObservableProperty]
        bool showViewGamePopup;

        [RelayCommand]
        async Task ViewGame(object record)
        {            
            var id = (record as Game).Id;
            var game = await _gamesService.GetGameById(id);
            ViewGameDataSource = new()
            {
                AmountPaid = game.AmountPaid,
                CeXGame = game.CexDetails?.BoxId,
                Summary = game.Summary,
                GameCoverUrl = game.GameCoverUrl,
                HasCase = game.HasCase.Value,
                HasGame = game.HasGame.Value,
                HasManual = game.HasManual.Value,
                Id = id,
                IgdbId = game.IgdbId.Value,
                IsSpecialEdition = game.IsCollectorsEdition.Value,
                IgdbUrl = game.IgdbUrl,
                IsCopy = game.IsCopy.Value,
                IsNonPalRegion = game.IsNonPalRegion.Value,
                Name = game.Name,
                Notes = game.Notes
            };

       
            ViewGameDataSource.CeXGameData = new()
            {
                BoxId = game.CexDetails?.BoxId,
                Name = game.CexDetails?.Name,
                SellPrice = game.CexDetails?.LastSalePrice,
                HistoricalPrices = new()
            };
             game.CexDetails?.SellPrices.ForEach(record => ViewGameDataSource.CeXGameData.HistoricalPrices.Add(new() { Date = record.DateChecked.Value.LocalDateTime.Date, Price = record.Price.Value }));
               
                        
            ShowViewGamePopup = true;
        }

        #endregion

        [RelayCommand]
        async Task GameSearch()
        {
            if (IsUpdate) return;

            try
            {
                await Application.Current.Dispatcher.DispatchAsync(async () =>
                {
                    IsSearching = true;

                    AddGamePopupTabHeaderSearchText = "Searching (Please Wait)";

                             
                    var response = await IGDBClient.SearchGame(ModifyGameDataSource.Name, ModifyGameDataSource.SelectedConsole.IgdbId);
                    if (response is null)
                    {
                        GameSearchResultCount = 0;
                        GameSearchResults = Enumerable.Empty<GameSearchResult>();
                        AddGamePopupTabHeaderSearchText = "Search";
                        IsSearching = false;
                        return;
                    }
                    response.ForEach(record =>
                    {
                        if (record.Cover != null && record.Cover.Url.StartsWith("//"))
                        {
                            record.Cover.Url = $"https:{record.Cover.Url}";
                        }
                    });
                    GameSearchResults = response;
                    GameSearchResultCount = response.Count();

                    AddGamePopupTabHeaderSearchText = "Search";
                    IsSearching = false;
                });
            } catch (Exception)
            {
                GameSearchResultCount = 0;
                GameSearchResults = Enumerable.Empty<GameSearchResult>();
                IsSearching = false;
                return;
            }
        }

        [RelayCommand]
        async Task EditGame(ObjectId? gameId)
        {
            IsUpdate = true;
            ModifyGameDataSource = new();
            IsUpdate = false;

            if (gameId != null)
            {
                IsUpdate = true;
                var game = await _gamesService.GetGameById(gameId);
                ModifyGameDataSource = new()
                {
                    AmountPaid = game.AmountPaid,
                    GameCoverUrl = game.GameCoverUrl,
                    HasCase = game.HasCase ?? false,
                    HasGame = game.HasGame ?? false,
                    HasManual = game.HasManual ?? false,
                    Id = game.Id,
                    IgdbId = game.IgdbId ?? 0,
                    IgdbUrl = game.IgdbUrl,
                    IsCopy = game.IsCopy ?? false,
                    IsNonPalRegion = game.IsNonPalRegion ?? false,
                    IsSpecialEdition = game.IsCollectorsEdition ?? false,
                    Name = game.Name,
                    Summary = game.Summary,
                    SelectedConsole = game.ConsoleId,
                    Notes = game.Notes,
                    CeXGame = game.CexDetails?.BoxId
                };

                IsModifyGamePopupOpen = true;
                return;
            }
            

            GameSearchResultCount = 0;
            GameSearchResults = Enumerable.Empty<GameSearchResult>();

            if(_consoleCategoryId != null)
            {
                ModifyGameDataSource.SelectedConsole = ConsoleTypesDataSource.FirstOrDefault(record => record.Id == _consoleCategoryId.Value);
            }

            // ModifyGameDataSource.HasGame


            IsModifyGamePopupOpen = true;
        }

        [RelayCommand]
        async Task UpdateImageFromIGDB()
        {
            var gameDetail = await IGDBClient.GetGameDetail(ModifyGameDataSource.IgdbId);

            if (gameDetail.Cover is null) return;

            if (gameDetail.Cover!.Url.StartsWith("//"))
            {
                gameDetail.Cover!.Url = $"https:{gameDetail.Cover?.Url}";
            }
            var lastPart = gameDetail.Cover?.Url.Split("/").Last();
            var internalGameCoverUri = await _blobStorageService.SaveFile("games", InternalSettings.UserId.Value, lastPart, gameDetail.Cover!.Url);
            ModifyGameDataSource.GameCoverUrl = internalGameCoverUri.ToString();
            

                
        }

        [RelayCommand]
        async Task SaveGame()
        {

            if (!IsUpdate && (string.IsNullOrWhiteSpace(ModifyGameDataSource.Name) || ModifyGameDataSource.SelectedSearchedGame is null || ModifyGameDataSource.SelectedConsole is null)) return;

            if (IsUpdate && string.IsNullOrWhiteSpace(ModifyGameDataSource.Name)) return;

            IsProcessing = true;

            if (ModifyGameDataSource.SelectedSearchedGame?.Cover != null)
            {
                var lastPart = ModifyGameDataSource.SelectedSearchedGame.Cover?.Url.Split("/").Last();

                var internalGameCoverUri = await _blobStorageService.SaveFile("games", InternalSettings.UserId.Value, lastPart, ModifyGameDataSource.SelectedSearchedGame.Cover!.Url);

                ModifyGameDataSource.SelectedSearchedGame.Cover.Url = internalGameCoverUri.ToString();
            }

            var cexDetails = null as CeXGame;

            if (!IsUpdate && !string.IsNullOrWhiteSpace(ModifyGameDataSource.CeXGame))
            {
                cexDetails = await _cexService.SaveCeXBoxInformation(ModifyGameDataSource.CeXGame, ModifyGameDataSource.SelectedSearchedGame.Id, ModifyGameDataSource.SelectedSearchedGame.Name);
            }

            var game = new Game()
            {
                ConsoleId = ModifyGameDataSource.SelectedConsole,
                HasCase = ModifyGameDataSource.HasCase,
                HasGame = ModifyGameDataSource.HasGame,
                HasManual = ModifyGameDataSource.HasManual,
                Id = ObjectId.GenerateNewId(),
                IsCollectorsEdition = ModifyGameDataSource.IsSpecialEdition,
                IsCopy = ModifyGameDataSource.IsCopy,
                IsNonPalRegion = ModifyGameDataSource.IsNonPalRegion,
                LastUpdated = DateTime.Now,
                UserId = InternalSettings.UserId,
                Notes = ModifyGameDataSource.Notes,
                AmountPaid = ModifyGameDataSource.AmountPaid,
                CexDetails = cexDetails
            };

            if (IsUpdate)
            {
                var originalGame = await _gamesService.GetGameById(ModifyGameDataSource.Id.Value);

                if (originalGame is null) return;

                game.CexDetails = await _cexService.SaveCeXBoxInformation(ModifyGameDataSource.CeXGame, ModifyGameDataSource.IgdbId, ModifyGameDataSource.Name);
                game.IgdbId = ModifyGameDataSource.IgdbId;
                game.IgdbUrl = ModifyGameDataSource.IgdbUrl;
                game.Summary = ModifyGameDataSource.Summary;
                game.Id = ModifyGameDataSource.Id.Value;                
                game.Name = ModifyGameDataSource.Name;
                game.GameCoverUrl = ModifyGameDataSource.GameCoverUrl;
            } else
            {
                game.GameCoverUrl = ModifyGameDataSource.SelectedSearchedGame.Cover?.Url;
                game.Name = ModifyGameDataSource.SelectedSearchedGame.Name;
                game.IgdbId = ModifyGameDataSource.SelectedSearchedGame.Id;
                game.IgdbUrl = ModifyGameDataSource.SelectedSearchedGame.Url;
                game.Summary = ModifyGameDataSource.SelectedSearchedGame.Summary;
            }

            await _gamesService.SaveGame(game, IsUpdate);

            await RefreshViewCommand.ExecuteAsync(null);
                        
            IsProcessing = false;

            IsModifyGamePopupOpen = false;

        }

        #endregion



        [RelayCommand]
        async Task RefreshView()
        {
            IsRefreshing = true;
            DataSource = await _gamesService.GetGames(_consoleCategoryId);
            var console = await _consoleService.GetConsoleById(_consoleCategoryId);
            Title = $"{console.Name} ({DataSource.Count()} games)";
            IsRefreshing = false;
        }

    }
}
