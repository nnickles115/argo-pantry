namespace ArgoPantry.WPF.HostBuilders; 
public static class AddServicesHostBuilderExtensions {
    public static IHostBuilder AddServices(this IHostBuilder host) {
        host.ConfigureServices(services => {
            services.AddHostedService<ApplicationHostService>();

            services.AddSingleton<ITaskBarService, TaskBarService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IContentDialogService, ContentDialogService>();

            services.AddTransient(typeof(IDataService<>), typeof(GenericDataService<>));
            services.AddTransient<OrderDataService>();
            services.AddTransient<CSVParserService>();
        });
        return host;
    }
}