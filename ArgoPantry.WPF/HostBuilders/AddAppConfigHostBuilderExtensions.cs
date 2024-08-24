using ArgoPantry.WPF.Models;

namespace ArgoPantry.WPF.HostBuilders; 
public static class AddAppConfigHostBuilderExtensions {
    public static IHostBuilder AddAppConfig(this IHostBuilder host) {
        host.ConfigureServices((context, services) => {
            var appConfig = context.Configuration.GetSection("Database").Get<AppConfig>();
            services.AddSingleton<AppConfig>();
        });
        return host;
    }
}