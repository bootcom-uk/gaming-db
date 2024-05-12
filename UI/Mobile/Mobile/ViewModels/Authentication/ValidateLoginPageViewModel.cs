using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace Mobile.ViewModels.Authentication
{
    public partial class ValidateLoginPageViewModel : ViewModelBase
    {

        internal readonly HttpService _httpService;

        public ValidateLoginPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, HttpService httpService, RealmService realmService) : base(screenReader, navigationService, realmService)
        {
            _httpService = httpService;
        }

        [ObservableProperty]
        string validateLoginText;

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            ValidateLoginText = "Please wait whilst we check that your authentication is valid ...";

            try
            {
                var response = await _httpService.ValidateLogin(InternalSettings.DeviceId!.Value, InternalSettings.UserToken, InternalSettings.RefreshToken);

                InternalSettings.CurrentScreen = "MainPage";

                if (response is null)
                {
                    _realmService.SetUserId(InternalSettings.UserId.Value);
                    await _navigationService.NavigateAsync("MainPage");
                    return;
                }

                InternalSettings.UserToken = response["token"];
                InternalSettings.RefreshToken = response["refreshToken"];
                InternalSettings.UserId = Guid.Parse(response["userId"]);
                _realmService.SetUserId(InternalSettings.UserId.Value);
                await _navigationService.NavigateAsync("MainPage");
            }
            catch (Exception)
            {
                InternalSettings.UserToken = null;
                InternalSettings.RefreshToken = null;
                InternalSettings.UserId = null;
                await _navigationService.NavigateAsync("LoginPage");
            }

        }

    }
}
