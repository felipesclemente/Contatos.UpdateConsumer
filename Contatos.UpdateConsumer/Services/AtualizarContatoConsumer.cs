using Contatos.DataContracts.Commands;
using Contatos.UpdateConsumer.Interfaces;
using MassTransit;

namespace Contatos.UpdateConsumer.Services
{
    public class AtualizarContatoConsumer : IConsumer<AtualizarContato>
    {
        private readonly IContatoRepository _repoContato;

        public AtualizarContatoConsumer(IContatoRepository repoContato)
        {
            _repoContato = repoContato;
        }

        public async Task Consume(ConsumeContext<AtualizarContato> context)
        {
            Console.WriteLine(context.Message);
            await _repoContato.AtualizarContatoAsync(context.Message);
        }
    }
}
