using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigiVaultAPI.Data;

// Design-time dla dotnet ef: appsettings → env → user-secrets (nadpisuje Development/localhost).
public class DigiVaultDbContextFactory : IDesignTimeDbContextFactory<DigiVaultDbContext>
{
    public DigiVaultDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .AddUserSecrets(typeof(DigiVaultDbContextFactory).Assembly, optional: true)
            .Build();

        var raw = configuration.GetConnectionString("DefaultConnection");
        var connectionString = PostgresConnectionStringNormalizer.ForNpgsql(raw);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Brak ConnectionStrings:DefaultConnection. Ustaw connection string z hostingu: " +
                "zmienna środowiskowa ConnectionStrings__DefaultConnection albo " +
                "dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"...\" " +
                "(w katalogu projektu API). W repozytorium nie ma stringa do bazy w chmurze — tylko localhost w appsettings.Development.json.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<DigiVaultDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new DigiVaultDbContext(optionsBuilder.Options);
    }
}
