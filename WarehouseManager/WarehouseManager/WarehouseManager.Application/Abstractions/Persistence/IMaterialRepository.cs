using WarehouseManager.Domain.Entities;

namespace WarehouseManager.Application.Abstractions.Persistence;

public interface IMaterialRepository
{
    Task<IReadOnlyList<Material>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Material?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Material material,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
