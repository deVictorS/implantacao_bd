using MySqlConnector;
using System;
using Spectre.Console;
using cadastro;
using conectar;
using tabelacliente;

namespace salvarcliente
{
    public static class SalvarCliente
    {
        public static void SalvarNoBanco(CadastroCliente.CriarCadastro cliente)
        {
            try
            {
                using var conexao = Conectar.ObterConexaoAberta();

                string query = "INSERT INTO Clientes (Nome, Cpf, Email, Telefone, DataNascimento, PreferenciaViagem) VALUES (@Nome, @Cpf, @Email, @Telefone, @DataNascimento, @PreferenciaViagem)";
                using var comando = new MySqlCommand(query, conexao);

                comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                comando.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                comando.Parameters.AddWithValue("@Email", cliente.Email);
                comando.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                comando.Parameters.AddWithValue("@PreferenciaViagem", cliente.PreferenciaViagem);

                int resultado = comando.ExecuteNonQuery();
                
                if (resultado > 0)
                {
                    var tabelaCliente = new TabelaCliente();
                    tabelaCliente.ExibirCliente(cliente);

                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red bold]Erro na operação:[/] {ex.Message}");
            }

            AnsiConsole.MarkupLine("[grey bold]Pressione qualquer tecla para voltar...[/]");
            Console.ReadKey(true);
        }
    }
}