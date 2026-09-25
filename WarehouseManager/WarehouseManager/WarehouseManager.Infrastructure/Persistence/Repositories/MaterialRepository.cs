using Dapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Application.Abstractions.Persistence;
using WarehouseManager.Domain.Entities;
using WarehouseManager.Infrastructure.Persistence.Ef;

namespace WarehouseManager.Infrastructure.Persistence.Repositories;

internal sealed class MaterialRepository : IMaterialRepository
{
    private readonly WarehouseDbContext _dbContext;

    public MaterialRepository(
        WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IReadOnlyList<Material>>
        GetAllAsync(
            CancellationToken cancellationToken = default)
    {
        return await _dbContext.Materials
            .AsNoTracking()
            .OrderBy(x => x.Code)
            .ToListAsync(cancellationToken);
    }


    public async Task<Material?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Materials
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }


    public async Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Materials
            .AnyAsync(
                x => x.Code == code,
                cancellationToken);
    }


    public async Task AddAsync(
        Material material,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Materials
            .AddAsync(
                material,
                cancellationToken);
    }


    public Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return _dbContext
            .SaveChangesAsync(
                cancellationToken);
    }
}