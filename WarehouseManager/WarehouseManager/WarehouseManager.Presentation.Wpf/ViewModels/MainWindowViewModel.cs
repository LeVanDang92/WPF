using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WarehouseManager.Presentation.Wpf.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DashboardViewModel _dashboardViewModel;
    private readonly InventoryViewModel _inventoryViewModel;
    private readonly MaterialsViewModel _materialsViewModel;

    [ObservableProperty]
    private ViewModelBase _currentViewModel;

    public MainWindowViewModel(DashboardViewModel dashboardViewModel, InventoryViewModel inventoryViewModel, MaterialsViewModel materialsViewModel)
    {
        _dashboardViewModel = dashboardViewModel;
        _inventoryViewModel = inventoryViewModel;
        _materialsViewModel = materialsViewModel;
        _currentViewModel = _dashboardViewModel;
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
}
