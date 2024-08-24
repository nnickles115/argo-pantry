namespace ArgoPantry.WPF.Services; 
public class ApplicationHostService : IHostedService {
    private readonly IServiceProvider _serviceProvider;

    private INavigationWindow _navigationWindow;

    public ApplicationHostService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        await HandleActivationAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken) {
        await Task.CompletedTask;
    }

    private async Task HandleActivationAsync() {
        if(!Application.Current.Windows.OfType<MainWindow>().Any()) {
            _navigationWindow = (
                _serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow
            )!;
            _navigationWindow!.ShowWindow();

            _navigationWindow.Navigate(typeof(DashboardPage));
        }

        await Task.CompletedTask;
    }
}
