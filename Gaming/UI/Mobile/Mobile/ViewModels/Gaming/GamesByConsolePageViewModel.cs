using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gaming.RealmObjects;
using Microsoft.Extensions.Configuration;
using Mobile.Common.Config;
using Services;
using Services.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModels.Gaming
{
    public partial class GamesByConsolePageViewModel : ViewModelBase
    {

        [ObservableProperty]
        IQueryable<ConsoleType> dataSource;

        internal ConsoleService _consoleService { get; }

        internal GamesService _gamesService { get; }


        public GamesByConsolePageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService, ConsoleService consoleService, GamesService gamesService) : base(screenReader, navigationService, realmService)
        {
            _consoleService = consoleService;
            _gamesService = gamesService;
        }

        [RelayCommand]
        async Task SelectConsole(object selectedItem)
        {
            var console = (selectedItem as ConsoleType);
            await NavigateTo("GamesPage", new NavigationParameters()
            {
                { "console", console.Id }
            });
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            DataSource = await _consoleService.GetActiveConsoles();
            var gameCount = (await _gamesService.GetGames(null)).Count();
            Title = $"{gameCount} Games Found";
        }
    }
}
