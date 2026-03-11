using conectar;
using Spectre.Console;
using MySqlConnector;
using cadastro;
using tabelacliente;
using validar;

namespace pesquisarcliente
{
    public static class PesquisarCliente
    {
        public static void Executar()
        {
            bool pesquisando = true;

            while (pesquisando)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[green bold]Pesquisa de Cliente[/]").Centered());

                var escolha = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[bold]Selecione o campo para pesquisar[/]:")
                        .AddChoices(new[] {
                            "[bold]Consultar CPF[/]",
                            "[red bold]Sair[/]"
                        }));

                if (escolha.Contains("[red bold]Sair[/]"))
                {
                    pesquisando = false;
                    break; 
                }

                string cpfBusca = AnsiConsole.Prompt(
                    new TextPrompt<string>("[bold]Digite o CPF para busca (ou 0 para voltar):[/]")
                    .Validate(input => 
                    {
                        if (input == "0") return ValidationResult.Success();
                        {
                            return ValidarDados.ValidarCpf(input);
                        }
                    }));

                if (cpfBusca == "0")
                {
                    continue; 
                }

                try
                {
                    using var conexao = Conectar.ObterConexaoAberta();
                    string query = "SELECT Nome, Cpf, Email, Telefone, DataNascimento, PreferenciaViagem, NivelFidelidade FROM Clientes WHERE Cpf = @Cpf";
                    
                    using var comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@Cpf", cpfBusca);

                    using var leitor = comando.ExecuteReader();

                    if (leitor.Read())
                    {
                        var cliente = new CadastroCliente.CriarCadastro
                        {
                            Nome = leitor.IsDBNull(leitor.GetOrdinal("Nome")) ? "Não informado" : leitor.GetString("Nome"),
                            Cpf = leitor.IsDBNull(leitor.GetOrdinal("Cpf")) ? "" : leitor.GetString("Cpf"),
                            Email = leitor.IsDBNull(leitor.GetOrdinal("Email")) ? "" : leitor.GetString("Email"),
                            Telefone = leitor.IsDBNull(leitor.GetOrdinal("Telefone")) ? "" : leitor.GetString("Telefone"),
                            DataNascimento = leitor.IsDBNull(leitor.GetOrdinal("DataNascimento")) ? "00/00/0000" : leitor.GetDateTime("DataNascimento").ToString("dd/MM/yyyy"),
                            PreferenciaViagem = leitor.IsDBNull(leitor.GetOrdinal("PreferenciaViagem")) ? "Não informada" : leitor.GetString("PreferenciaViagem"),
                            NivelFidelidade = leitor.IsDBNull(leitor.GetOrdinal("NivelFidelidade")) ? "Bronze" : leitor.GetString("NivelFidelidade")
                        };

                        var telaTabela = new TabelaCliente();
                        telaTabela.ExibirCliente(cliente);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow bold]CPF não encontrado na base de dados.[/]");
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red bold]Erro:[/] {ex.Message}");
                }

                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[grey bold]Pressione qualquer tecla para continuar...[/]");
                Console.ReadKey(true);
            }
        }
    }
}