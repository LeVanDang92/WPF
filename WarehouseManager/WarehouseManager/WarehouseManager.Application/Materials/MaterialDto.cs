namespace WarehouseManager.Application.Materials;

public sealed record MaterialDto(long Id,
    string Code,
    string Name,
    string Unit,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
