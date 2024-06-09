using Gaming.RealmObjects;
using Mobile.ViewModels.Gaming;

namespace Mobile.Views.Gaming;

public partial class ConsolesPage : ContentPage
{
	public ConsolesPage()
	{
		InitializeComponent();

        lvConsoles.DataSource.SourceCollectionChanged += (sender, e) =>
        {
            var viewModel = (BindingContext as ConsolesPageViewModel);
            lvConsoles.DataSource.Filter = PerformFilter;
            lvConsoles.DataSource.RefreshFilter();
        };
	}

    private bool isToggled = false;

    private void swShowAllConsoles_Toggled(object sender, ToggledEventArgs e)
    {

        isToggled = e.Value;
        lvConsoles.DataSource.Filter = PerformFilter;
        lvConsoles.DataSource.RefreshFilter();

    }

    private bool PerformFilter(object obj)
    {
        var console = obj as ConsoleType;
        if(isToggled) return true;
        return console.Enabled;
    }

}
