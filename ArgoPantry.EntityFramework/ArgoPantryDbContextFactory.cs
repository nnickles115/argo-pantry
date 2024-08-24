using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ArgoPantry.EntityFramework;

// Design-time factory for creating a new instance of the DB context.
public class ArgoPantryDbContextFactory : IDesignTimeDbContextFactory<ArgoPantryDbContext> {
    public ArgoPantryDbContext CreateDbContext(string[] args = null) {
        DbContextOptionsBuilder<ArgoPantryDbContext> optionsBuilder = new();

        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var dbPath = Path.Combine(documentsPath, "ArgoPantry.db");

        //_configureDbContext(optionsBuilder);

        optionsBuilder.UseSqlite($"Data Source={dbPath}");



        return new ArgoPantryDbContext(optionsBuilder.Options);
    }
}