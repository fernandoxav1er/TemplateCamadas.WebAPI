# Contexto do Projeto — TemplateCamadas.WebAPI

## Arquitetura e regras de negócio

Template de WebAPI em camadas (.NET 8), consumido por frontends e por clientes que integram via HTTP.

Camadas e regra de dependência (API → Application → Domain ← Infrastructure):

- **API**: controllers, pipeline HTTP, configurações (DI, Swagger, HealthCheck, CORS). Composition root. Referencia Application e Infrastructure.
- **Application**: orquestração dos casos de uso (`UseCases`), DTOs (`Dtos/Requests`, `Dtos/Responses`), mapeamentos (`Mappings`) e validadores (`Validators`). Referencia apenas Domain.
- **Domain**: entidades (`Entities`), enums (`Enums`), contratos (`Interfaces`), modelos de resposta (`Models/Responses`) e infra de validação/notificação (`Services`). **Não referencia Entity Framework nem provider de banco.**
- **Infrastructure**: `DatabaseContext`, repositórios (`GenericRepository`, `SqlExecutorRepository`) e a implementação de transação (`EfTransaction`). Única camada que conhece EF Core e o provider.

Padrões vigentes:

- **Caso de uso**: cada operação de negócio é uma classe em `Application/UseCases` que herda de `BaseUseCase<TReturn, TParameters>` e implementa `Process`. A orquestração vive aqui — não no Domain.
- A infra de validação/notificação (`ValidationService`, `NotificationService`) fica no Domain e é reutilizada pelo `BaseUseCase`. `ExecuteValidations` roda um `AbstractValidator` do FluentValidation e acumula erros no `NotificationService`.
- Fluxo do exemplo: `SampleController` → `CreateSampleUseCase` (valida `CreateSampleRequest`, mapeia via `SampleMappings` para `Sample` e retorna `SampleResponse`).
- Respostas padronizadas via `ResponseBase`/`ResponseBase<T>` e `MainController.CustomResponse`.
- Erros acumulados por request no `NotificationService` (Scoped); `MainController` converte notificações em `BadRequest`.
- Acesso a dados por `IGenericRepository<TEntity>` (CRUD) e `ISqlExecutorRepository` (procedures/functions e SQL raw).
- Transações expostas por meio da abstração `ITransaction` (sem vazar tipos do EF).
- Mapeamento manual por extension methods (`Mappings`), sem AutoMapper.
- **Registro de DI por camada**: cada camada expõe seu próprio extension method e o Startup (composition root) apenas os compõe: `AddDomain()` (`NotificationService`), `AddInfrastructure(configuration)` (`DbContext` + repositórios), `AddApplication()` (casos de uso). Concerns de API (`HttpContextAccessor`) ficam em `AddWebApiConfiguration`. Não há mais `DIConfiguration` central.

## Decisões técnicas recentes

- **Versionamento de API real** via `Asp.Versioning.Mvc` (`ApiVersioningConfiguration`): versão no segmento de URL (`UrlSegmentApiVersionReader`), default `1.0`, `ReportApiVersions`. Controllers usam `[ApiVersion("1.0")]` + rota `v{version:apiVersion}/...`. Swagger gera um doc por versão (`ConfigureSwaggerOptions` + `IApiVersionDescriptionProvider`) e a UI monta um endpoint por versão. `[SwaggerOperation]` habilitado via `EnableAnnotations`.
- **Swagger restrito a não-produção.** `UseSwagger`/`UseSwaggerUI` gated por `!env.IsProduction()` em `WebApiConfiguration`. Exposto em Development/Staging, oculto em Production.
- **Tratamento global de exceções.** `GlobalExceptionHandler : IExceptionHandler` (registrado por `ExceptionHandlerConfiguration`, primeiro no pipeline) captura exceções não tratadas, loga e responde 500 no contrato `ResponseBase` (`Success=false`). Em Development expõe `exception.Message`; em produção, mensagem genérica. Substituiu o `UseDeveloperExceptionPage` — contrato unificado em todos os ambientes.
- **CORS restritivo por padrão, com atalho de liberação total.** Origens lidas de `Cors:AllowedOrigins` (`appsettings`) em `CorsConfiguration`. `[ "*" ]` ativa `AllowAnyOrigin` (full liberado); qualquer outra lista usa `WithOrigins` exato; lista vazia bloqueia cross-origin. Default do `appsettings.json` é vazio; `appsettings.Development.json` usa `[ "*" ]`. Substituído o antigo `AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()` fixo.
- **Camada Application adicionada** (`TemplateCamadas.Application`) como lar dos casos de uso. A antiga base `BaseValidationService`/`IBaseValidationService` (que misturava validação com orquestração no Domain) foi movida para `Application/UseCases` e renomeada para `BaseUseCase`/`IUseCase`. `ValidationService` e `NotificationService` permaneceram no Domain como infra compartilhada.
- **Novas pastas de organização**: Domain ganhou `Entities` e `Enums`; Application define `Dtos/{Requests,Responses}`, `Mappings`, `Validators` e `UseCases`.
- **Provider de banco padronizado em PostgreSQL (Npgsql).** Removido `Microsoft.Data.SqlClient`; `SqlExecutorRepository` usa `NpgsqlParameter` e sintaxe PostgreSQL (`SELECT * FROM func(...)` para retorno de dados, `CALL proc(...)` para procedures).
- **Domain desacoplado da infraestrutura de dados.** Pacotes `Microsoft.EntityFrameworkCore*` e `Npgsql` movidos do Domain para o Infrastructure. A dependência de EF na interface `ISqlExecutorRepository` (antes `IDbContextTransaction`) foi substituída pela abstração `ITransaction`, implementada por `EfTransaction` na Infrastructure.
- Troca de provider documentada em [database-provider.md](database-provider.md).
