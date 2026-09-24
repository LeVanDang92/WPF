using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManager.Application.Materials;

namespace WarehouseManager.Presentation.Wpf.ViewModels
{
    public sealed class MaterialsViewModel : ViewModelBase
    {
        public override string Title => "Materials Management";

        MaterialsViewModel(MaterialService materialService)
        {

        }
    }
}
