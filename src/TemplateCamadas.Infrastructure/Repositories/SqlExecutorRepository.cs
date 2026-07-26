using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text.RegularExpressions;
using TemplateCamadas.Domain.Interfaces;

namespace TemplateCamadas.Infrastructure.Repositories;

public class SqlExecutorRepository : ISqlExecutorRepository
{
    private readonly DatabaseContext _context;

    public SqlExecutorRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<T>> FromSqlRawAsync<T>(string functionName, Dictionary<string, object> parameters) where T : class, new()
    {
        if (!Regex.IsMatch(functionName, @"^[a-zA-Z0-9_.]+$"))
            throw new ArgumentException("Invalid function name");

        var sqlParams = parameters.Select(p => new NpgsqlParameter(p.Key, p.Value ?? DBNull.Value)).ToArray();

        var sql = $"SELECT * FROM {functionName}(" + string.Join(", ", parameters.Keys.Select(k => $"@{k}")) + ")";

        return await _context.Set<T>().FromSqlRaw(sql, sqlParams).ToListAsync();
    }

    public async Task<int?> ExecuteSqlRawAsync(string procedureName, Dictionary<string, object> parameters)
    {
        if (!Regex.IsMatch(procedureName, @"^[a-zA-Z0-9_.]+$"))
            throw new ArgumentException("Invalid procedure name");

        var sqlParams = parameters.Select(p => new NpgsqlParameter(p.Key, p.Value ?? DBNull.Value)).ToArray();

        var sql = $"CALL {procedureName}(" + string.Join(", ", parameters.Keys.Select(k => $"@{k}")) + ")";

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

    public async Task<ITransaction> BeginTransactionAsync()
        => new EfTransaction(await _context.Database.BeginTransactionAsync());
}
