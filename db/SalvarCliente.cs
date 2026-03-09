using MySqlConnector;
using System;
using Spectre.Console;
using cadastro;
using conectar;

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
                    AnsiConsole.MarkupLine("[green bold]Cliente salvo com sucesso![/]");
                    var tabela = new Table();
                    tabela.AddColumn("[white]Campo[/]");
                    tabela.AddColumn("[white]Dados Cadastrados[/]");
                    tabela.AddRow("Nome", cliente.Nome);
                    tabela.AddRow("CPF", cliente.Cpf);
                    tabela.AddRow("E-mail", cliente.Email);
                    tabela.AddRow("Telefone", cliente.Telefone);
                    tabela.AddRow("Data Nasc.", cliente.DataNascimento);
                    tabela.AddRow("Preferência", cliente.PreferenciaViagem);            
                    
                    AnsiConsole.Write(tabela);

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