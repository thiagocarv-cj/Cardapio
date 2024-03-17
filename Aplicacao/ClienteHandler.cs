using Dominio;
using MediatR;

namespace Aplicacao
{
    public class ClienteHandler :
        IRequestHandler<ObterClientesQuery, IEnumerable<Cliente>>,
        IRequestHandler<ObterClientePorIdQuery, Cliente>,
        IRequestHandler<AdicionarClienteCommand, Cliente>,
        IRequestHandler<AtualizarClienteCommand, bool>,
        IRequestHandler<RemoverClienteCommand, bool>
    {
        private readonly List<Cliente> _clientes = new List<Cliente>();
        private int _nextId = 1;

        public async Task<IEnumerable<Cliente>> Handle(ObterClientesQuery request, CancellationToken cancellationToken)
        {
            return _clientes;
        }

        public async Task<Cliente> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            return _clientes.Find(c => c.Id == request.Id);
        }

        public async Task<Cliente> Handle(AdicionarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente { Id = _nextId++, Nome = request.Nome, Email = request.Email };
            _clientes.Add(cliente);
            return cliente;
        }

        public async Task<bool> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteIndex = _clientes.FindIndex(c => c.Id == request.Id);
            if (clienteIndex == -1)
            {
                return false;
            }

            _clientes[clienteIndex] = new Cliente { Id = request.Id, Nome = request.Nome, Email = request.Email };
            return true;
        }

        public async Task<bool> Handle(RemoverClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = _clientes.Find(c => c.Id == request.Id);
            if (cliente == null)
            {
                return false;
            }

            _clientes.Remove(cliente);
            return true;
        }
    }
}