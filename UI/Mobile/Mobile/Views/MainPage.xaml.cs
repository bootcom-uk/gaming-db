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
            Dispatcher.CreateTimer();
            Dispatcher.StartTimer(TimeSpan.FromMilliseconds(600), () =>
            {
                (sender as Border).Stroke = Color.Parse("Magenta");
                _isRunning = false;
                return true;
            });
            
        }
    }

}
