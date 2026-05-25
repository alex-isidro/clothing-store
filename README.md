# Clothing Store API - CP3 .NET

Projeto acadêmico desenvolvido para o CP3 da disciplina de .NET, evoluindo a entrega do CP2 para uma API REST documentada, com Clean Architecture, Entity Framework Core, repositório genérico e tratamento global de exceções.

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
|       |-- Repositories/
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

Uso demonstrado nos controllers, por exemplo:

```txt
CategoriasController -> IRepository<Categoria>
MarcasController     -> IRepository<Marca>
ProdutosController   -> IRepository<Categoria> e IRepository<Marca>
PedidosController    -> IRepository<Endereco>
```

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

## Checklist CP3

- [x] API REST criada com controllers.
- [x] Pelo menos 3 recursos expostos via HTTP.
- [x] DTOs de request/response criados.
- [x] Controllers não injetam `DbContext` diretamente.
- [x] Clean Architecture mantida.
- [x] `IRepository<T>` criado na Application.
- [x] `Repository<T>` criado na Infrastructure.
- [x] Repositório genérico registrado na DI.
- [x] Repositório genérico usado em fluxo real.
- [x] Swagger configurado com título, versão e descrição.
- [x] XML comments habilitados.
- [x] Actions documentadas com `ProducesResponseType`.
- [x] `GlobalExceptionHandler` implementado com `IExceptionHandler`.
- [x] Erros retornam `ProblemDetails`.
- [x] README atualizado com instruções de execução.

---

## Observação

Este CP3 evolui a estrutura do CP2. As migrations, o `DbContext`, os mapeamentos e as entidades do domínio foram mantidos.
