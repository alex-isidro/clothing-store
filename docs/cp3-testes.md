# Evidências CP3 - Swagger e ProblemDetails

Este arquivo serve como evidência textual para a entrega do CP3. Ele pode ser mantido junto com um print da tela do Swagger, caso o professor solicite imagem.

## Swagger

Rota local da documentação:

```txt
https://localhost:<porta>/swagger
```

Endpoints documentados:

```txt
GET    /api/categorias
GET    /api/categorias/{id}
POST   /api/categorias

GET    /api/marcas
GET    /api/marcas/{id}
POST   /api/marcas

GET    /api/produtos
GET    /api/produtos/{id}
POST   /api/produtos

GET    /api/clientes
GET    /api/clientes/{id}
POST   /api/clientes

GET    /api/pedidos
GET    /api/pedidos/{id}
GET    /api/pedidos/cliente/{clienteId}
GET    /api/pedidos/status/{status}
POST   /api/pedidos
```

## Exemplo de erro tratado - 404

Requisição:

```http
GET /api/produtos/00000000-0000-0000-0000-000000000000
```

Resposta esperada:

```json
{
  "type": "about:blank",
  "title": "Recurso não encontrado",
  "status": 404,
  "detail": "Produto com id '00000000-0000-0000-0000-000000000000' não foi encontrado.",
  "instance": "/api/produtos/00000000-0000-0000-0000-000000000000",
  "traceId": "..."
}
```

Content-Type esperado:

```txt
application/problem+json
```

## Exemplo de erro tratado - 409

Ao tentar cadastrar um cliente com CPF ou e-mail já existente:

```json
{
  "type": "about:blank",
  "title": "Conflito de dados",
  "status": 409,
  "detail": "Já existe um cliente cadastrado com este CPF.",
  "instance": "/api/clientes",
  "traceId": "..."
}
```
