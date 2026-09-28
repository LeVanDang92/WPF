using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WarehouseManager.Infrastructure.Persistence.Ef;

public sealed class WarehouseDbContextFactory : IDesignTimeDbContextFactory<WarehouseDbContext>
{
    public WarehouseDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__WarehouseDb")
            ?? ReadConnectionString();

        var options = new DbContextOptionsBuilder<WarehouseDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new WarehouseDbContext(options);
    }

    private static string ReadConnectionString()
    {
        foreach (var startDirectory in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
        {
            for (var directory = new DirectoryInfo(startDirectory); directory is not null; directory = directory.Parent)
            {
                foreach (var path in new[]
                {
                    Path.Combine(directory.FullName, "WarehouseManager.Presentation.Wpf", "appsettings.json"),
                    Path.Combine(directory.FullName, "appsettings.json")
                })
                {
                    if (!File.Exists(path)) continue;

                    using var document = JsonDocument.Parse(File.ReadAllText(path), new JsonDocumentOptions
                    {
                        AllowTrailingCommas = true,
                        CommentHandling = JsonCommentHandling.Skip
                    });

                    if (document.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings)
                        && connectionStrings.TryGetProperty("WarehouseDb", out var warehouseDb)
                        && !string.IsNullOrWhiteSpace(warehouseDb.GetString()))
                    {
                        return warehouseDb.GetString()!;
                    }
                }
            }
        }

        throw new InvalidOperationException(
            "Connection string 'WarehouseDb' was not found. Set ConnectionStrings__WarehouseDb " +
            "or provide WarehouseManager.Presentation.Wpf/appsettings.json.");
    }
}
