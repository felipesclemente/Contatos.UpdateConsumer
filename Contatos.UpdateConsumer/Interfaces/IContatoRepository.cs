using Contatos.DataContracts.Commands;

namespace Contatos.UpdateConsumer.Interfaces
{
    public interface IContatoRepository
    {
        Task AtualizarContatoAsync(AtualizarContato contato);
    }
}
