using Asp.Versioning;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.DTOs.Produtos;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ClothingStore.API.Controllers;

[ApiController]
[ApiVersion("1.0", Deprecated = true)]
[ApiVersion("2.0")]
[Route("api/produtos")]
[Produces("application/json")]
public class ProdutosController(
    IProdutoService service,
    ILogger<ProdutosController> logger) : ControllerBase
{
    /// <summary>
    /// Lista produtos usando o contrato antigo. Esta versão está deprecada.
    /// </summary>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IEnumerable<ProdutoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> GetAllV1(
        CancellationToken cancellationToken)
    {
        var produtos = await service.GetAllAsync(cancellationToken);
        return Ok(produtos);
    }

    /// <summary>
    /// Lista produtos paginados usando o contrato v2.
    /// </summary>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(typeof(PagedResponse<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResponse<ProdutoResponse>>> GetAllV2(
        [FromQuery] PaginationQuery pagination,
        CancellationToken cancellationToken)
    {
        if (pagination.TryGetError(out var message))
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Parâmetros de paginação inválidos",
                Detail = message
            });

        var (items, totalItems) = await service.GetPagedAsync(
            pagination.Page,
            pagination.PageSize,
            cancellationToken);

        return Ok(new PagedResponse<ProdutoResponse>(
            items,
            pagination.Page,
            pagination.PageSize,
            totalItems));
    }

    /// <summary>
    /// Busca um produto por ID. Disponível nas versões 1.0 e 2.0.
    /// </summary>
    [HttpGet("{id:guid}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var produto = await service.GetByIdAsync(id, cancellationToken)
            ?? throw new ResourceNotFoundException("Produto", id);

        return Ok(produto);
    }

    /// <summary>
    /// Cria um novo produto. O endpoint continua disponível na v2.
    /// </summary>
    [HttpPost]
    [MapToApiVersion("2.0")]
    [EnableRateLimiting("produtos-write")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    public async Task<ActionResult<ProdutoResponse>> Create(
        [FromBody] ProdutoRequest request,
        CancellationToken cancellationToken)
    {
        var traceId = HttpContext.TraceIdentifier;

        logger.LogInformation(
            "Iniciando criação de produto. {MarcaId} {CategoriaId} {Nome} {TraceId}",
            request.MarcaId,
            request.CategoriaId,
            request.Nome,
            traceId);

        var response = await service.CreateAsync(request, cancellationToken);

        logger.LogInformation(
            "Produto criado com sucesso. {ProdutoId} {TraceId}",
            response.Id,
            traceId);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
