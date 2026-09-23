using WarehouseManager.Application.Abstractions.Persistence;
using WarehouseManager.Domain.Entities;

namespace WarehouseManager.Application.Materials;

public sealed class MaterialService
{
    private readonly IMaterialRepository _materialRepository;

    public MaterialService(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<IReadOnlyList<MaterialDto>>
        GetAllAsync(
            CancellationToken cancellationToken = default)
    {
        var materials =
            await _materialRepository
                .GetAllAsync(cancellationToken);


        return materials
            .Select(ToDto)
            .ToList();
    }


    public async Task<long> CreateAsync(
        CreateMaterialRequest request,
        CancellationToken cancellationToken = default)
    {
        var code =
            request.Code
                .Trim()
                .ToUpperInvariant();


        var exists =
            await _materialRepository
                .ExistsByCodeAsync(
                    code,
                    cancellationToken);


        if (exists)
        {
            throw new InvalidOperationException(
                $"Material code '{code}' already exists.");
        }


        var material =
            Material.CreateNew(
                code,
                request.Name,
                request.Unit);


        return await _materialRepository
            .InsertAsync(
                material,
                cancellationToken);
    }


    public async Task UpdateAsync(
        UpdateMaterialRequest request,
        CancellationToken cancellationToken = default)
    {
        var material =
            await _materialRepository
                .GetByIdAsync(
                    request.Id,
                    cancellationToken)
            ?? throw new InvalidOperationException(
                $"Material id {request.Id} was not found.");


        material.UpdateDetails(
            request.Name,
            request.Unit);


        await _materialRepository
            .UpdateAsync(
                material,
                cancellationToken);
    }


    public async Task SetActiveAsync(
        long id,
        bool isActive,
        CancellationToken cancellationToken = default)
    {
        var material =
            await _materialRepository
                .GetByIdAsync(
                    id,
                    cancellationToken)
            ?? throw new InvalidOperationException(
                $"Material id {id} was not found.");


        material.SetActive(isActive);

        await _materialRepository
            .SetActiveAsync(
                id,
                isActive,
                cancellationToken);
    }


    private static MaterialDto ToDto(
        Material material)
    {
        return new MaterialDto(
            material.Id,
            material.Code,
            material.Name,
            material.Unit,
            material.IsActive,
            material.CreatedAt,
            material.UpdatedAt);
    }
}
