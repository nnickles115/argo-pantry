namespace ArgoPantry.WPF.HostBuilders;

public static class AddConfigurationHostBuilderExtensions {
    public static IHostBuilder AddConfiguration(this IHostBuilder host) {
        host.ConfigureAppConfiguration(c => {
            c.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            c.AddEnvironmentVariables();
        });
        return host;
    }
}