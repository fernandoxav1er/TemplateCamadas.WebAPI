# Troca de Provider de Banco de Dados

Provider padrão do template: **PostgreSQL (Npgsql)**.

O acoplamento com o provider está isolado na camada **Infrastructure**. A camada **Domain** depende apenas de abstrações (`IGenericRepository`, `ISqlExecutorRepository`, `ITransaction`) e não deve receber referência a EF Core ou a qualquer provider.

## Onde o provider aparece

| Item | Arquivo |
| --- | --- |
| Pacote NuGet do provider | `src/TemplateCamadas.Infrastructure/TemplateCamadas.Infrastructure.csproj` |
| Registro do `DbContext` (`UseNpgsql`) | `src/TemplateCamadas.API/Startup.cs` |
| Parâmetros e sintaxe SQL de procedure/function | `src/TemplateCamadas.Infrastructure/Repositories/SqlExecutorRepository.cs` |
| String de conexão | `src/TemplateCamadas.API/appsettings.json` → `ConnectionStrings:DefaultConnection` |

## Passos para trocar de provider (ex.: PostgreSQL → SQL Server)

1. **Pacote NuGet** — no `.csproj` da Infrastructure, troque `Npgsql.EntityFrameworkCore.PostgreSQL` pelo provider desejado, ex.:
   - SQL Server: `Microsoft.EntityFrameworkCore.SqlServer`
   - MySQL: `Pomelo.EntityFrameworkCore.MySql`
   - SQLite: `Microsoft.EntityFrameworkCore.Sqlite`

2. **Registro do `DbContext`** — em `Startup.cs`, ajuste o método de configuração:
   - PostgreSQL: `options.UseNpgsql(...)`
   - SQL Server: `options.UseSqlServer(...)`
   - MySQL: `options.UseMySql(..., ServerVersion.AutoDetect(...))`

3. **`SqlExecutorRepository`** — ajuste o tipo de parâmetro e a sintaxe SQL:
   - Tipo do parâmetro: `NpgsqlParameter` → `SqlParameter` (SQL Server) / `MySqlParameter`.
   - Execução de procedure/function (ver tabela de sintaxe abaixo).

4. **String de conexão** — atualize `DefaultConnection` no `appsettings.json` para o formato do novo provider.

5. **Migrations** — se já existirem migrations, elas são específicas do provider. Apague a pasta `Migrations` e gere novamente:
   ```bash
   dotnet ef migrations add Inicial -p src/TemplateCamadas.Infrastructure -s src/TemplateCamadas.API
   ```

## Diferenças de sintaxe SQL (atenção ao trocar)

A sintaxe de procedures/functions **não é portável** entre bancos:

| Operação | PostgreSQL (atual) | SQL Server |
| --- | --- | --- |
| Retornar resultado | `SELECT * FROM minha_funcao(@p)` | `EXEC minha_proc @p` |
| Executar procedure | `CALL minha_proc(@p)` | `EXEC minha_proc @p` |
| Placeholder de parâmetro | `@p` | `@p` |

Outros pontos de atenção:

- **Tipos e nomes**: PostgreSQL usa identificadores em minúsculas (dobra maiúsculas com aspas) e tipos como `uuid`, `timestamptz`, `text`; SQL Server usa `uniqueidentifier`, `datetime2`, `nvarchar`. Revise `[Column]`/`HasColumnType` das entidades.
- **`DateTime`**: PostgreSQL (`timestamptz`) exige `DateTime` em UTC (`DateTimeKind.Utc`). Salvar `DateTime.Now` local pode lançar exceção no Npgsql.
- **Sequences/Identity**: geração de chaves difere (`SERIAL`/`IDENTITY` vs `GENERATED`).
- **Case-sensitivity**: nomes de objetos são case-sensitive no PostgreSQL quando criados com aspas.

## Regra que deve ser mantida

Nunca adicione o pacote do provider (ou qualquer `Microsoft.EntityFrameworkCore*`) ao `TemplateCamadas.Domain.csproj`. Qualquer tipo específico de infraestrutura que precise ser exposto ao Domain deve passar por uma abstração na pasta `Domain/Interfaces` (como foi feito com `ITransaction`).
