# Clothing Store API — CP5 .NET

Projeto acadêmico desenvolvido para o **CP5** da disciplina de **.NET**, evoluindo a solução dos CPs anteriores com **Versionamento de API** (convivência de contratos v1 deprecado e v2 ativo), **Paginação cortada no banco via `IQueryable`** e **Rate Limit nativo com Fixed Window**, preservando integralmente a Clean Architecture, Entity Framework Core, PostgreSQL, Health Checks (`GET /health`), logs estruturados com `traceId`, tratamento global de exceções (RFC 7807) e testes automatizados com xUnit e Moq.

---

## Integrantes do Grupo

| Nome | RM | Turma | GitHub | LinkedIn |
|---|---|---|---|---|
| **Alexander Dennis Isidro Mamani** | **565554** | 2TDSPG | [alex-isidro](https://github.com/alex-isidro) | [LinkedIn](https://www.linkedin.com/in/alexander-dennis-a3b48824b/) |
| **Kelson Zhang** | **563748** | 2TDSPG | [KelsonZh0](https://github.com/KelsonZh0) | [LinkedIn](https://www.linkedin.com/in/kelson-zhang-211456323/) |

---

## Domínio Escolhido

**Loja de Roupas (Clothing Store)**

O sistema modela e gerencia o catálogo e operações de uma loja de vestuário, contemplando clientes, endereços, marcas, categorias, produtos, pedidos, itens de pedido, estoque e pagamentos.

---

## Tecnologias e Pacotes Utilizados

- **.NET 10** / C#
- **ASP.NET Core Web API**
- **Asp.Versioning.Mvc** e **Asp.Versioning.Mvc.ApiExplorer** (Versionamento de API)
- **Microsoft.AspNetCore.RateLimiting** (Rate Limiting nativo do ASP.NET Core)
- **Entity Framework Core 10** & **Npgsql.EntityFrameworkCore.PostgreSQL**
- **Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore**
- **Swashbuckle.AspNetCore** (Swagger / OpenAPI com suporte a múltiplas versões)
- **xUnit** & **Moq** (Testes de unidade em Domain e Application)
- **Clean Architecture** e GUID como chave primária

---

## Estrutura da Solução

```txt
clothing-store/
|-- ClothingStore.API/                          # Camada de apresentação e composição HTTP
|   |-- Controllers/                            # Controllers versionados e endpoints
|   |   |-- CategoriasController.cs
|   |   |-- ClientesController.cs
|   |   |-- MarcasController.cs
|   |   |-- PedidosController.cs
|   |   `-- ProdutosController.cs               # Recurso versionado (v1 e v2) + Rate Limit
|   |-- Exceptions/                             # GlobalExceptionHandler (RFC 7807)
|   |-- Extensions/                             # DI, Swagger Multi-versão e Health Checks
|   |   |-- ClothingStoreServiceCollectionExtensions.cs
|   |   `-- ConfigureSwaggerOptions.cs          # Configuração dos docs v1 e v2 no Swagger
|   |-- Health/                                 # HealthCheckResponseWriter
|   |-- Program.cs                              # Configuração de Versionamento, Rate Limiting, DI
|   `-- appsettings.json
|
|-- ClothingStore.Application/                  # Casos de uso, DTOs e regras de orquestração
|   |-- DTOs/
|   |   |-- Categorias/
|   |   |-- Clientes/
|   |   |-- Marcas/
|   |   |-- Pedidos/
|   |   |-- Produtos/
|   |   |-- PagedResponse.cs                    # Envelope paginado da v2
|   |   `-- PaginationQuery.cs                  # Parâmetros de consulta e validação de página
|   |-- Interfaces/
|   |   |-- Repositories/                       # IRepository<T> com GetPagedAsync
|   |   `-- Services/                           # IProdutoService
|   `-- Services/
|       `-- ProdutoService.cs                   # Compartilhado entre v1 e v2 (sem duplicação de regra)
|
|-- ClothingStore.Domain/                       # Entidades puras, regras de negócio e exceções
|   |-- Commom/                                 # BaseEntity (Id, CreatedAt, Active)
|   |-- Entities/                               # Cliente, Produto, Pedido, Categoria, Marca, etc.
|   `-- Exceptions/                             # DomainException, ResourceNotFoundException, etc.
|
|-- ClothingStore.Infrastructure/               # Persistência EF Core e repositórios
|   |-- Migrations/
|   `-- Persistence/
|       |-- ClothingStoreContext.cs
|       |-- configuration/
|       `-- Repositories/                       # Repository<T> (Count + OrderBy + Skip + Take no IQueryable)
|
|-- ClothingStore.Domain.Tests/                 # Testes unitários do Domínio (xUnit)
|-- ClothingStore.Application.Tests/            # Testes unitários da Aplicação (xUnit + Moq)
|-- docs/                                       # Evidências do CP5 e banco de dados
|   |-- CP5-evidencias.pdf
|   `-- banco/
`-- clothing store.sln
```

---

## SGBD e Configuração do Banco

O projeto utiliza **PostgreSQL**. A connection string deve ser informada via **User Secrets** ou variável de ambiente.

Exemplo via User Secrets:

```bash
dotnet user-secrets --project ClothingStore.API/ClothingStore.API.csproj set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=clothing_store;Username=postgres;Password=SUA_SENHA"
```

Aplicar as migrations:

```bash
dotnet ef database update --project ClothingStore.Infrastructure/ClothingStore.Infrastructure.csproj --startup-project ClothingStore.API/ClothingStore.API.csproj
```

---

## Como Executar a API

Restaurar dependências:

```bash
dotnet restore
```

Executar a aplicação:

```bash
dotnet run --project ClothingStore.API/ClothingStore.API.csproj
```

### URLs Principais

| Endpoint | Descrição |
|---|---|
| `http://localhost:5092/swagger` | Swagger UI com seletor de versões (`v1.0` deprecada e `v2.0` ativa) |
| `http://localhost:5092/health` | Health Check (não sujeito a rate limit) |
| `GET /api/produtos` | Listagem **v2** padrão (envelope paginado) |
| `GET /api/produtos?api-version=1.0` | Listagem **v1** deprecada (array / lista plana) |
| `GET /api/produtos` com header `X-Api-Version: 1.0` | Listagem **v1** deprecada via header |

---

# 📌 Implementações do CP5

---

## 1. Versionamento de API (Convivência de Contratos)

O recurso escolhido para versionamento é **`/api/produtos`**.

### Configuração no `Program.cs`:
- `AddApiVersioning`:
  - `DefaultApiVersion = new ApiVersion(2.0)`
  - `AssumeDefaultVersionWhenUnspecified = true` (requisição sem versão cai automaticamente na **2.0**)
  - `ReportApiVersions = true` (adiciona headers `api-supported-versions` e `api-deprecated-versions` na resposta)
  - `ApiVersionReader.Combine`: suporta query string `api-version` e header `X-Api-Version`.
- `AddApiExplorer`:
  - `GroupNameFormat = "'v'VVVV"` para agrupamento correto no Swagger.

### Contratos Disponíveis no Recurso:

| Versão | Status | Formato do `GET /api/produtos` | Como Chamar |
|---|---|---|---|
| **v1.0** | **Deprecada** | Array direto de produtos `[ ... ]` (contrato CP3) | `?api-version=1.0` ou Header `X-Api-Version: 1.0` |
| **v2.0** | **Atual (Default)** | Envelope paginado `{ "page": 1, "pageSize": 20, "totalItems": ..., "items": [ ... ] }` | Sem versão, `?api-version=2.0` ou Header `X-Api-Version: 2.0` |

### Exemplos de Chamada de Versão:

1. **Via Query String (v1 deprecada):**
   ```http
   GET /api/produtos?api-version=1.0
   ```

2. **Via Header HTTP (v1 deprecada):**
   ```http
   GET /api/produtos
   X-Api-Version: 1.0
   ```

3. **Omissão de versão (cai automaticamente na v2.0):**
   ```http
   GET /api/produtos
   ```

### Headers de Resposta de Versionamento:
Em todas as respostas, a API retorna os headers informativos:
```http
api-supported-versions: 1.0, 2.0
api-deprecated-versions: 1.0
```

### Swagger Multi-versão:
Em ambiente de desenvolvimento, o Swagger UI (acessível em `http://localhost:5092/swagger`) possui um dropdown no canto superior direito para alternar entre:
- **`Clothing Store API v1.0`**: Exibe a v1 explicitamente marcada com *"Esta versão está deprecada. Use a versão 2.0."*.
- **`Clothing Store API v2.0`**: Exibe a v2 como versão atual.

Os demais controllers (`Categorias`, `Marcas`, `Clientes`, `Pedidos`) permanecem operacionais e acessíveis normalmente.

---

## 2. Paginação Cortada no Banco (v2)

A listagem **v2** de produtos implementa paginação com corte direto no banco de dados via `IQueryable`. A versão **v1** permanece sem paginação para evitar *breaking changes* em clientes legados.

### Parâmetros de Consulta (Query String):

| Parâmetro | Padrão | Tipo / Regra | Comportamento em Caso de Erro |
|---|---|---|---|
| `page` | `1` | Inteiro $\ge 1$ | `page < 1` $\rightarrow$ **HTTP 400 Bad Request** |
| `pageSize` | `20` | Inteiro de **1 a 100** | `pageSize < 1` ou `pageSize > 100` $\rightarrow$ **HTTP 400 Bad Request** |

### Envelope da Resposta v2 (HTTP 200 OK):

```json
{
  "items": [
    {
      "id": "b3d8c11e-249e-4b67-8977-c6b75c879944",
      "marcaId": "8f6c483a-4a25-4c07-b648-52fb58f69188",
      "categoriaId": "2c92e92c-0e78-4bf8-b996-ebdeffb35e07",
      "nome": "Camiseta Básica",
      "descricao": "Camiseta de algodão",
      "preco": 79.90,
      "tamanho": "M",
      "cor": "Preta",
      "ativo": true
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalItems": 45,
  "totalPages": 3,
  "hasPrevious": false,
  "hasNext": true
}
```

- `totalPages` é calculado como $\lceil \text{totalItems} / \text{pageSize} \rceil$.
- Solicitar uma página além do total (ex: `page=999`) retorna **HTTP 200 OK** com `"items": []` e os totais preservados.

### Resposta de Erro de Validação (HTTP 400 Bad Request — Problem Details):

```http
GET /api/produtos?page=0
```
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Parâmetros de paginação inválidos",
  "status": 400,
  "detail": "page deve ser maior ou igual a 1."
}
```

```http
GET /api/produtos?pageSize=9999
```
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Parâmetros de paginação inválidos",
  "status": 400,
  "detail": "pageSize deve estar entre 1 e 100."
}
```

### Arquitetura da Paginação:
- **Controller (`ProdutosController`):** Recebe `PaginationQuery`, valida o intervalo e invoca o serviço de aplicação.
- **Application (`ProdutoService`, `PagedResponse<T>`, `PaginationQuery`):** Centraliza os DTOs do envelope e orquestra a chamada.
- **Infrastructure (`Repository<T>`):** Executa a paginação no banco via LINQ/EF Core:
  ```csharp
  var query = _dbSet.AsNoTracking();
  var totalItems = await query.CountAsync(cancellationToken);
  var items = await query
      .OrderBy(entity => entity.CreatedAt)
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync(cancellationToken);
  ```
  A ordenação estável (`OrderBy(entity => entity.CreatedAt)`) garante reprodutibilidade entre páginas sem sobreposição.

---

## 3. Rate Limiting (Teto de Frequência)

A API utiliza o middleware nativo do ASP.NET Core (`Microsoft.AspNetCore.RateLimiting`) para proteção contra requisições excessivas.

### Política Configurada:
- **Nome da Política:** `produtos-write`
- **Algoritmo:** **Fixed Window** (`AddFixedWindowLimiter`)
- **Janela de Tempo:** **1 minuto** (`TimeSpan.FromMinutes(1)`)
- **Limite de Permissões (`PermitLimit`):** **10 requisições**
- **Fila (`QueueLimit`):** `0` (rejeição imediata ao exceder)
- **Endpoint Aplicado:** `POST /api/produtos` (via atributo `[EnableRateLimiting("produtos-write")]`)

### Resposta ao Exceder o Limite (HTTP 429 Too Many Requests):

Quando um cliente dispara mais de 10 requisições no intervalo de 1 minuto no endpoint de escrita de produtos, a API rejeita com status **429**, envia o header **`Retry-After`** e um corpo JSON explicativo:

```http
HTTP/1.1 429 Too Many Requests
Content-Type: application/json
Retry-After: 60
```

```json
{
  "title": "Too Many Requests",
  "status": 429,
  "detail": "Limite de 10 requisições por minuto excedido para este endpoint."
}
```

### Isolamento do Health Check:
O endpoint **`GET /health`** **não** possui limitação de taxa e permanece fora do rate limiter. Mesmo após estourar as 10 requisições do `POST /api/produtos`, chamadas para `/health` continuam respondendo **HTTP 200 OK** imediatamente.

---

## Endpoints Gerais da API

### Categorias
```txt
GET  /api/categorias
GET  /api/categorias/{id}
POST /api/categorias
```

### Marcas
```txt
GET  /api/marcas
GET  /api/marcas/{id}
POST /api/marcas
```

### Produtos (Recurso Versionado e Limitado)
```txt
GET  /api/produtos?api-version=1.0          # v1 deprecada (array)
GET  /api/produtos                          # v2 atual (envelope paginado)
GET  /api/produtos?page=1&pageSize=20       # v2 com parâmetros
GET  /api/produtos/{id}                     # v1 e v2
POST /api/produtos                          # v2 com Rate Limit (10 req/min)
```

### Clientes
```txt
GET  /api/clientes
GET  /api/clientes/{id}
POST /api/clientes
```

### Pedidos
```txt
GET  /api/pedidos
GET  /api/pedidos/{id}
GET  /api/pedidos/cliente/{clienteId}
GET  /api/pedidos/status/{status}
POST /api/pedidos
```

### Health Check (CP4)
```txt
GET /health
```

---

## Exemplos de Requisição

### Criar Produto (`POST /api/produtos` — v2, sujeito ao Rate Limit)

```http
POST /api/produtos
Content-Type: application/json
```

```json
{
  "marcaId": "8f6c483a-4a25-4c07-b648-52fb58f69188",
  "categoriaId": "2c92e92c-0e78-4bf8-b996-ebdeffb35e07",
  "nome": "Camiseta Básica Oversized",
  "descricao": "Camiseta 100% algodão fio 30.1 penteado",
  "preco": 89.90,
  "tamanho": "G",
  "cor": "Off-White"
}
```

---

## Tratamento Global de Exceções (RFC 7807)

O `GlobalExceptionHandler` intercepta exceções não tratadas e formata a resposta no padrão `application/problem+json`:

| Exceção | Status HTTP | Motivo |
|---|---:|---|
| `ArgumentException` | 400 | Erro de validação ou argumento inválido |
| `DomainException` | 400 | Violação de regra de negócio no domínio |
| `ResourceNotFoundException` | 404 | Recurso não encontrado por ID |
| `KeyNotFoundException` | 404 | Chave não encontrada |
| `ConflictException` | 409 | Conflito de unicidade (ex: CPF/e-mail já cadastrado) |
| Demais exceções | 500 | Erro interno não previsto (detalhes ocultos em produção) |

---

## Testes Automatizados

A solução conta com duas suítes de testes unitários automatizados (xUnit + Moq):

1. **`ClothingStore.Domain.Tests`**:
   - Validação da entidade `Produto` (criação válida com `[Fact]`, preço inválido/negativo com `[Theory]` + `[InlineData]`).
2. **`ClothingStore.Application.Tests`**:
   - `ProdutoServiceTests`:
     - Criação com categoria inexistente $\rightarrow$ lança `ResourceNotFoundException` e garante `Times.Never` no repositório.
     - Criação com dados válidos $\rightarrow$ garante `Times.Once` no repositório.
     - `GetPagedAsync` com parâmetros inválidos (`page <= 0`, `pageSize <= 0`, `pageSize > 100`) $\rightarrow$ lança `ArgumentException` e garante `Times.Never`.
     - `GetPagedAsync` com parâmetros válidos $\rightarrow$ retorna itens e total correto chamando `Times.Once`.

### Execução dos Testes:

Na raiz da solução:

```bash
dotnet test
```

---

## Evidências de Testes

As evidências de execução e validação do CP5 estão documentadas em:

```txt
docs/CP5-evidencias.pdf
```

Itens contemplados no documento:
1. Resposta `GET /api/produtos?api-version=1.0` (array v1) com headers `api-supported-versions` e `api-deprecated-versions` explícitos.
2. Resposta `GET /api/produtos` (envelope v2).
3. Demonstração de listagem com paginação.
4. Respostas de erro HTTP 400 para `page=0` e `pageSize=9999`.
5. Estouro do rate limit com HTTP 429, header `Retry-After: 60` e JSON de rejeição.
6. `GET /health` respondendo HTTP 200 imediatamente após o estouro do rate limit.
7. Prints do Swagger UI evidenciando os grupos v1.0 (deprecada) e v2.0.
8. Execução dos testes automatizados com `dotnet test`.

---

## Checklist de Conformidade CP5

- [x] Pacotes `Asp.Versioning.Mvc` e `Asp.Versioning.Mvc.ApiExplorer` instalados e configurados.
- [x] Default API version definida como **2.0** (`AssumeDefaultVersionWhenUnspecified = true`).
- [x] Suporte a versão via query string `api-version` e header `X-Api-Version`.
- [x] Headers `api-supported-versions` e `api-deprecated-versions` emitidos na resposta.
- [x] **v1 (deprecada)** entrega a listagem antiga em formato de array plano.
- [x] **v2 (atual)** entrega o envelope paginado (`items`, `page`, `pageSize`, `totalItems`, `totalPages`, `hasPrevious`, `hasNext`).
- [x] Mesma `ProdutoService` reutilizada entre v1 e v2 sem duplicação de regras de negócio.
- [x] Paginação executada no banco via `IQueryable` (`Count` + `OrderBy` + `Skip` + `Take`).
- [x] Validação de `page < 1` ou `pageSize` fora de 1–100 retornando **HTTP 400 Bad Request**.
- [x] Página além do total retornando **HTTP 200 OK** com `items: []`.
- [x] Rate limiting nativo configurado com Fixed Window (10 requisições / 1 minuto).
- [x] Rejeição do rate limit com **HTTP 429 Too Many Requests**, header `Retry-After: 60` e payload JSON.
- [x] **`GET /health`** permanece de fora do rate limit e continua respondendo HTTP 200.
- [x] Swagger configurado com múltiplos documentos (v1 marcada como deprecada, v2 como ativa).
- [x] Testes unitários com xUnit e Moq cobrindo domínio, aplicação e regras de paginação.
- [x] README e documentação de evidências em `docs/CP5-evidencias.pdf` atualizados.
- [x] Arquitetura Clean Architecture e entregas dos CPs 1, 2, 3 e 4 preservadas.
