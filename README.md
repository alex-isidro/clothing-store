# Clothing Store API - CP4 .NET

Projeto acadêmico desenvolvido para o CP4 da disciplina de .NET, evoluindo a entrega do CP3 com **Health Checks**, **logs estruturados com `traceId`** e **testes automatizados com xUnit e Moq**, mantendo a Clean Architecture, Entity Framework Core, PostgreSQL, repositório genérico e tratamento global de exceções.

---

## Integrantes do Grupo

- **Alexander Dennis Isidro** - **RM565554**
- **Kelson Zhang** - **RM563748**

---

## Domínio Escolhido

**Loja de Roupas (Clothing Store)**

O sistema representa a base de uma loja de roupas, permitindo modelar e consultar clientes, produtos, categorias, marcas, pedidos, pagamentos, endereços e estoque.

---

## Tecnologias Utilizadas

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Swagger / OpenAPI com Swashbuckle
- Clean Architecture
- GUID como chave primária

---

## Estrutura do Projeto

```txt
clothing-store/
|-- ClothingStore.API/
|   |-- Controllers/
|   |-- Exceptions/
|   |-- Program.cs
|   |-- appsettings.json
|
|-- ClothingStore.Application/
|   |-- DTOs/
|   |-- Interfaces/
|   |   |-- Repositories/
|   |   `-- Services/
|   `-- Services/
|
|-- ClothingStore.Domain/
|   |-- Commom/
|   |-- Entities/
|   |-- Exceptions/
|
|-- ClothingStore.Infrastructure/
|   |-- Persistence/
|   |   |-- ClothingStoreContext.cs
|   |   |-- configuration/
|   |   |-- Repositories/
|   |-- Migrations/
|
|-- docs/
|   |-- mer.pdf
|   |-- banco/
|   |-- cp3-testes.md
|
|-- README.md
`-- clothing store.sln
```

---

## Entidades Modeladas

- Cliente
- Endereco
- Pedido
- ItemPedido
- Produto
- Categoria
- Marca
- Pagamento
- Estoque

Todas as entidades utilizam **GUID** como chave primária.

---

## Resumo dos Relacionamentos

| Relacionamento | Cardinalidade | Observação |
|---|---:|---|
| Cliente - Endereco | 1:N | Um cliente pode possuir vários endereços. |
| Cliente - Pedido | 1:N | Um cliente pode realizar vários pedidos. |
| Endereco - Pedido | 1:N | Um endereço pode ser usado em vários pedidos. |
| Pedido - ItemPedido | 1:N | Um pedido possui vários itens. |
| Produto - ItemPedido | 1:N | Um produto pode aparecer em vários itens. |
| Pedido - Produto | N:N | Resolvido pela entidade associativa ItemPedido. |
| Categoria - Produto | 1:N | Uma categoria possui vários produtos. |
| Marca - Produto | 1:N | Uma marca possui vários produtos. |
| Pedido - Pagamento | 1:1 | Pagamento opcional para o pedido. |
| Produto - Estoque | 1:1 | Estoque opcional para o produto. |

O diagrama MER está disponível em `docs/mer.pdf`.

---

## SGBD Usado

O projeto utiliza **PostgreSQL**, mantendo a persistência criada no CP2 com Entity Framework Core e migrations.

A connection string não deve ser commitada com senha real. Configure via **User Secrets** ou variável de ambiente.

Exemplo com User Secrets:

```bash
dotnet user-secrets --project ClothingStore.API/ClothingStore.API.csproj set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=clothing_store;Username=postgres;Password=SUA_SENHA"
```

Aplicar migrations:

```bash
dotnet ef database update --project ClothingStore.Infrastructure/ClothingStore.Infrastructure.csproj --startup-project ClothingStore.API/ClothingStore.API.csproj
```

---

## Como Executar a API

Na raiz da solução, execute:

```bash
dotnet restore
```

Depois rode a API:

```bash
dotnet run --project ClothingStore.API/ClothingStore.API.csproj
```

Acesse o Swagger em:

```txt
https://localhost:<porta>/swagger
```

Também pode aparecer em HTTP, dependendo do perfil de execução local:

```txt
http://localhost:<porta>/swagger
```

---

## Endpoints Expostos no CP3

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

### Produtos

```txt
GET  /api/produtos
GET  /api/produtos/{id}
POST /api/produtos
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

