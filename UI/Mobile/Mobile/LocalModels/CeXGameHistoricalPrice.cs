using CommunityToolkit.Mvvm.ComponentModel;

namespace Mobile.LocalModels
{
    public partial class CeXGameHistoricalPrice : ObservableObject
    {

        [ObservableProperty]
        DateTime date;

        [ObservableProperty]
        double price;

    }
}
