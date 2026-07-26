# Contexto do Projeto — TemplateCamadas.WebAPI

## Arquitetura e regras de negócio

Template de WebAPI em camadas (.NET 8), consumido por frontends e por clientes que integram via HTTP.

Camadas e regra de dependência:

- **API**: controllers, pipeline HTTP, configurações (DI, Swagger, HealthCheck, CORS). Referencia Domain e Infrastructure.
- **Domain**: contratos (`Interfaces`), modelos de resposta (`Models/Responses`) e serviços de validação/notificação (`Services`). **Não referencia Entity Framework nem provider de banco** — depende apenas de abstrações.
- **Infrastructure**: `DatabaseContext`, repositórios (`GenericRepository`, `SqlExecutorRepository`) e a implementação de transação (`EfTransaction`). É a única camada que conhece EF Core e o provider.

Padrões vigentes:

- Respostas padronizadas via `ResponseBase`/`ResponseBase<T>` e `MainController.CustomResponse`.
- Validação/erros acumulados por request no `NotificationService` (Scoped); `MainController` converte notificações em `BadRequest`.
- Acesso a dados por `IGenericRepository<TEntity>` (CRUD) e `ISqlExecutorRepository` (procedures/functions e SQL raw).
- Transações expostas ao Domain pela abstração `ITransaction` (sem vazar tipos do EF).

## Decisões técnicas recentes

- **Provider de banco padronizado em PostgreSQL (Npgsql).** Removido `Microsoft.Data.SqlClient`; `SqlExecutorRepository` usa `NpgsqlParameter` e sintaxe PostgreSQL (`SELECT * FROM func(...)` para retorno de dados, `CALL proc(...)` para procedures).
- **Domain desacoplado da infraestrutura de dados.** Pacotes `Microsoft.EntityFrameworkCore*` e `Npgsql` movidos do Domain para o Infrastructure. A dependência de EF na interface `ISqlExecutorRepository` (antes `IDbContextTransaction`) foi substituída pela abstração `ITransaction`, implementada por `EfTransaction` na Infrastructure.
- Troca de provider documentada em [database-provider.md](database-provider.md).
