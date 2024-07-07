using CommunityToolkit.Mvvm.ComponentModel;
using Gaming.RealmObjects;
using IGDB.Models;
using MongoDB.Bson;

namespace Mobile.LocalModels
{
    public partial class ModifyGameDataModel : ObservableObject
    {

        [ObservableProperty]
        ObjectId? id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        ConsoleType selectedConsole;

        [ObservableProperty]
        GameSearchResult selectedSearchedGame;

        [ObservableProperty]
        string ceXGame;

        [ObservableProperty]
        long igdbId;

        [ObservableProperty]
        string igdbUrl;

        [ObservableProperty]
        bool hasGame;

        [ObservableProperty]
        bool hasCase;

        [ObservableProperty]
        bool hasManual;

        [ObservableProperty]
        bool isSpecialEdition;

        [ObservableProperty]
        bool isCopy;

        [ObservableProperty]
        bool isNonPalRegion;

        [ObservableProperty]
        string summary;

        [ObservableProperty]
        string gameCoverUrl;

        [ObservableProperty]
        string notes;

        [ObservableProperty]
        double? amountPaid;
    }
}
