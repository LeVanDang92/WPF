using Dapper;
using WarehouseManager.Application.Abstractions.Persistence;
using WarehouseManager.Domain.Entities;

namespace WarehouseManager.Infrastructure.Persistence.Repositories;

internal sealed class MaterialRepository : IMaterialRepository
{
    private readonly SqlConnectionFactory _connectionFactory;
    public MaterialRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<Material>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                Id,
                Code,
                Name,
                Unit,
                IsActive,
                CreatedAt,
                UpdatedAt
            FROM dbo.Materials
            ORDER BY Code;
            """;

        await using var connection =
           _connectionFactory.Create();

        var command =
           new CommandDefinition(
               sql,
               cancellationToken:
                   cancellationToken);


        var rows = await connection
                .QueryAsync<MaterialRow>(
                    command);

        return rows
           .Select(ToDomain)
           .ToList();
    }

    public async Task<Material?> GetByIdAsync(
           long id,
           CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                Id,
                Code,
                Name,
                Unit,
                IsActive,
                CreatedAt,
                UpdatedAt
            FROM dbo.Materials
            WHERE Id = @Id;
            """;


        await using var connection =
            _connectionFactory.Create();


        var command =
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken:
                    cancellationToken);


        var row =
            await connection
                .QuerySingleOrDefaultAsync<MaterialRow>(
                    command);


        return row is null
            ? null
            : ToDomain(row);
    }


    public async Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                COUNT_BIG(1)
            FROM dbo.Materials
            WHERE Code = @Code;
            """;


        await using var connection =
            _connectionFactory.Create();


        var command =
            new CommandDefinition(
                sql,
                new
                {
                    Code = code
                },
                cancellationToken:
                    cancellationToken);


        var count =
            await connection
                .ExecuteScalarAsync<long>(
                    command);


        return count > 0;
    }


    public async Task<long> InsertAsync(
        Material material,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO dbo.Materials
            (
                Code,
                Name,
                Unit,
                IsActive,
                CreatedAt
            )
            VALUES
            (
                @Code,
                @Name,
                @Unit,
                @IsActive,
                @CreatedAt
            );

            SELECT CAST(
                SCOPE_IDENTITY()
                AS BIGINT);
            """;


        await using var connection =
            _connectionFactory.Create();


        var command =
            new CommandDefinition(
                sql,
                new
                {
                    material.Code,
                    material.Name,
                    material.Unit,
                    material.IsActive,
                    material.CreatedAt
                },
                cancellationToken:
                    cancellationToken);


        return await connection
            .ExecuteScalarAsync<long>(
                command);
    }


    public async Task UpdateAsync(
        Material material,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE dbo.Materials
            SET
                Name = @Name,
                Unit = @Unit,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;
            """;


        await using var connection =
            _connectionFactory.Create();


        var command =
            new CommandDefinition(
                sql,
                new
                {
                    material.Id,
                    material.Name,
                    material.Unit,
                    material.UpdatedAt
                },
                cancellationToken:
                    cancellationToken);


        var affectedRows =
            await connection
                .ExecuteAsync(command);


        if (affectedRows != 1)
        {
            throw new InvalidOperationException(
                $"Material {material.Id} could not be updated.");
        }
    }


    public async Task SetActiveAsync(
        long id,
        bool isActive,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE dbo.Materials
            SET
                IsActive = @IsActive,
                UpdatedAt = SYSDATETIME()
            WHERE Id = @Id;
            """;


        await using var connection =
            _connectionFactory.Create();


        var command =
            new CommandDefinition(
                sql,
                new
                {
                    Id = id,
                    IsActive = isActive
                },
                cancellationToken:
                    cancellationToken);


        var affectedRows =
            await connection
                .ExecuteAsync(command);


        if (affectedRows != 1)
        {
            throw new InvalidOperationException(
                $"Material {id} could not be updated.");
        }
    }

    private sealed class MaterialRow
    {
        public long Id { get; init; }

        public string Code { get; init; }
            = string.Empty;

        public string Name { get; init; }
            = string.Empty;

        public string Unit { get; init; }
            = string.Empty;

        public bool IsActive { get; init; }

        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }

    private static Material ToDomain(MaterialRow row)
    {
        return new Material(
            row.Id,
            row.Code,
            row.Name,
            row.Unit,
            row.IsActive,
            row.CreatedAt,
            row.UpdatedAt);
    }
}