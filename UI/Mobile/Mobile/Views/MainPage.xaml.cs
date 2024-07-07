namespace Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        internal bool _isRunning = false;

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (_isRunning) return;
            _isRunning = true;
            (sender as Border).Stroke = Color.Parse("Black");
            (sender as Border).Background = Color.Parse("Magenta");
            Dispatcher.CreateTimer();
            Dispatcher.StartTimer(TimeSpan.FromSeconds(6), () =>
            {
                (sender as Border).Stroke = Color.Parse("Magenta");
                _isRunning = false;
                return true;
            });
            
        }
    }

}
