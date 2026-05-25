using ClothingStore.Application.DTOs.Marcas;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de marcas de produtos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MarcasController : ControllerBase
{
    private readonly IRepository<Marca> _repository;

    public MarcasController(IRepository<Marca> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lista todas as marcas cadastradas.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Lista de marcas.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MarcaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MarcaResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var marcas = await _repository.GetAllAsync(cancellationToken);
        var response = marcas.Select(MarcaResponse.FromEntity);

        return Ok(response);
    }

    /// <summary>
    /// Busca uma marca pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da marca.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Marca encontrada.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MarcaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MarcaResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var marca = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new ResourceNotFoundException("Marca", id);

        return Ok(MarcaResponse.FromEntity(marca));
    }

    /// <summary>
    /// Cria uma nova marca.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/marcas
    ///     {
    ///       "nome": "Nike",
    ///       "descricao": "Marca de roupas e acessórios esportivos"
    ///     }
    /// </remarks>
    /// <param name="request">Dados da marca.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Marca criada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(MarcaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MarcaResponse>> Create([FromBody] MarcaRequest request, CancellationToken cancellationToken)
    {
        var marca = new Marca(request.Nome, request.Descricao);

        await _repository.AddAsync(marca, cancellationToken);

        var response = MarcaResponse.FromEntity(marca);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
