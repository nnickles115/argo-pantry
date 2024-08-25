namespace ArgoPantry.WPF;
public partial class App : Application {
    private readonly IHost _host;

    public App() {
        _host = CreateHostBuilder().Build();
    }

    public static IHostBuilder CreateHostBuilder(string[]? args = null) {
        return Host.CreateDefaultBuilder(args)
            .AddDbContext()
            .AddServices()
            .AddViewModels()
            .AddViews();
    }

    private async void OnStartup(object sender, StartupEventArgs e) {
        _host.Start();
        InitDbMigrations();
        await InitThemeAsync();
    }

    private void InitDbMigrations() {
        using var scope = _host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ArgoPantryDbContext>();
        context.Database.Migrate();
    }

    private async Task InitThemeAsync() {
        using var scope = _host.Services.CreateScope();
        var themeService = scope.ServiceProvider.GetRequiredService<IThemeService>();

        // Get the theme setting from the user settings
        string themeSetting = Settings.Default.Theme;

        // Default to system theme if the setting is not set
        if(!Enum.TryParse(themeSetting, out ApplicationTheme currentTheme)) {
            currentTheme = themeService.GetSystemTheme();
        }
        // Ensure the Apply method is called on the main thread
        await Application.Current.Dispatcher.InvokeAsync(() 
            => ApplicationThemeManager.Apply(currentTheme));


        // Update the settings view model to reflect the current theme
        var settingsViewModel = scope.ServiceProvider.GetRequiredService<SettingsViewModel>();
        settingsViewModel.CurrentTheme = currentTheme;
    }

    private async void OnExit(object sender, ExitEventArgs e) {
        await _host.StopAsync();
        _host.Dispose();
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
        // Log the exception
        var logger = _host.Services.GetRequiredService<ILogger<App>>();
        logger.LogError(e.Exception, "An unhandled exception occurred.");

        // Show a message box to the user
        System.Windows.MessageBox.Show(
            "An unexpected error occurred.", 
            "Error",
            System.Windows.MessageBoxButton.OK, 
            MessageBoxImage.Error
        );

        // Prevent the application from crashing
        e.Handled = true;
    }
}