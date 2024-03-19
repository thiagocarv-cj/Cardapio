using Microsoft.AspNetCore.Mvc;
using Dominio;
using MediatR;
using static Infraestrutura.RepositorioCliente;

namespace Api_Cardapio.Controllers
{
    [Route("api/Clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var query = new ObterClientesQuery();
            var clientes = await _mediator.Send(query);
            return Ok(clientes);
        }

        // GET: api/clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var query = new ObterClientePorIdQuery(id);
            var cliente = await _mediator.Send(query);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post([FromBody] AdicionarClienteCommand command)
        {
            var cliente = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        // PUT: api/clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarClienteCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new RemoverClienteCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