---

## Exemplos de Requisição

### Criar Categoria

```http
POST /api/categorias
Content-Type: application/json
```

```json
{
  "nome": "Camisetas",
  "descricao": "Categoria de camisetas masculinas e femininas"
}
```

### Criar Marca

```http
POST /api/marcas
Content-Type: application/json
```

```json
{
  "nome": "Nike",
  "descricao": "Marca de roupas e acessórios esportivos"
}
```

### Criar Produto

Antes de criar um produto, crie uma marca e uma categoria e use os respectivos IDs retornados.

```http
POST /api/produtos
Content-Type: application/json
```

```json
{
  "marcaId": "ID_DA_MARCA",
  "categoriaId": "ID_DA_CATEGORIA",
  "nome": "Camiseta Básica",
  "descricao": "Camiseta de algodão",
  "preco": 79.90,
  "tamanho": "M",
  "cor": "Preta"
}
```

### Criar Cliente

```http
POST /api/clientes
Content-Type: application/json
```

```json
{
  "nome": "Maria Silva",
  "cpf": "12345678901",
  "email": "maria@email.com",
  "telefone": "11999999999"
}
```

### Criar Pedido

Antes de criar um pedido, já deve existir um cliente e um endereço no banco.

```http
POST /api/pedidos
Content-Type: application/json
```

```json
{
  "clienteId": "ID_DO_CLIENTE",
  "enderecoEntregaId": "ID_DO_ENDERECO",
  "status": "Criado",
  "valorTotal": 199.90,
  "dataPedido": "2026-05-24T12:00:00Z"
}
```

---

## DTOs

A API não expõe entidades de domínio diretamente nos endpoints.

Foram criados DTOs de request e response na camada **Application**:

```txt
ClothingStore.Application/DTOs/Categorias
ClothingStore.Application/DTOs/Marcas
ClothingStore.Application/DTOs/Produtos
ClothingStore.Application/DTOs/Clientes
ClothingStore.Application/DTOs/Pedidos
```

Exemplo:

```txt
CategoriaRequest
CategoriaResponse
ProdutoRequest
ProdutoResponse
ClienteRequest
ClienteResponse
PedidoRequest
PedidoResponse
ItemPedidoResponse
```

---

## Repositório Genérico

O CP3 solicita um contrato genérico de acesso a dados. O projeto implementa:

```txt
ClothingStore.Application/Interfaces/Repositories/IRepository.cs
ClothingStore.Infrastructure/Persistence/Repositories/Repository.cs
```

O contrato possui operações comuns:

```txt
GetAllAsync
GetByIdAsync
AddAsync
Update
Remove
ExistsAsync
```

Registro no `Program.cs`:

```csharp
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
```

O contrato continua disponível para os fluxos que precisam de operações genéricas. No CP4, a criação de produtos foi movida para `ProdutoService`, que recebe as interfaces de repositório pela Application, mantendo o controller focado na camada HTTP.

Os repositórios específicos continuam existindo para consultas mais próprias do domínio:

```txt
IClienteRepository
IProdutoRepository
IPedidoRepository
```

---

## Swagger / OpenAPI

O Swagger foi configurado no `Program.cs` com:

- `AddEndpointsApiExplorer()`;
- `AddSwaggerGen()`;
- metadados da API: título, versão e descrição;
- `IncludeXmlComments()`;
- `UseSwagger()`;
- `UseSwaggerUI()`.

O projeto `ClothingStore.API.csproj` habilita a geração de XML comments:

