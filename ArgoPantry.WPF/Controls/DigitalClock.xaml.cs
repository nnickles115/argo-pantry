namespace ArgoPantry.WPF.Controls;

public partial class DigitalClock : UserControl {
    private readonly DispatcherTimer timer = new();

    public DigitalClock() {
        InitializeComponent();
        timer.Tick += OnTimerTick!;
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Start();
    }

    private void OnTimerTick(object sender, EventArgs e) {
        Clock.Text = DateTime.Now.ToString("T");
    }

    private void OnLoaded(object sender, RoutedEventArgs e) {
        timer.Start();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e) {
        timer.Stop();
    }
}