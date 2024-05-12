using CommunityToolkit.Mvvm.Input;
using Services;
using IGDB;

namespace Mobile.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        
        private string _text = "Click me";

        public MainPageViewModel(ISemanticScreenReader screenReader, INavigationService navigationService, RealmService realmService) : base(screenReader, navigationService, realmService)
        {
            Title = "Main Page";
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public DelegateCommand CountCommand { get; }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
    }
}
