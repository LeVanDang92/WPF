using WarehouseManager.Domain.Entities;

namespace WarehouseManager.Application.Abstractions.Persistence;

public interface IMaterialRepository
{
    Task<IReadOnlyList<Material>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Material?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code,CancellationToken cancellationToken = default);

    Task<long> InsertAsync(Material material,CancellationToken cancellationToken = default);
    Task UpdateAsync(Material material,CancellationToken cancellationToken = default);
    Task SetActiveAsync(long id, bool isActive,CancellationToken cancellationToken = default);
}
