namespace ArgoPantry.WPF.HostBuilders;

public static class AddViewsHostBuilderExtensions {
    public static IHostBuilder AddViews(this IHostBuilder host) {
        host.ConfigureServices(services => {
            services.AddSingleton<INavigationWindow, MainWindow>();
            
            // Main navigation menu items
            services.AddSingleton<DashboardPage>();
            services.AddSingleton<OrdersPage>();
            services.AddSingleton<StudentsPage>();
            services.AddSingleton<ItemsPage>();
            services.AddSingleton<TransactionsPage>();
            services.AddSingleton<ImportPage>();
            services.AddSingleton<SettingsPage>();
        });
        return host;
    }
}