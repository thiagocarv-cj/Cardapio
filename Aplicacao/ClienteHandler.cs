using Dominio;
using Infraestrutura;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Infraestrutura.RepositorioCliente;

namespace Aplicacao
{
    public class ClienteHandler :
        IRequestHandler<ObterClientesQuery, IEnumerable<Cliente>>,
        IRequestHandler<ObterClientePorIdQuery, Cliente>,
        IRequestHandler<AdicionarClienteCommand, Cliente>,
        IRequestHandler<AtualizarClienteCommand, bool>,
        IRequestHandler<RemoverClienteCommand, bool>
    {
        private readonly AplicacaoDbContext _context;

        public ClienteHandler(AplicacaoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> Handle(ObterClientesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clientes.FindAsync(request.Id);
        }

        public async Task<Cliente> Handle(AdicionarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente { Nome = request.Nome, Email = request.Email };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes.FindAsync(request.Id);
            if (cliente == null)
            {
                return false;
            }

            cliente.Nome = request.Nome;
            cliente.Email = request.Email;            
            return true;
        }

        public async Task<bool> Handle(RemoverClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes.FindAsync(request.Id);
            if (cliente == null)
            {
                return false;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}