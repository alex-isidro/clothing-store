# CP5 — Evidências

> Preencher/atualizar este arquivo com prints ou trechos reais obtidos ao executar a API.

## 1. GET v1 — lista antiga

```http
GET /api/produtos?api-version=1.0
```

Cole aqui a resposta JSON e os headers `api-supported-versions` e `api-deprecated-versions`.

## 2. GET v2 — envelope paginado

```http
GET /api/produtos?page=1&pageSize=2
```

Cole aqui a resposta.

## 3. v2 — página 2

```http
GET /api/produtos?page=2&pageSize=2
```

Demonstre que os itens não se sobrepõem à página 1 e que `totalPages` é coerente.

## 4. Query string e header

Demonstre:

```http
GET /api/produtos?api-version=1.0
```

e:

```http
GET /api/produtos
X-Api-Version: 1.0
```

## 5. Default v2

```http
GET /api/produtos
```

Sem versão deve retornar o envelope da v2.

## 6. Swagger

Print do Swagger mostrando:

- v1.0;
- v2.0;
- v1.0 identificada como deprecada.

## 7. Validação 400

```http
GET /api/produtos?page=0
GET /api/produtos?pageSize=9999
```

Ambas devem retornar HTTP 400 com mensagem explicando a regra.

## 8. Rate limit

Executar o `POST /api/produtos` mais de 10 vezes no intervalo de 1 minuto até obter:

```http
HTTP 429 Too Many Requests
Retry-After: 60
```

Guardar o JSON retornado.

## 9. Health após rate limit

Depois do 429:

```http
GET /health
```

Deve continuar retornando HTTP 200 quando API e banco estiverem saudáveis.

## 10. Testes

Guardar a saída de:

```bash
dotnet test
```

Os testes de Domain e Application do CP4 devem continuar verdes.
