# CP1 — Modelo Entidade-Relacionamento (MER) e Estrutura Inicial WebAPI

## Integrantes do Grupo
- **Alexander Dennis Isidro** — **RM565554**
- **Kelson Zhang** — **RM563748**

---

## Domínio Escolhido
**Loja de Roupas (Clothing Store)**

O projeto representa a estrutura inicial de uma loja de roupas, com foco exclusivo na **modelagem das entidades** e no **diagrama MER**.

---

## Objetivo do Trabalho
Este repositório foi desenvolvido para atender ao desafio de:

- elaborar um **MER (Modelo Entidade-Relacionamento)** com entidades relacionadas;
- definir **atributos principais**, **chaves primárias** e **relacionamentos**;
- indicar **cardinalidade** e **opcionalidade**;
- criar a **estrutura inicial de uma WebAPI em .NET**, seguindo o padrão **Clean Architecture**;
- modelar em código as mesmas entidades representadas no MER.

---

## Entidades Modeladas
As entidades criadas no projeto foram:

- **Cliente**
- **Endereco**
- **Pedido**
- **ItemPedido**
- **Produto**
- **Categoria**
- **Marca**
- **Pagamento**
- **Estoque**

Todas as entidades utilizam **GUID** como chave primária, conforme a estratégia de identificação adotada no projeto.

---

## Resumo dos Relacionamentos

### Cliente e Endereco
- Um **Cliente** pode ter **um ou vários Endereços**.
- Cada **Endereco** pertence a **um único Cliente**.

**Cardinalidade:** `1:N`

---

### Cliente e Pedido
- Um **Cliente** pode realizar **um ou vários Pedidos**.
- Cada **Pedido** pertence a **um único Cliente**.

**Cardinalidade:** `1:N`

---

### Endereco e Pedido
- Um **Endereco** pode ser utilizado em **um ou vários Pedidos** de entrega.
- Cada **Pedido** possui **um único Endereco de entrega**.

**Cardinalidade:** `1:N`

---

### Pedido e ItemPedido
- Um **Pedido** possui **um ou vários ItensPedido**.
- Cada **ItemPedido** pertence a **um único Pedido**.

**Cardinalidade:** `1:N`

---

### Produto e ItemPedido
- Um **Produto** pode aparecer em **um ou vários ItensPedido**.
- Cada **ItemPedido** referencia **um único Produto**.

**Cardinalidade:** `1:N`

---

### Pedido e Produto
- Um **Pedido** pode conter **vários Produtos**.
- Um **Produto** pode estar presente em **vários Pedidos**.
- Esse relacionamento é resolvido pela entidade associativa **ItemPedido**.

**Cardinalidade:** `N:N`

---

### Categoria e Produto
- Uma **Categoria** pode possuir **vários Produtos**.
- Cada **Produto** pertence a **uma única Categoria**.

**Cardinalidade:** `1:N`

---

### Marca e Produto
- Uma **Marca** pode estar associada a **vários Produtos**.
- Cada **Produto** pertence a **uma única Marca**.

**Cardinalidade:** `1:N`

---

### Pedido e Pagamento
- Um **Pedido** pode possuir **zero ou um Pagamento**.
- Cada **Pagamento** pertence a **um único Pedido**.

**Cardinalidade:** `1:1`  
**Opcionalidade:** pagamento **opcional** para o pedido.

---

### Produto e Estoque
- Um **Produto** pode possuir **zero ou um Estoque**.
- Cada **Estoque** pertence a **um único Produto**.

**Cardinalidade:** `1:1`  
**Opcionalidade:** estoque **opcional** para o produto.

---

## Diagrama MER
O diagrama MER está disponível em:

`/docs/MER - clothing store.pdf`

---

## Tecnologias Utilizadas
- **C#**
- **.NET**
- **Clean Architecture**
- **GUID** como estratégia de identificação das entidades

---
## Estrutura do Projeto
A organização do repositório foi feita em camadas, seguindo a proposta de **Clean Architecture**:


```txt
ClothingStore.API/
ClothingStore.Application/
ClothingStore.Domain/
ClothingStore.Infrastructure/
docs/
README.md
clothing store.sln