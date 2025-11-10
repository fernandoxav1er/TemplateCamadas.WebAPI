using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TemplateCamadas.Domain.Interfaces;

namespace TemplateCamadas.Infrastructure.Repositories;

public class ProcedureExecutorRepository : IProcedureExecutorRepository
{
    private readonly DatabaseContext _context;

    public ProcedureExecutorRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<T>> FromSqlRawAsync<T>(string procedureName, Dictionary<string, object> parameters) where T : class, new()
    {
        var sqlParams = parameters.Select(p => new SqlParameter(p.Key, p.Value ?? DBNull.Value)).ToArray();

        var sql = $"EXEC {procedureName} " + string.Join(", ", parameters.Keys.Select(k => $"@{k}"));

        return await _context.Set<T>().FromSqlRaw(sql, sqlParams).ToListAsync();
    }

    public async Task<int?> ExecuteSqlRawAsync(string procedureName, Dictionary<string, object> parameters)
    {
        var sqlParams = parameters.Select(p => new SqlParameter(p.Key, p.Value ?? DBNull.Value)).ToArray();

        var sql = $"EXEC {procedureName} " + string.Join(", ", parameters.Keys.Select(k => $"@{k}"));

        return await _context.Database.ExecuteSqlRawAsync(sql, sqlParams);
    }

    public async Task<List<T>> QuerySqlRawAsync<T>(string sql) where T : class, new()
    {
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<int> ExecuteSqlRawAsync(string sql)
    {
        return await _context.Database.ExecuteSqlRawAsync(sql);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
}
