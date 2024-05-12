using Gaming.RealmObjects;

namespace Mobile.Views.Gaming;

public partial class GamesPage : ContentPage
{
	public GamesPage()
	{
		InitializeComponent();
	}


    SearchBar searchBar;

    private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        searchBar = (sender as SearchBar);
        if (lvGames.DataSource != null)
        {
            this.lvGames.DataSource.Filter = FilterGames;
            this.lvGames.DataSource.RefreshFilter();
        }
    }

    private bool FilterGames(object obj)
    {
        if (searchBar == null || searchBar.Text == null)
        {
            return true;
        }
            
        var game = obj as Game;
        if (game.Name.ToLower().Contains(searchBar.Text.ToLower()))
        {
            return true;
        }

        return false;
    }

}
