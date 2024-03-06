using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gaming.RealmObjects;
using Services;
using Services.DataServices;

namespace Mobile.ViewModels.Gaming
{
    public partial class MyCeXWishlistPageViewModel : ViewModelBase
    {

        internal CeXWishlistService _cexWishlistService { get; }

        public MyCeXWishlistPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService, CeXWishlistService cexWishlistService) : base(screenReader, navigationService, realmService)
        {
            Title = "My CeX Wishlist";
            _cexWishlistService = cexWishlistService;
        }

        [ObservableProperty]
        IQueryable<CeXWishlist> dataSource;

        [RelayCommand]
        void RefreshView()
        {
            IsRefreshing = false;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            DataSource = await _cexWishlistService.GetWishlistItems();
        }
    }
}
