using CommunityToolkit.Mvvm.ComponentModel;

namespace WarehouseManager.Presentation.Wpf.ViewModels;

public abstract class ViewModelBase : ObservableValidator
{
    public abstract string Title { get;}
}
