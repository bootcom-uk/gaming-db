using CommunityToolkit.Mvvm.ComponentModel;

namespace Mobile.Common.Config
{
    public partial class IGDB : ObservableObject
    {

        [ObservableProperty]
        string clientId;

        [ObservableProperty]
        string clientSecret;

    }
}