```xml
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

As actions dos controllers possuem comentários XML e atributos como:

```csharp
[ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
```

---

## Tratamento Global de Exceções

O projeto possui um handler global em:

```txt
ClothingStore.API/Exceptions/GlobalExceptionHandler.cs
```

Ele implementa `IExceptionHandler` e retorna erros no padrão **RFC 7807** usando `ProblemDetails` com o content type:

```txt
application/problem+json
```

Registro no `Program.cs`:

```csharp
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
app.UseExceptionHandler();
```

### Mapeamento de Exceções

| Exceção | Status HTTP | Motivo |
|---|---:|---|
| `ArgumentException` | 400 | Erro de validação ou argumento inválido. |
| `DomainException` | 400 | Erro de regra de domínio. |
| `ResourceNotFoundException` | 404 | Recurso não encontrado. |
| `KeyNotFoundException` | 404 | Recurso não encontrado. |
| `ConflictException` | 409 | Conflito de dados, como CPF ou e-mail duplicado. |
| Demais exceções | 500 | Erro interno genérico, sem expor detalhes em produção. |

Exemplos de retorno estão em:

```txt
docs/cp3-testes.md
```

---


---

# CP4 — Health Checks, Observabilidade e Testes

O CP4 evolui a mesma solução do CP3. As migrations, o `DbContext`, os controllers, os DTOs, o `IRepository<T>` e o `GlobalExceptionHandler` foram mantidos.

## 1. Health Check

A API expõe **somente `GET /health`** para verificar a disponibilidade operacional.

Foram registrados dois checks:

| Check | Função | Status |
|---|---|---|
| `self` | Confirma que o processo da API está em execução. | `Healthy` |
| `database` | Verifica a conectividade do `ClothingStoreContext` com o PostgreSQL. | `Healthy` / `Unhealthy` |

A implementação segue a abordagem **A** do enunciado: `AddDbContextCheck<ClothingStoreContext>()`, usando o pacote `Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore`.

A resposta de `/health` é JSON e contém:

- `status` geral;
- `duration` da execução;
- lista de `checks`;
- nome e status de cada check;
- duração de cada check;
- mensagem da exceção somente em **Development**.

Os códigos HTTP são:

```txt
Healthy   -> 200
Degraded  -> 200
Unhealthy -> 503
```

Exemplo de chamada:

```http
GET /health
```

Exemplo de estrutura da resposta:

```json
{
  "status": "Healthy",
  "duration": "00:00:00.1234567",
  "checks": [
    {
      "name": "self",
      "status": "Healthy",
      "description": "API em execução.",
      "duration": "00:00:00.0001234",
      "error": null
    },
    {
      "name": "database",
      "status": "Healthy",
      "description": null,
      "duration": "00:00:00.0987654",
      "error": null
    }
  ]
}
```

### Validação de falha do banco

Com a API em execução e PostgreSQL disponível:

```txt
GET /health -> HTTP 200
```

Para simular a indisponibilidade do banco em ambiente local, pare o PostgreSQL ou configure uma connection string inválida sem commitar credenciais:

```txt
GET /health -> HTTP 503
```

O detalhe da exceção do banco não deve ser exposto em produção.

A implementação foi separada em:

```txt
ClothingStore.API/Health/HealthCheckResponseWriter.cs
ClothingStore.API/Extensions/ClothingStoreServiceCollectionExtensions.cs
```

---

## 2. Observabilidade e logs

O projeto utiliza `ILogger<T>` nativo do ASP.NET Core.

O fluxo de criação de produto (`POST /api/produtos`) possui:

1. log de início da operação;
2. log de sucesso;
3. propriedades nomeadas;
4. `traceId` da requisição.

Exemplo conceitual do log:

```txt
Iniciando criação de produto. MarcaId CategoriaId Nome TraceId
Produto criado com sucesso. ProdutoId TraceId
```

O `GlobalExceptionHandler` também registra exceções em nível `Error`, incluindo:

```txt
Method
Path
TraceId
Exception
```

O `traceId` é incluído no `ProblemDetails.Extensions` somente em Development. Em Production, detalhes internos e stack trace não são expostos na resposta HTTP.

O handler continua centralizando o mapeamento das exceções do CP3.

---

## 3. Serviço de aplicação

Para manter a responsabilidade de negócio fora do controller, o fluxo de criação de produto foi organizado na camada Application:

```txt
ClothingStore.Application
|-- Interfaces/Services/IProdutoService.cs
`-- Services/ProdutoService.cs
```

O `ProdutoService` recebe as interfaces de repositório por injeção de dependência e verifica:

- existência da marca;
- existência da categoria;
- criação da entidade `Produto`;
- persistência pelo `IProdutoRepository`.

O controller permanece responsável pela camada HTTP e pelos logs da requisição.

Registro na DI:

```csharp
services.AddScoped<IProdutoService, ProdutoService>();
```

---

## 4. Testes automatizados

A solution possui dois projetos de teste:

```txt
ClothingStore.Domain.Tests
ClothingStore.Application.Tests
```

### Domain Tests

O projeto referencia **somente** `ClothingStore.Domain`.

Arquivo principal:

```txt
ClothingStore.Domain.Tests/ProdutoTests.cs
```

Cobertura:

- `[Fact]` para criação de produto com dados válidos;
- `[Theory]` + `[InlineData]` para preços negativos;
- validação da `DomainException`;
- padrão AAA: Arrange / Act / Assert.

### Application Tests

O projeto referencia `ClothingStore.Application` e usa **Moq** para simular os repositórios.

Arquivo principal:

```txt
ClothingStore.Application.Tests/Services/ProdutoServiceTests.cs
```

Cenários:

- categoria inexistente → `ResourceNotFoundException`;
- quando a categoria não existe, `IProdutoRepository.AddAsync` **não é chamado** (`Times.Never`);
- criação válida → `AddAsync` chamado uma vez (`Times.Once`).

Não são usados:

- banco real nos testes;
- `DbContext` nos testes de Application;
- `WebApplicationFactory`;
- testes de controller para substituir os testes exigidos de Domain/Application.

### Executar os testes

A partir da raiz da solução:

```bash
dotnet test
```

O comando deve terminar com todos os testes passando.

---

## 5. URLs

Com a API executando localmente:

```txt
Swagger:
https://localhost:<porta>/swagger

Health:
https://localhost:<porta>/health
```

O endpoint `/health` não é um endpoint de negócio e não precisa aparecer no Swagger.

---

## 6. Evidências

As evidências do CP4 devem ser armazenadas em:

```txt
docs/
```

O arquivo:

```txt
docs/cp4-evidencias.md
```

indica quais evidências devem ser registradas:

- `/health` com API e banco saudáveis;
- `/health` com banco indisponível;
- log de `POST /api/produtos` contendo `traceId`;
- exceção tratada pelo `GlobalExceptionHandler`;
- saída do `dotnet test`.

---

## Checklist CP4

- [x] `AddHealthChecks()` registrado na API.
- [x] Check `self` implementado.
- [x] Check do PostgreSQL via `AddDbContextCheck<ClothingStoreContext>()`.
- [x] `GET /health` com relatório JSON.
- [x] `Healthy -> 200`, `Degraded -> 200`, `Unhealthy -> 503`.
- [x] Exceção do health check exibida somente em Development.
- [x] Logs estruturados com `ILogger<T>`.
- [x] `traceId` no fluxo de criação de produto.
- [x] `GlobalExceptionHandler` registra exceções com `traceId`.
- [x] `traceId` em `ProblemDetails.Extensions` em Development.
- [x] `ClothingStore.Domain.Tests` criado sem referência à Infrastructure/API.
- [x] Domain com `[Fact]` e `[Theory]` + `[InlineData]`.
- [x] `ClothingStore.Application.Tests` criado com Moq.
- [x] Application verifica `Times.Never` no caminho de erro.
- [x] Application verifica `Times.Once` no caminho feliz.
- [x] `dotnet test` documentado.
- [x] Migrations, `DbContext`, controllers, DTOs, Swagger e `GlobalExceptionHandler` do CP3 preservados.

---

## 👥 Integrantes da Equipe

| Nome | RM | Turma | GitHub | LinkedIn |
|---|---|---|---|---|
| **Alexander Dennis Isidro Mamani** | 565554 | 2TDSPG | [alex-isidro](https://github.com/alex-isidro) | [LinkedIn](https://www.linkedin.com/in/alexander-dennis-a3b48824b/) |
| **Kelson Zhang** | 563748 | 2TDSPG | [KelsonZh0](https://github.com/KelsonZh0) | [LinkedIn](https://www.linkedin.com/in/kelson-zhang-211456323/) |
