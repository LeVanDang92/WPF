using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WarehouseManager.Infrastructure;
using WarehouseManager.Presentation.Wpf.ViewModels;
namespace WarehouseManager.Presentation.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private readonly IHost _host;

    public App()
    {
        var builder = Host.CreateApplicationBuilder();

        ConfigureServices(builder.Services, builder.Configuration);
     
        _host = builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services,IConfiguration configuration)
    {
        // Register your services and view models here
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<InventoryViewModel>();
        services.AddSingleton<MaterialsViewModel>();
        services.AddSingleton<WarehousesViewModel>();
        services.AddInfrastructure(configuration);

        // Register the main window
        services.AddSingleton<MainWindow>();     
    }

    override protected void OnStartup(StartupEventArgs e)
    {
        _host.Start();
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    override protected void OnExit(ExitEventArgs e)
    {
        _host.Dispose();
        base.OnExit(e);
    }
}
