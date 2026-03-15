# Clothing Store API

## Integrantes do Grupo

- **Alexander Dennis Isidro** – RM565554
- **Kelson Zhang** – RM563748

---

# Domínio do Projeto

O domínio escolhido para o projeto é **uma loja de roupas (Clothing Store)**.

O sistema representa o funcionamento básico de uma loja de roupas, permitindo o gerenciamento de clientes, produtos, categorias, marcas e pedidos realizados pelos clientes. A aplicação modela todo o fluxo de compra, desde o cadastro do cliente e seus endereços, passando pela escolha de produtos, criação de pedidos e itens de pedido, até o registro do pagamento e controle de estoque.

---

# Entidades Modeladas

As seguintes entidades foram modeladas no domínio do sistema:

- Cliente
- Endereco
- Pedido
- ItemPedido
- Produto
- Categoria
- Marca
- Pagamento
- Estoque

---

# Resumo dos Relacionamentos

- Cliente (1:N) Endereco
- Cliente (1:N) Pedido
- Endereco (1:N) Pedido
- Pedido (1:N) ItemPedido
- Produto (1:N) ItemPedido
- Categoria (1:N) Produto
- Marca (1:N) Produto
- Pedido (1:1) Pagamento
- Produto (1:1) Estoque

---

# Estrutura do Projeto

O projeto foi desenvolvido seguindo o padrão **Clean Architecture**, dividido nas seguintes camadas:

```
src
├── ClothingStore.API
├── ClothingStore.Application
├── ClothingStore.Domain
└── ClothingStore.Infrastructure
```


- **API** – Camada responsável por expor os endpoints da aplicação.
- **Application** – Contém regras de aplicação e comunicação entre API e domínio.
- **Domain** – Contém as entidades e regras de negócio do sistema.
- **Infrastructure** – Responsável por integração com banco de dados e serviços externos.
