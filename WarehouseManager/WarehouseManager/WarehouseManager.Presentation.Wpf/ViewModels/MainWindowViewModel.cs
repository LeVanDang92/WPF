using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WarehouseManager.Presentation.Wpf.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DashboardViewModel _dashboardViewModel;
    private readonly InventoryViewModel _inventoryViewModel;
    private readonly MaterialsViewModel _materialsViewModel;
    private readonly WarehousesViewModel _warehousesViewModel;

    [ObservableProperty]
    private ViewModelBase _currentViewModel;

    public MainWindowViewModel(DashboardViewModel dashboardViewModel,
        InventoryViewModel inventoryViewModel, 
        MaterialsViewModel materialsViewModel, WarehousesViewModel warehousesViewModel)
    {
        _dashboardViewModel = dashboardViewModel;
        _inventoryViewModel = inventoryViewModel;
        _materialsViewModel = materialsViewModel;
        CurrentViewModel = _dashboardViewModel;
        _warehousesViewModel = warehousesViewModel;
    }

    [RelayCommand]
    private void ShowDashboard()
    {
        CurrentViewModel = _dashboardViewModel;
    }
    
    [RelayCommand]
    private void ShowInventory()
    {
        CurrentViewModel = _inventoryViewModel;
    }

    [RelayCommand]
    private void ShowMaterials()
    {
        CurrentViewModel = _materialsViewModel;
    }

    [RelayCommand]
    private void ShowWarehouses()
    {
        CurrentViewModel = _warehousesViewModel;
    }

    public string CurrentUserName => "John Doe"; // Replace with actual user name retrieval logic

    override public string Title => "Warehouse Manager System";
}
