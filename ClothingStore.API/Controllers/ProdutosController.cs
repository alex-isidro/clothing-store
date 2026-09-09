using ClothingStore.Application.DTOs.Produtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de produtos da loja.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoService _service;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(
        IProdutoRepository produtoRepository,
        IProdutoService service,
        ILogger<ProdutosController> logger)
    {
        _produtoRepository = produtoRepository;
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Lista todos os produtos cadastrados.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Lista de produtos.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var produtos = await _produtoRepository.GetAllAsync(cancellationToken);
        var response = produtos.Select(ProdutoResponse.FromEntity);

        return Ok(response);
    }

    /// <summary>
    /// Busca um produto pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Produto encontrado.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProdutoResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new ResourceNotFoundException("Produto", id);

        return Ok(ProdutoResponse.FromEntity(produto));
    }

    /// <summary>
    /// Cria um novo produto vinculado a uma marca e a uma categoria existentes.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/produtos
    ///     {
    ///       "marcaId": "11111111-1111-1111-1111-111111111111",
    ///       "categoriaId": "22222222-2222-2222-2222-222222222222",
    ///       "nome": "Camiseta Básica",
    ///       "descricao": "Camiseta de algodão",
    ///       "preco": 79.90,
    ///       "tamanho": "M",
    ///       "cor": "Preta"
    ///     }
    /// </remarks>
    /// <param name="request">Dados do produto.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Produto criado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProdutoResponse>> Create([FromBody] ProdutoRequest request, CancellationToken cancellationToken)
    {
        var traceId = HttpContext.TraceIdentifier;

        _logger.LogInformation(
            "Iniciando criação de produto. {MarcaId} {CategoriaId} {Nome} {TraceId}",
            request.MarcaId,
            request.CategoriaId,
            request.Nome,
            traceId);

        var response = await _service.CreateAsync(request, cancellationToken);

        _logger.LogInformation(
            "Produto criado com sucesso. {ProdutoId} {TraceId}",
            response.Id,
            traceId);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
