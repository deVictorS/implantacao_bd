using Spectre.Console;
using styles;
using cadastro;

namespace menu
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear();

            Console.Title = "Software de Gestão";

            BannerMenu.Banner_Menu();

            var encerrar = false;

            while (!encerrar)
            {
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[grey]Selecione uma opção:[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Use as setas para navegar)[/]")
                        .AddChoices(new[]
                        {
                            "Criar tabela no BD"
                        }
                ));
                switch (opcao)
                {
                    case "Criar tabela no BD":
                        CadastroCliente.Criar_Cadastro();
                        break;
                }
            }
            
        }
    }
}