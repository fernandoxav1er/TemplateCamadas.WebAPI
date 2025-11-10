using Microsoft.EntityFrameworkCore.Storage;

namespace TemplateCamadas.Domain.Interfaces;

public interface IProcedureExecutorRepository
{
    Task<List<T>> FromSqlRawAsync<T>(string procedureName, Dictionary<string, object> parameters) where T : class, new();
    Task<int?> ExecuteSqlRawAsync(string procedureName, Dictionary<string, object> parameters);
    Task<List<T>> QuerySqlRawAsync<T>(string sql) where T : class, new();
    Task<int> ExecuteSqlRawAsync(string sql);
    Task<IDbContextTransaction> BeginTransactionAsync();
}

