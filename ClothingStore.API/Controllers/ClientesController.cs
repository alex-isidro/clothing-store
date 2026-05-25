using ClothingStore.Application.DTOs.Clientes;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de clientes da loja.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClientesController : ControllerBase
{
    private readonly IClienteRepository _repository;

    public ClientesController(IClienteRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lista todos os clientes cadastrados.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Lista de clientes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ClienteResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var clientes = await _repository.GetAllAsync(cancellationToken);
        var response = clientes.Select(ClienteResponse.FromEntity);

        return Ok(response);
    }

    /// <summary>
    /// Busca um cliente pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Cliente encontrado.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClienteResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new ResourceNotFoundException("Cliente", id);

        return Ok(ClienteResponse.FromEntity(cliente));
    }

    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/clientes
    ///     {
    ///       "nome": "Maria Silva",
    ///       "cpf": "12345678901",
    ///       "email": "maria@email.com",
    ///       "telefone": "11999999999"
    ///     }
    /// </remarks>
    /// <param name="request">Dados do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Cliente criado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClienteResponse>> Create([FromBody] ClienteRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByCpfAsync(request.Cpf) is not null)
        {
            throw new ConflictException("Já existe um cliente cadastrado com este CPF.");
        }

        if (await _repository.GetByEmailAsync(request.Email) is not null)
        {
            throw new ConflictException("Já existe um cliente cadastrado com este e-mail.");
        }

        var cliente = new Cliente(request.Nome, request.Cpf, request.Email, request.Telefone);

        await _repository.AddAsync(cliente, cancellationToken);

        var response = ClienteResponse.FromEntity(cliente);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
