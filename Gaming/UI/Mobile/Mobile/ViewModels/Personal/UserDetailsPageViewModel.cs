using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gaming.RealmObjects;
using MongoDB.Bson;
using Services;
using Services.DataServices;

namespace Mobile.ViewModels.Personal
{
    public partial class UserDetailsPageViewModel : ViewModelBase
    {

        internal UserDetailService _userDetailService;

        public UserDetailsPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService, UserDetailService userDetailService) : base(screenReader, navigationService, realmService)
        {
            _userDetailService = userDetailService;
        }

        [ObservableProperty]
        UserDetails dataSource;

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {            
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Title = "My Details";

            var userDetails = await _userDetailService.GetUserDetails();

            DataSource = new()
            {
                CeXMemberUsername = userDetails?.CeXMemberUsername,
                CeXMemberId = userDetails?.CeXMemberId,
                CeXMemberPassword = userDetails?.CeXMemberPassword
            };
        }

        [RelayCommand]
        async Task SaveGame()
        {
            var savedUserDetails = await _userDetailService.GetUserDetails();

            var userDetails = new UserDetails()
            {
                CeXMemberId = DataSource.CeXMemberId,
                CeXMemberUsername = DataSource.CeXMemberUsername,
                CeXMemberPassword= DataSource.CeXMemberPassword
            };

            if(savedUserDetails is null)
            {
                userDetails.Id = ObjectId.GenerateNewId();
                userDetails.UserId = InternalSettings.UserId.Value;
            }

            await _userDetailService.Save(userDetails);

            await NavigateCommand.ExecuteAsync("MainPage");
        }
    }
}
