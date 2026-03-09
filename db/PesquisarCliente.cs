using conectar;
using Spectre.Console;
using MySqlConnector;
using menu;

namespace pesquisarcliente
{
    public static class PesquisarCliente
    {
        public static void Executar(string Cpf)
        {
            try
            {
                using var conexao = Conectar.ObterConexaoAberta();

                string query = "SELECT Nome, Cpf, Email, Telefone, DataNascimento, PreferenciaViagem FROM Clientes WHERE Cpf = @Cpf";
                using var comando = new MySqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@Cpf", Cpf);

                using var leitor = comando.ExecuteReader();

                if (leitor.Read())
                {
                    AnsiConsole.MarkupLine("[green bold]Cliente encontrado![/]");
                    var tabela = new Table();
                    tabela.AddColumn("[white]Campo[/]");
                    tabela.AddColumn("[white]Dados Cadastrados[/]");
                    tabela.AddRow("Nome", leitor["Nome"].ToString());
                    tabela.AddRow("CPF", leitor["Cpf"].ToString());
                    tabela.AddRow("E-mail", leitor["Email"].ToString());
                    tabela.AddRow("Telefone", leitor["Telefone"].ToString());
                    tabela.AddRow("Data Nasc.", leitor["DataNascimento"].ToString());
                    tabela.AddRow("Preferência", leitor["PreferenciaViagem"].ToString());            
                    
                    AnsiConsole.Write(tabela);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red bold]Nenhum cliente encontrado com o CPF fornecido.[/]");
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