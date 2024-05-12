using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;

namespace Mobile.ViewModels.Authentication
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService) : base(screenReader, navigationService, realmService)
        {
        }


        [ObservableProperty]
        string emailAddress;

        [RelayCommand]
        async Task Login()
        {
            InternalSettings.EmailAddress = EmailAddress;
            await _navigationService.NavigateAsync("AuthenticateEmailPage");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            EmailAddress = InternalSettings.EmailAddress;
        }
    }
}
