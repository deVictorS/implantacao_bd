using System;
using System.Collections.Generic;
using Spectre.Console;
using validar;
using salvarcliente;

namespace cadastro
{
    public class CadastroCliente
    {
        public class CriarCadastro
        {
            private static readonly string[] NiveisFidelidade = { "Bronze", "Prata", "Ouro", "Platina", "Diamante" };
            private const string OpcaoSalvar = "[bold green]Salvar dados[/]";
            private const string OpcaoSair = "[bold red]Sair[/]";

            public required string Nome { get; set; } = string.Empty;
            public required string Cpf { get; set; } = string.Empty;
            public required string Email { get; set; } = string.Empty;
            public required string Telefone { get; set; } = string.Empty;
            public required string DataNascimento { get; set; } = string.Empty;
            public required string PreferenciaViagem { get; set; } = string.Empty;
            public required string NivelFidelidade { get; set; } = "Bronze";

            public static void Executar()
            {
                var cadastro = new CriarCadastro { 
                    Nome = "", Cpf = "", Email = "", Telefone = "", 
                    DataNascimento = "", PreferenciaViagem = "", NivelFidelidade = "Bronze" 
                };

                while (true)
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Write(new Rule("[green bold]Cadastro de Cliente[/]").Centered());

                    var opcao = MenuPrincipal(cadastro);

                    if (opcao.Contains("Sair")) break;
                    if (opcao.Contains("Salvar dados"))
                    {
                        if (TentarSalvar(cadastro)) break;
                        continue;
                    }

                    ProcessarEntrada(opcao, cadastro);
                }
            }

            private static string MenuPrincipal(CriarCadastro c)
            {
                return AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Selecione um campo para preencher/editar[/]:")
                        .PageSize(10)
                        .AddChoices(new[] {
                            $"[bold]Nome:[/] {Markup.Escape(c.Nome)}",
                            $"[bold]CPF:[/] {Markup.Escape(c.Cpf)}",
                            $"[bold]Email:[/] {Markup.Escape(c.Email)}",
                            $"[bold]Telefone:[/] {Markup.Escape(c.Telefone)}",
                            $"[bold]Data Nasc.:[/] {Markup.Escape(c.DataNascimento)}",
                            $"[bold]Preferência:[/] {Markup.Escape(c.PreferenciaViagem)}",
                            $"[bold]Nível de Fidelidade:[/] {Markup.Escape(c.NivelFidelidade)}",
                            "[bold green]Salvar dados[/]",
                            "[bold red]Sair[/]"
                        }));
            }

            private static void ProcessarEntrada(string opcao, CriarCadastro c)
            {                string selecao = Markup.Remove(opcao);

                if (selecao.StartsWith("Nome:")) 
                    c.Nome = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Nome completo:[/]").Validate(ValidarDados.ValidarNome));
                
                else if (selecao.StartsWith("CPF:")) 
                    c.Cpf = AnsiConsole.Prompt(new TextPrompt<string>("[bold]CPF:[/]").Validate(ValidarDados.ValidarCpf));
                
                else if (selecao.StartsWith("Email:")) 
                    c.Email = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Email:[/]").Validate(ValidarDados.ValidarEmail));
                
                else if (selecao.StartsWith("Telefone:")) 
                    c.Telefone = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Telefone:[/]").Validate(ValidarDados.ValidarTelefone));
                
                else if (selecao.StartsWith("Data Nasc.:")) 
                    c.DataNascimento = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Data nascimento:[/]").Validate(ValidarDados.ValidarDataNascimento));
                
                else if (selecao.StartsWith("Preferência:")) 
                    c.PreferenciaViagem = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Preferência:[/]").Validate(ValidarDados.ValidarPreferenciaViagem));
                
                else if (selecao.StartsWith("Nível de Fidelidade:")){ 
                    c.NivelFidelidade = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("[bold]Selecione o nível de fidelidade:[/]")
                        .AddChoices(NiveisFidelidade));
                }
            }

            private static bool TentarSalvar(CriarCadastro c)
            {
                var camposFaltando = ValidarDados.VerificarCamposVazios(c);

                if (camposFaltando.Count > 0)
                {
                    ExibirErroCampos(camposFaltando);
                    return false;
                }

                try
                {
                    salvarcliente.SalvarCliente.SalvarNoBanco(c);
                    AnsiConsole.MarkupLine("[bold green]Cliente salvo com sucesso no RDS![/]");
                    System.Threading.Thread.Sleep(1500);
                    return true;
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]Erro ao salvar: {Markup.Escape(ex.Message)}[/]");
                    Console.ReadKey(true);
                    return false;
                }
            }

            private static void ExibirErroCampos(List<string> campos)
            {
                AnsiConsole.MarkupLine("[red bold]Campos obrigatórios faltando:[/]");
                foreach (var campo in campos) AnsiConsole.MarkupLine($" [red]- {campo}[/]");
                AnsiConsole.MarkupLine("\n[grey bold]Pressione qualquer tecla para voltar...[/]");
                Console.ReadKey(true);
            }
        }
    }
}