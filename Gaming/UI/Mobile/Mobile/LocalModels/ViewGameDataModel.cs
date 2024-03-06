using CommunityToolkit.Mvvm.ComponentModel;

namespace Mobile.LocalModels
{
    public partial class ViewGameDataModel : ModifyGameDataModel
    {

        [ObservableProperty]
        CeXGameLookup ceXGameData;

    }
}
