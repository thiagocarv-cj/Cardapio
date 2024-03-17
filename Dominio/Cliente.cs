using MediatR;

namespace Dominio
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    public class ObterClientesQuery : IRequest<IEnumerable<Cliente>> { }

    public class ObterClientePorIdQuery : IRequest<Cliente>
    {
        public int Id { get; }

        public ObterClientePorIdQuery(int id)
        {
            Id = id;
        }
    }

    public class AdicionarClienteCommand : IRequest<Cliente>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    public class AtualizarClienteCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
    public class RemoverClienteCommand : IRequest<bool>
    {
        public int Id { get; }

        public RemoverClienteCommand(int id)
        {
            Id = id;
        }
    }

}