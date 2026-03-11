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
            public string Nome { get; set; }
            public string Cpf { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string DataNascimento { get; set; }
            public string PreferenciaViagem { get; set; }
            public string NivelFidelidade { get; set; }

            public static void Executar()
            {
                CriarCadastro cadastro = new CriarCadastro
                {
                    Nome = "",
                    Cpf = "",
                    Email = "",
                    Telefone = "",
                    DataNascimento = "",
                    PreferenciaViagem = "",
                    NivelFidelidade = "Bronze"
                };

                bool finalizado = false;

                while (!finalizado)
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Write(new Rule("[green bold]Cadastro de Cliente[/]").Centered());

                    var opcao = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[bold]Selecione um campo para [blue]preencher/editar[/]:")
                            .PageSize(10)
                            .AddChoices(new[] {
                                $"[bold]Nome:[/] {Markup.Escape(cadastro.Nome)}",
                                $"[bold]CPF:[/] {Markup.Escape(cadastro.Cpf)}",
                                $"[bold]Email:[/] {Markup.Escape(cadastro.Email)}",
                                $"[bold]Telefone:[/] {Markup.Escape(cadastro.Telefone)}",
                                $"[bold]Data Nasc.:[/] {Markup.Escape(cadastro.DataNascimento)}",
                                $"[bold]Preferência:[/] {Markup.Escape(cadastro.PreferenciaViagem)}",
                                $"[bold]Nível de Fidelidade:[/] {Markup.Escape(cadastro.NivelFidelidade)}",
                                "[bold green]Salvar dados[/]",
                                "[bold red]Sair[/]"
                            }));

                    if (opcao.Contains("Nome:"))
                    {
                        cadastro.Nome = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Digite o nome completo do cliente:[/]").Validate(ValidarDados.ValidarNome));
                    }
                    else if (opcao.Contains("CPF:"))
                    {
                        cadastro.Cpf = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Digite o CPF do cliente:[/]").Validate(ValidarDados.ValidarCpf));
                    }
                    else if (opcao.Contains("Email:"))
                    {
                        cadastro.Email = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Digite o email do cliente:[/]").Validate(ValidarDados.ValidarEmail));
                    }
                    else if (opcao.Contains("Telefone:"))
                    {
                        cadastro.Telefone = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Digite o telefone do cliente:[/]").Validate(ValidarDados.ValidarTelefone));
                    }
                    else if (opcao.Contains("Data Nasc.:"))
                    {
                        cadastro.DataNascimento = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Data de nascimento:[/]").Validate(ValidarDados.ValidarDataNascimento));
                    }
                    else if (opcao.Contains("Preferência:"))
                    {
                        cadastro.PreferenciaViagem = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Digite a preferência de viagem:[/]").Validate(ValidarDados.ValidarPreferenciaViagem));
                    }

                    else if (opcao.Contains("Preferência:"))
                    {
                        cadastro.PreferenciaViagem = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Digite a preferência de viagem:[/]").Validate(ValidarDados.ValidarPreferenciaViagem));
                    }
                    else if (opcao.Contains("Nível de Fidelidade:"))
                    {
                        cadastro.NivelFidelidade = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold]Selecione o nível de fidelidade do cliente:[/]")
                                .PageSize(5)
                                .AddChoices(new[] { "Bronze", "Prata", "Ouro", "Platina" }));
                                
                        AnsiConsole.MarkupLine($"[green]Nível definido como:[/] {cadastro.NivelFidelidade}");
                    }
                    else if (opcao.Contains("Salvar dados"))
                    {
                        var camposFaltando = ValidarDados.VerificarCamposVazios(cadastro);

                        if (camposFaltando.Count > 0)
                        {
                            AnsiConsole.MarkupLine("[red bold]Erro: Os seguintes campos são obrigatórios:[/]");
                            foreach (var campo in camposFaltando)
                            {
                                AnsiConsole.MarkupLine($"[red]  - {campo}[/]");
                            }
                            AnsiConsole.MarkupLine("\n[grey bold]Pressione qualquer tecla para corrigir...[/]");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            try
                            {
                                salvarcliente.SalvarCliente.SalvarNoBanco(cadastro);
                                finalizado = true;
                            }            
                            catch (Exception ex)
                            {
                                AnsiConsole.MarkupLine($"[bold red]Erro crítico: {ex.Message}[/]");
                                AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para voltar...[/]");
                                Console.ReadKey(true);
                            }
                        }
                    }
                    else if (opcao.Contains("Sair"))
                    {
                        AnsiConsole.Clear();
                        finalizado = true;
                    }
                }
            }
        }
    }
}