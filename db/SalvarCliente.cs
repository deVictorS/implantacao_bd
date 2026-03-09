using MySqlConnector;
using System;
using Spectre.Console;
using DotNetEnv;

namespace salvarcliente
{
    public static class SalvarCliente
    {
        private static string CriarConexao()
        {
            Env.Load();
            return $"Server={Env.GetString("DB_SERVER")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_DATABASE")};Uid={Env.GetString("DB_USER")};Pwd={Env.GetString("DB_PASSWORD")};";
        }

        public static void SalvarNoBanco(cadastro.CadastroCliente.CriarCadastro cliente)
        {
            try
            {
                using var conexao = new MySqlConnection(CriarConexao());
                conexao.Open();

                string query = "INSERT INTO clientes (Nome, Cpf, Email, Telefone, DataNascimento, PreferenciaViagem) VALUES (@Nome, @Cpf, @Email, @Telefone, @DataNascimento, @PreferenciaViagem)";
                using var comando = new MySqlCommand(query, conexao);

                comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                comando.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                comando.Parameters.AddWithValue("@Email", cliente.Email);
                comando.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                comando.Parameters.AddWithValue("@PreferenciaViagem", cliente.PreferenciaViagem);

                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                    AnsiConsole.MarkupLine("[green bold]Cliente salvo com sucesso![/]");
                else
                    AnsiConsole.MarkupLine("[red bold]Erro ao salvar o cliente.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red bold]Erro: {ex.Message}[/]");
            }
        }
    }
}