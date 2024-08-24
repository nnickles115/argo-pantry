namespace ArgoPantry.WPF.HostBuilders;

public static class AddViewModelsHostBuilderExtensions {
    public static IHostBuilder AddViewModels(this IHostBuilder host) {
        host.ConfigureServices(services => {
            services.AddSingleton<MainWindowViewModel>();

            // Main navigation menu items
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<OrderViewModel>();
            services.AddSingleton<StudentsViewModel>();
            services.AddSingleton<ItemsViewModel>();
            services.AddSingleton<TransactionsViewModel>();
            services.AddSingleton<ImportViewModel>();
            services.AddSingleton<SettingsViewModel>();

            // Control forms
            services.AddTransient<StudentForm>();
            services.AddTransient<ItemForm>();
        });
        return host;
    }
}