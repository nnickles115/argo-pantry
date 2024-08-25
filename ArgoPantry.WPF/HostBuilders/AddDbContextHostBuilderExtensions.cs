namespace ArgoPantry.WPF.HostBuilders; 

public static class AddDbContextHostBuilderExtensions {
    public static IHostBuilder AddDbContext(this IHostBuilder host) {
        host.ConfigureServices((context, services) => {
            // Hardcoded file name and folder path
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileName = "ArgoPantry.db";
            var dbPath = Path.Combine(documentsPath, fileName);

            // Connection string
            string connectionString = $"Data Source={dbPath}";

            // Configure the DbContext with the connection string
            void configureDbContext(DbContextOptionsBuilder o) => o.UseSqlite(connectionString);
            services.AddDbContext<ArgoPantryDbContext>(configureDbContext);
        });
        return host;
    }
}