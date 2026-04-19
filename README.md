# CP1 e CP2 - Modelo Entidade-Relacionamento (MER) e WebAPI

## Integrantes do Grupo
- **Alexander Dennis Isidro** - **RM565554**
- **Kelson Zhang** - **RM563748**

---

## Domínio Escolhido
**Loja de Roupas (Clothing Store)**

Este projeto representa a base de uma loja de roupas, com foco na **modelagem das entidades**, no **diagrama MER** e na **estrutura inicial de uma WebAPI em .NET** com separação por camadas.

---

## Objetivo do Trabalho
Este repositório foi desenvolvido para:

- elaborar um **MER (Modelo Entidade-Relacionamento)** com entidades relacionadas;
- definir **atributos principais**, **chaves primárias** e **relacionamentos**;
- indicar **cardinalidade** e **opcionalidade**;
- criar a **estrutura inicial de uma WebAPI em .NET**, seguindo o padrão **Clean Architecture**;
- modelar em código as mesmas entidades representadas no MER.

---

## Entidades Modeladas
As entidades do domínio são:

- **Cliente**
- **Endereco**
- **Pedido**
- **ItemPedido**
- **Produto**
- **Categoria**
- **Marca**
- **Pagamento**
- **Estoque**

Todas as entidades utilizam **GUID** como chave primária.

---

## Resumo dos Relacionamentos

### Cliente e Endereco
- Um **Cliente** pode ter **um ou vários Endereços**.
- Cada **Endereco** pertence a **um único Cliente**.

**Cardinalidade:** `1:N`

### Cliente e Pedido
- Um **Cliente** pode realizar **um ou vários Pedidos**.
- Cada **Pedido** pertence a **um único Cliente**.

**Cardinalidade:** `1:N`

### Endereco e Pedido
- Um **Endereco** pode ser utilizado em **um ou vários Pedidos** para entrega.
- Cada **Pedido** possui **um único Endereco de entrega**.

**Cardinalidade:** `1:N`

### Pedido e ItemPedido
- Um **Pedido** possui **um ou vários ItensPedido**.
- Cada **ItemPedido** pertence a **um único Pedido**.

**Cardinalidade:** `1:N`

### Produto e ItemPedido
- Um **Produto** pode aparecer em **um ou vários ItensPedido**.
- Cada **ItemPedido** referencia **um único Produto**.

**Cardinalidade:** `1:N`

### Pedido e Produto
- Um **Pedido** pode conter **vários Produtos**.
- Um **Produto** pode estar presente em **vários Pedidos**.
- Esse relacionamento é resolvido pela entidade associativa **ItemPedido**.

**Cardinalidade:** `N:N`

### Categoria e Produto
- Uma **Categoria** pode possuir **vários Produtos**.
- Cada **Produto** pertence a **uma única Categoria**.

**Cardinalidade:** `1:N`

### Marca e Produto
- Uma **Marca** pode estar associada a **vários Produtos**.
- Cada **Produto** pertence a **uma única Marca**.

**Cardinalidade:** `1:N`

### Pedido e Pagamento
- Um **Pedido** pode possuir **zero ou um Pagamento**.
- Cada **Pagamento** pertence a **um único Pedido**.

**Cardinalidade:** `1:1`  
**Opcionalidade:** pagamento **opcional** para o pedido.

### Produto e Estoque
- Um **Produto** pode possuir **zero ou um Estoque**.
- Cada **Estoque** pertence a **um único Produto**.

**Cardinalidade:** `1:1`  
**Opcionalidade:** estoque **opcional** para o produto.

---

## Diagrama MER
O diagrama MER está disponível em `docs/mer.pdf`.

---

## Tecnologias Utilizadas
- **C#**
- **.NET**
- **Entity Framework Core**
- **PostgreSQL**
- **Clean Architecture**
- **GUID** como estratégia de identificação das entidades

---

## Estrutura do Projeto
A solução está organizada por camadas, com responsabilidades separadas para facilitar manutenção e evolução:

```txt
clothing-store/
|-- ClothingStore.API/
|   |-- Program.cs
|   |-- appsettings.json
|
|-- ClothingStore.Application/
|   |-- Interfaces/
|       |-- Repositories/
|
|-- ClothingStore.Domain/
|   |-- Commom/
|   |-- Entities/
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
|
|-- README.md
`-- clothing store.sln
```

### Papel de cada camada
- **ClothingStore.API**: ponto de entrada da aplicação, configuração de serviços, pipeline HTTP e injeção de dependências.
- **ClothingStore.Application**: contratos e abstrações da aplicação (ex.: interfaces de repositório).
- **ClothingStore.Domain**: núcleo do negócio com entidades e regras de domínio.
- **ClothingStore.Infrastructure**: detalhes de persistência com EF Core, configurações de mapeamento, repositórios e migrações.
- **docs**: documentação de apoio, incluindo o MER do projeto.

---

## Migrations e DbContext
O acesso ao banco é centralizado no `ClothingStore.Infrastructure/Persistence/ClothingStoreContext.cs`, que define os `DbSet<>` das entidades e aplica os mapeamentos com `ApplyConfigurationsFromAssembly`.

As migrações ficam em `ClothingStore.Infrastructure/Migrations/`:
- `InitialCreate`: criação inicial do esquema.
- `FixItemPedidoMappings`: ajuste do mapeamento de `ItemPedido` (remoção de ambiguidade de relacionamento e criação do índice composto único `PedidoId + ProdutoId`, mantendo fidelidade ao MER no N:N entre Pedido e Produto).

Na API, o contexto é registrado no `ClothingStore.API/Program.cs` com `AddDbContext<ClothingStoreContext>()` e `UseNpgsql`, utilizando a connection string `Postgres`.

### Como reproduzir no ambiente local
1. Configurar `ConnectionStrings:Postgres` via User Secrets (ou variável de ambiente).
2. Exemplo com User Secrets:

```bash
dotnet user-secrets --project ClothingStore.API/ClothingStore.API.csproj set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=clothing_store;Username=postgres;Password=SUA_SENHA"
```

3. Executar a atualização do banco:

```bash
dotnet ef database update --project ClothingStore.Infrastructure/ClothingStore.Infrastructure.csproj --startup-project ClothingStore.API/ClothingStore.API.csproj
```

4. Evidências do esquema físico (banco) disponíveis em `docs/banco/diagrama.png` e `docs/banco/tabelas.png`.

---

## Repositories
Os contratos de acesso a dados estão em `ClothingStore.Application/Interfaces/Repositories/`, com a interface genérica `IGenericRepository<>` e interfaces específicas como `IClienteRepository`, `IPedidoRepository` e `IProdutoRepository`.

As implementações estão em `ClothingStore.Infrastructure/Persistence/Repositories/`, com `GenericRepository<>` e os repositórios específicos da aplicação. O vínculo entre interfaces e implementações é feito via injeção de dependências no `ClothingStore.API/Program.cs`.

