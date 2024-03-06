using CommunityToolkit.Mvvm.ComponentModel;

namespace Mobile.Common.Config
{
    public partial class AzureBlobStorage : ObservableObject
    {

        [ObservableProperty]
        string blobStorageUrl;

    }
}
