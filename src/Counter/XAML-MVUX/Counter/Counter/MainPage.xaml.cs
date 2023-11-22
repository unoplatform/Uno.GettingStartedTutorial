namespace Counter
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new BindableMainModel();
        }
    }
}
