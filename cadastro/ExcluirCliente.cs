using validar;

namespace exluircliente
{
    public static class ExcluirCliente
    {
        public static void Executar()
        {
             while (!finalizado)
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Write(new Rule("[green bold]Exclusão de Cliente[/]").Centered());

                    var opcao = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Selecione um campo para [blue]preencher/editar[/] ou escolha [green]Salvar[/]:")
                            .PageSize(10)
                            .AddChoices(new[] {
                                $"[bold]CPF:[/] {Markup.Escape(cadastro.Cpf)}",
                                "[bold red]Sair[/]"
                            }));

                    if (opcao.Contains("CPF:"))
                    {
                        cadastro.Cpf = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Digite o CPF do cliente a excluir:[/]").Validate(ValidarDados.ValidarCpf));
                    }
                    else if (opcao.Contains("Sair"))
                    {
                        finalizado = true;
                    }
                }
        }
    }    
}