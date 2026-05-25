using ClothingStore.Application.DTOs.Categorias;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de categorias de produtos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriasController : ControllerBase
{
    private readonly IRepository<Categoria> _repository;

    public CategoriasController(IRepository<Categoria> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lista todas as categorias cadastradas.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Lista de categorias.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoriaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var categorias = await _repository.GetAllAsync(cancellationToken);
        var response = categorias.Select(CategoriaResponse.FromEntity);

        return Ok(response);
    }

    /// <summary>
    /// Busca uma categoria pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da categoria.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Categoria encontrada.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoriaResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var categoria = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new ResourceNotFoundException("Categoria", id);

        return Ok(CategoriaResponse.FromEntity(categoria));
    }

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/categorias
    ///     {
    ///       "nome": "Camisetas",
    ///       "descricao": "Categoria de camisetas masculinas e femininas"
    ///     }
    /// </remarks>
    /// <param name="request">Dados da categoria.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Categoria criada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoriaResponse>> Create([FromBody] CategoriaRequest request, CancellationToken cancellationToken)
    {
        var categoria = new Categoria(request.Nome, request.Descricao);

        await _repository.AddAsync(categoria, cancellationToken);

        var response = CategoriaResponse.FromEntity(categoria);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
