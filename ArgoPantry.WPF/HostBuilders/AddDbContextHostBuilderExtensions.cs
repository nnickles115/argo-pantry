namespace ArgoPantry.WPF.HostBuilders; 

public static class AddDbContextHostBuilderExtensions {
    public static IHostBuilder AddDbContext(this IHostBuilder host) {
        host.ConfigureServices((context, services) => {
            // Get items defined in appsettings.json
            //var folderPath = context.Configuration["Database:FolderPath"];
            var fileName = context.Configuration["Database:FileName"];
            string sqliteConnection = context.Configuration.GetConnectionString("SQLiteConnection")!;

            // Check if the folder path is null
            if(fileName == null) {
                throw new ArgumentNullException(nameof(fileName));
            }
            
            // Combine the folder path and file name
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dbPath = Path.Combine(documentsPath, fileName);

            // Define the connection string
            string connectionString = $"{sqliteConnection}{dbPath}";

            // Configure the DbContext with the connection string
            void configureDbContext(DbContextOptionsBuilder o) => o.UseSqlite(connectionString);
            services.AddDbContext<ArgoPantryDbContext>(configureDbContext);
        });
        return host;
    }
}