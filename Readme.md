# Template .NET - Camadas API

Template de **WebAPI .NET 8 em arquitetura de camadas**, para acelerar a criação de APIs consumidas por frontends ou por clientes que integram via HTTP.

---

## O que já vem pronto

- **Camadas** (`API`, `Application`, `Domain`, `Infrastructure`) com regra de dependência `API → Application → Domain ← Infrastructure`.
- **Casos de uso** (`Application/UseCases`) com base `BaseUseCase` + validação via **FluentValidation**.
- **Registro de DI por camada**: `AddDomain()`, `AddInfrastructure()`, `AddApplication()`.
- **Respostas padronizadas** (`ResponseBase`) e acúmulo de erros por request (`NotificationService`).
- **Tratamento global de exceções** (`IExceptionHandler`) no mesmo contrato de resposta.
- **Versionamento de API** por URL (`Asp.Versioning`) com Swagger por versão.
- **Swagger** (restrito a não-produção) e **HealthChecks** com UI (`/monitor`).
- **CORS configurável** por `appsettings` (`Cors:AllowedOrigins`).
- **Acesso a dados** com EF Core + **PostgreSQL (Npgsql)**: repositório genérico e executor de procedures/functions.

> Persistência é **opt-in**: `DbContext` e repositórios vêm comentados em `Infrastructure/DependencyInjection.cs`. O template compila e roda sem banco.

Documentação de arquitetura em [`docs/project_context.md`](docs/project_context.md) e troca de banco em [`docs/database-provider.md`](docs/database-provider.md).

---

## Instalar o template

```bash
dotnet new install .
```

Ou a partir de um caminho específico:

```bash
dotnet new install ./caminho/do/template
```

## Verificar a instalação

```bash
dotnet new list camadasapi
```

Saída esperada:

```
Template Name              Short Name    Language    Tags
-------------------------  ------------  ----------  --------------------------------
Template Camadas WebAPI    camadasapi    [C#]        WebAPI/Camadas/Clean Architecture
```

## Criar um novo projeto

```bash
dotnet new camadasapi -n MeuNovoProjeto -o MeuNovoProjeto.WebAPI
```

- `-n`: nome da solução/projeto (substitui `TemplateCamadas`)
- `-o`: diretório de saída

## Desinstalar

Liste os templates instalados e o comando exato de remoção:

```bash
dotnet new uninstall
```

Depois remova pelo caminho informado:

```bash
dotnet new uninstall <caminho-absoluto-do-template>
```

---

## Rodando o projeto gerado

```bash
dotnet run --project src/MeuNovoProjeto.API
```

- Swagger (Development): `https://localhost:<porta>/swagger`
- HealthCheck UI: `https://localhost:<porta>/monitor`
- Endpoint de exemplo: `GET /v1/teste`

Para habilitar banco: descomente o `AddDbContext` (Npgsql) e os repositórios em `Infrastructure/DependencyInjection.cs` e preencha `ConnectionStrings:DefaultConnection` no `appsettings.json`.

---

## Licença

Licenciado sob a [MIT License](LICENSE).
