using Spectre.Console;
using cadastro;

namespace menu
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var encerrar = false;

            while (!encerrar)
            {
                Console.Clear();

                Console.Title = "Software de Gestão";

                AnsiConsole.Write(
                    new FigletText("Valoures Turismo&Câmbio")
                    .Centered()
                    .Color(Color.Green));

                AnsiConsole.Write(new Rule("[green bold]Menu Principal[/]").Centered());
                    
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[grey bold]Selecione uma opção:[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey bold](Use as setas para navegar)[/]")
                        .AddChoices(new[]
                        {
                            "[bold]Novo cliente[/]",
                            "[bold]Editar cliente[/]",
                            "[bold]Excluir cliente[/]",
                            "[bold]Pesquisar cliente[/]",
                            "[bold]Novo pacote de viagens[/]",
                            "[bold]Vendas[/]",
                            "[bold]Cotação de viagens[/]",
                            "[bold]Cotação de moedas[/]",                  
                            "[bold]Inteligência[/]",
                            "[bold]Consulta IA[/]",
                            "[bold red]Finalizar programa[/]",
                            
                        }
                ));
                switch (opcao)
                {
                    case "[bold]Novo cliente[/]":
                        CadastroCliente.CriarCadastro.Executar();
                        break;
                    
                    case "[bold red]Finalizar programa[/]":
                        Console.Clear();
                        AnsiConsole.MarkupLine("[red bold]Encerrando o programa...[/]");
                        encerrar = true;
                        break;

                }
            }
            
        }
    }
}