using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;

namespace Mobile.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject, INavigationAware
    {

        [ObservableProperty]
        string title;

        [ObservableProperty]
        bool isSearching;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool canRefresh;

        [ObservableProperty]
        bool isProcessing;

        [ObservableProperty]
        string footerText;
       
        internal ISemanticScreenReader _screenReader { get; }

        internal INavigationService _navigationService { get; }

        internal RealmService _realmService { get; }

        public ViewModelBase(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService) {
            _screenReader = screenReader;
            _navigationService = navigationService;
            _realmService = realmService;

            CurrentScreen = InternalSettings.CurrentScreen;
            FooterText = $"© 2023 - {DateTime.Now.Year} BootCom";
        }

        public abstract void OnNavigatedFrom(INavigationParameters parameters);

        public abstract void OnNavigatedTo(INavigationParameters parameters);

        #region "Navigation"

        [ObservableProperty]
        bool isNavigationPopupOpen;

        [ObservableProperty]
        string currentScreen;

        [ObservableProperty]
        string navigationScreenName;

        [RelayCommand]
        async Task Navigate(string pageName)
        {
            await NavigateTo(pageName);
        }

        public async Task NavigateTo(string pageName, NavigationParameters parameters = null)
        {
            IsNavigationPopupOpen = false;
            InternalSettings.CurrentScreen = pageName;
            await _navigationService.NavigateAsync($"/{pageName}", parameters);
        }

        [RelayCommand]
        void OpenNavigationPopup()
        {
            IsNavigationPopupOpen = true;
        }

        #endregion

    }
}
