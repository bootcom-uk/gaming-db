using CommunityToolkit.Mvvm.Input;
using Services;
using IGDB;
using CommunityToolkit.Mvvm.ComponentModel;
using Services.DataServices;

namespace Mobile.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {

        [ObservableProperty]
        GamesService gameService;

        public MainPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService, GamesService gamesService) : base(screenReader, navigationService, realmService)
        {
            Title = "Main Page";
            GameService = gamesService;
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        [ObservableProperty]
        int numberOfGamesAssociatedToCexWishlistAtHighestPrice;

        [ObservableProperty]
        int numberOfGamesAssociatedToCexWishlistAtLowestPrice;


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var highestPriceList = await GameService.GamesAssociatedToCeXWishlistAtHighestPrice();
            var lowestPriceList = await GameService.GamesAssociatedToCeXWishlistAtLowestPrice();

            NumberOfGamesAssociatedToCexWishlistAtHighestPrice = highestPriceList.Count();
            NumberOfGamesAssociatedToCexWishlistAtLowestPrice = lowestPriceList.Count();

        }
    }
}
