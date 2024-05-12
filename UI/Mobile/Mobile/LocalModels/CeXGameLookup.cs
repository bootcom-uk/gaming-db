using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Mobile.LocalModels
{
    public partial class CeXGameLookup : ObservableObject
    {

        [ObservableProperty]
        string boxId;

        [ObservableProperty]
        double? sellPrice;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string coverUrl;

        [ObservableProperty]
        ObservableCollection<CeXGameHistoricalPrice> historicalPrices;

    }
}
