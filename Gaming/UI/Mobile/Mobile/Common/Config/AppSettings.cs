using CommunityToolkit.Mvvm.ComponentModel;

namespace Mobile.Common.Config
{
    public partial class AppSettings : ObservableObject
    {

        [ObservableProperty]
        Syncfusion syncfusion;

        [ObservableProperty]
        IGDB iGDB;

        [ObservableProperty]
        Azure azure;

    }
}
