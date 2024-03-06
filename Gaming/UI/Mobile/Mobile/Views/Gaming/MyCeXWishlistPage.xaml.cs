using Gaming.RealmObjects;

namespace Mobile.Views.Gaming;

public partial class MyCeXWishlistPage : ContentPage
{
	public MyCeXWishlistPage()
	{
		InitializeComponent();
	}

    internal Switch swShowOnlyActiveItems { get; set; }

    private void swShowOnlyActiveItems_Toggled(object sender, ToggledEventArgs e)
    {
        swShowOnlyActiveItems = sender as Switch;
        this.lvGames.DataSource.Filter = FilterWishlistItems;
        this.lvGames.DataSource.RefreshFilter();
    }

    private bool FilterWishlistItems(object obj)
    {        
        var cexWishlistItem = obj as CeXWishlist;
        if (swShowOnlyActiveItems.IsToggled) return true;        
        return cexWishlistItem.Active;
    }
}
