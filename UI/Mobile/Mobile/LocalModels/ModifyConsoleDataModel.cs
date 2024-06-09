using CommunityToolkit.Mvvm.ComponentModel;
using MongoDB.Bson;

namespace Mobile.LocalModels
{
    public partial class ModifyConsoleDataModel : ObservableObject
    {

        [ObservableProperty]
        ObjectId id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        int? ceXConsoleId;

        [ObservableProperty]
        bool hasGames;

        [ObservableProperty]
        bool enabled;

        [ObservableProperty]
        int iGDBId;

        [ObservableProperty]
        string coverURL;

    }
}
