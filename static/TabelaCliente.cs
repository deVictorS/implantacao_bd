using cadastro;
using Spectre.Console;

namespace tabelacliente
{
    public class TabelaCliente
    {
        public void ExibirCliente(CadastroCliente.CriarCadastro cliente)
        {
            AnsiConsole.MarkupLine("[bold]Cliente:[/]");
            var tabela = new Table();
            tabela.AddColumn("[white]Campo[/]");
            tabela.AddColumn("[white]Dados Cadastrados[/]");
            tabela.AddRow("Nome", cliente.Nome);
            tabela.AddRow("CPF", cliente.Cpf);
            tabela.AddRow("E-mail", cliente.Email);
            tabela.AddRow("Telefone", cliente.Telefone);
            tabela.AddRow("Data Nasc.", cliente.DataNascimento);
            tabela.AddRow("Preferência", cliente.PreferenciaViagem);
            tabela.AddRow("Nível de Fidelidade", cliente.NivelFidelidade);
            
            AnsiConsole.Write(tabela);
        }
    }
}