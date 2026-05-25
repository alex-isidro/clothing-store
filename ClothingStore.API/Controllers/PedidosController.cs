using ClothingStore.Application.DTOs.Pedidos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers;

/// <summary>
/// Endpoints para consulta e criação de pedidos da loja.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IRepository<Endereco> _enderecoRepository;

    public PedidosController(
        IPedidoRepository pedidoRepository,
        IClienteRepository clienteRepository,
        IRepository<Endereco> enderecoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
        _enderecoRepository = enderecoRepository;
    }

    /// <summary>
    /// Lista todos os pedidos cadastrados.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Lista de pedidos.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var pedidos = await _pedidoRepository.GetAllAsync(cancellationToken);
        var response = pedidos.Select(PedidoResponse.FromEntity);

        return Ok(response);
    }

    /// <summary>
    /// Busca um pedido pelo identificador, incluindo itens e pagamento quando existirem.
    /// </summary>
    /// <param name="id">Identificador do pedido.</param>
    /// <returns>Pedido encontrado.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoResponse>> GetById(Guid id)
    {
        var pedido = await _pedidoRepository.GetWithItensAsync(id)
            ?? throw new ResourceNotFoundException("Pedido", id);

        return Ok(PedidoResponse.FromEntity(pedido));
    }

    /// <summary>
    /// Lista pedidos de um cliente específico.
    /// </summary>
    /// <param name="clienteId">Identificador do cliente.</param>
    /// <returns>Pedidos do cliente informado.</returns>
    [HttpGet("cliente/{clienteId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> GetByCliente(Guid clienteId)
    {
        if (await _clienteRepository.GetByIdAsync(clienteId) is null)
        {
            throw new ResourceNotFoundException("Cliente", clienteId);
        }

        var pedidos = await _pedidoRepository.GetByClienteIdAsync(clienteId);
        var response = pedidos.Select(PedidoResponse.FromEntity);

        return Ok(response);
    }

    /// <summary>
    /// Lista pedidos filtrados por status.
    /// </summary>
    /// <param name="status">Status usado no filtro.</param>
    /// <returns>Pedidos encontrados para o status informado.</returns>
    [HttpGet("status/{status}")]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> GetByStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            throw new DomainException("Status não pode ser vazio.");
        }

        var pedidos = await _pedidoRepository.GetByStatusAsync(status);
        var response = pedidos.Select(PedidoResponse.FromEntity);

        return Ok(response);
    }

    /// <summary>
    /// Cria um novo pedido vinculado a um cliente e endereço existentes.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/pedidos
    ///     {
    ///       "clienteId": "11111111-1111-1111-1111-111111111111",
    ///       "enderecoEntregaId": "22222222-2222-2222-2222-222222222222",
    ///       "status": "Criado",
    ///       "valorTotal": 199.90,
    ///       "dataPedido": "2026-05-24T12:00:00Z"
    ///     }
    /// </remarks>
    /// <param name="request">Dados do pedido.</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
    /// <returns>Pedido criado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PedidoResponse>> Create([FromBody] PedidoRequest request, CancellationToken cancellationToken)
    {
        if (await _clienteRepository.GetByIdAsync(request.ClienteId, cancellationToken) is null)
        {
            throw new ResourceNotFoundException("Cliente", request.ClienteId);
        }

        if (!await _enderecoRepository.ExistsAsync(request.EnderecoEntregaId, cancellationToken))
        {
            throw new ResourceNotFoundException("Endereco", request.EnderecoEntregaId);
        }

        var pedido = new Pedido(
            request.ClienteId,
            request.EnderecoEntregaId,
            request.DataPedido ?? DateTime.UtcNow,
            request.Status,
            request.ValorTotal);

        await _pedidoRepository.AddAsync(pedido, cancellationToken);

        var response = PedidoResponse.FromEntity(pedido);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
