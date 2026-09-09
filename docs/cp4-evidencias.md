# CP4 — Evidências

Este arquivo organiza as evidências solicitadas no enunciado do CP4. Os valores abaixo devem ser substituídos pelos resultados reais obtidos durante a execução local.

## 1. `/health` — Healthy

Com a API e o PostgreSQL em execução:

```http
GET /health
```

Resultado esperado:

```txt
HTTP 200
```

A resposta deve apresentar os checks `self` e `database` com status `Healthy`.

### Evidência

> Inserir aqui o print do navegador, Swagger/HTTP Client ou o JSON real retornado pela API.

---

## 2. `/health` — Unhealthy

Com o PostgreSQL parado ou com uma connection string inválida configurada somente no ambiente local:

```http
GET /health
```

Resultado esperado:

```txt
HTTP 503
```

O check `database` deve aparecer como `Unhealthy`, fazendo o status agregado de `/health` também ficar `Unhealthy`.

### Evidência

> Inserir aqui o print do resultado real com o banco indisponível.

**Atenção:** não inserir senha, token ou outra credencial real no print ou neste arquivo.

---

## 3. Log de `POST /api/produtos`

Executar uma criação válida de produto:

```http
POST /api/produtos
```

O console deve apresentar logs estruturados contendo, no mínimo:

- início da operação;
- `MarcaId`;
- `CategoriaId`;
- `Nome`;
- `TraceId`;
- sucesso da operação;
- `ProdutoId`.

### Evidência

> Inserir aqui o trecho real do console mostrando o `TraceId`.

---

## 4. Exceção tratada pelo `GlobalExceptionHandler`

Executar uma requisição que gere uma exceção já mapeada pelo handler.

O console deve registrar a exceção em nível `Error` contendo o `TraceId`.

### Evidência

> Inserir aqui o trecho real do log do `GlobalExceptionHandler`.

---

## 5. Testes

Na raiz da solution:

```bash
dotnet test
```

### Evidência

> Inserir aqui o print da saída real do `dotnet test` com todos os testes passando.

Os testes obrigatórios estão separados em:

```txt
ClothingStore.Domain.Tests
ClothingStore.Application.Tests
```
