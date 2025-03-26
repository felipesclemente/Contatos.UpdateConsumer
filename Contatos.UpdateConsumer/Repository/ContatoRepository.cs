using Contatos.DataContracts.Commands;
using Contatos.UpdateConsumer.Interfaces;
using Dapper;
using Npgsql;
using Serilog;

namespace Contatos.UpdateConsumer.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ContatoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Postgres") ?? string.Empty;
        }

        public async Task AtualizarContatoAsync(AtualizarContato contato)
        {
            var novoNome = contato.NomeCompleto;
            var novoDDD = contato.DDD;
            var novoTelefone = contato.Telefone;
            var novoEmail = contato.Email;
            var id = contato.Id;

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                var queryString = @"UPDATE Contato SET NomeCompleto = @novoNome, DDD = @novoDDD, Telefone = @novoTelefone, Email = @novoEmail
                                    WHERE Id = @id";
                var result = await connection.ExecuteAsync(queryString, new { novoNome, novoDDD, novoTelefone, novoEmail, id });
                Console.WriteLine($"Operação concluída. Linhas afetadas: {result}.");
            }
            catch (Exception ex)
            {
                Log.Error($"ContatoRepository: Falha ao atualizar o contato ID {contato.Id}. Exception: {ex.GetType()}. Message: {ex.Message}.");
                Console.WriteLine($"ContatoRepository: Falha ao atualizar um contato. Exception: {ex.GetType()}. Message: {ex.Message}.");
            }
        }
    }
}
