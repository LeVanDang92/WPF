using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using WarehouseManager.Application.Materials;

namespace WarehouseManager.Presentation.Wpf.ViewModels
{
    public sealed partial class MaterialsViewModel : ViewModelBase
    {
        public override string Title => "Materials Management";

        public MaterialService _materialService { get; }

        public ObservableCollection<MaterialDto> Materials => new();
        public ICollectionView MaterialItemsView { get; }
        public IReadOnlyList<string> Units => new List<string> { "EA", "KG", "M", "BOX" };

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(50)]
        private string code =
        string.Empty;


        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(200)]
        private string name =
            string.Empty;


        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(20)]
        private string unit =
            "EA";


        [ObservableProperty]
        private bool isActive =
            true;


        [ObservableProperty]
        [NotifyPropertyChangedFor(
            nameof(IsEditing))]
        private long? editingId;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(
            nameof(DeactivateCommand))]
        private MaterialDto?
            selectedMaterial;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(
            nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(
            nameof(DeactivateCommand))]
        private bool isBusy;


        [ObservableProperty]
        private string statusMessage =
            string.Empty;

        [ObservableProperty]
        private string errorMessage =
            string.Empty;
        public bool IsEditing =>
        EditingId.HasValue;

        public MaterialsViewModel(MaterialService materialService)
        {
            _materialService = materialService;
            MaterialItemsView = CollectionViewSource.GetDefaultView(Materials);
            MaterialItemsView.Filter = FilterMaterial;
        }

        partial void OnSearchTextChanged(string value)
        {
            MaterialItemsView.Refresh();
        }

        partial void OnSelectedMaterialChanged(
            MaterialDto? value)
        {
            DeactivateCommand
                .NotifyCanExecuteChanged();


            if (value is null)
            {
                return;
            }

            EditingId =
                value.Id;

            Code =
                value.Code;

            Name =
                value.Name;

            Unit =
                value.Unit;

            IsActive =
                value.IsActive;

            ClearErrors();
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            if (IsBusy)
            {
                return;
            }


            try
            {
                IsBusy = true;

                ErrorMessage =
                    string.Empty;

                StatusMessage =
                    "Loading materials...";


                await LoadCoreAsync();


                StatusMessage =
                    $"{Materials.Count} materials loaded.";
            }
            catch (Exception ex)
            {
                ErrorMessage =
                    ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }


        [RelayCommand]
        private void New()
        {
            ClearForm();

            ErrorMessage =
               string.Empty;

            StatusMessage =
                "New material";

            ClearErrors();
        }

        [RelayCommand(
            CanExecute =
                nameof(CanSave))]
        private async Task SaveAsync()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                ErrorMessage =
                    "Please correct the validation errors.";

                return;
            }

            try
            {
                IsBusy = true;

                ErrorMessage =
                    string.Empty;

                bool isNew = EditingId is null;

                if (EditingId is null)
                {
                    var request =
                        new CreateMaterialRequest(
                            Code,
                            Name,
                            Unit);


                    await _materialService
                        .CreateAsync(request);
                }
                else
                {
                    var request =
                        new UpdateMaterialRequest(
                            EditingId.Value,
                            Name,
                            Unit);


                    await _materialService
                        .UpdateAsync(request);

                }

                await LoadCoreAsync();

                ClearForm();

                StatusMessage =
                    isNew 
                        ? "Material created successfully."
                        : "Material updated successfully.";
            }
            catch (Exception ex)
            {
                ErrorMessage =
                    ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ClearForm()
        {
            SelectedMaterial =
                null;

            EditingId =
                null;

            Code =
                string.Empty;

            Name =
                string.Empty;

            Unit =
                "EA";

            IsActive =
                true;

            ClearErrors();
        }

        private bool CanSave()
        {
            return !IsBusy;
        }

        [RelayCommand(
            CanExecute =
                nameof(CanDeactivate))]
        private async Task DeactivateAsync()
        {
            if (SelectedMaterial is null)
            {
                return;
            }

            try
            {
                IsBusy = true;

                ErrorMessage =
                    string.Empty;


                await _materialService
                    .SetActiveAsync(
                        SelectedMaterial.Id,
                        false);

                await LoadCoreAsync();

                New();

                StatusMessage =
                    "Material deactivated.";
            }
            catch (Exception ex)
            {
                ErrorMessage =
                    ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanDeactivate()
        {
            return
                !IsBusy
                &&
                SelectedMaterial is
                {
                    IsActive: true
                };
        }

        private async Task LoadCoreAsync()
        {
            var items =
                await _materialService
                    .GetAllAsync();


            Materials.Clear();


            foreach (var item in items)
            {
                Materials.Add(item);
            }


            MaterialItemsView.Refresh();
        }

        private bool FilterMaterial(
            object obj)
        {
            if (obj is not MaterialDto material)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                SearchText))
            {
                return true;
            }

            return
                material.Code.Contains(
                    SearchText,
                    StringComparison.OrdinalIgnoreCase)
                ||
                material.Name.Contains(
                    SearchText,
                    StringComparison.OrdinalIgnoreCase);
        }
    }
}
