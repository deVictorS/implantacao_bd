using System.Text.RegularExpressions;
using Spectre.Console;  
using System.Globalization;
using System.Collections.Generic;
using cadastro;

namespace validar
{
    public static class ValidarDados
    {
        static ValidarDados()
        {
            AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromMilliseconds(100));
        }

        public static ValidationResult ValidarNome(string Nome)
        {
            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3)
                return ValidationResult.Error("[red]O nome deve conter pelo menos 3 caracteres.[/]");

            var isLetter = Regex.IsMatch(Nome, @"^[a-zA-ZÀ-ÿ\s]+$", RegexOptions.NonBacktracking);

            return isLetter
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]O nome deve conter apenas letras e espaços.[/]");
        }

        public static ValidationResult ValidarCpf(string Cpf)
        {
            if (string.IsNullOrWhiteSpace(Cpf))
                return ValidationResult.Error("[red]O CPF é obrigatório.[/]");

            var cpfRegex = new Regex(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", RegexOptions.None, TimeSpan.FromMilliseconds(100));
            
            return cpfRegex.IsMatch(Cpf) 
                ? ValidationResult.Success() 
                : ValidationResult.Error("[red]Formato de CPF inválido! Use: 000.000.000-00[/]");
        }

        public static ValidationResult ValidarEmail(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
                return ValidationResult.Error("[red]O email é obrigatório.[/]");

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.NonBacktracking);

            return emailRegex.IsMatch(Email) 
                ? ValidationResult.Success() 
                : ValidationResult.Error("[red]Formato de email inválido! Use: exemplo@dominio.com[/]");
        }

        public static ValidationResult ValidarTelefone(string Telefone)
        {
            if (string.IsNullOrWhiteSpace(Telefone))
                return ValidationResult.Error("[red]O telefone é obrigatório.[/]");

            var telRegex = new Regex(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", RegexOptions.NonBacktracking);

            return telRegex.IsMatch(Telefone) 
                ? ValidationResult.Success() 
                : ValidationResult.Error("[red]Formato de telefone inválido! Use: (00) 00000-0000[/]");
        }
        
        public static ValidationResult ValidarDataNascimento(string DataNascimento)
        {
            if (string.IsNullOrWhiteSpace(DataNascimento))
                return ValidationResult.Error("[red]A data de nascimento é obrigatória.[/]");

            if (!DateTime.TryParseExact(DataNascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var data))
                return ValidationResult.Error("[red]Formato de data inválido! Use: dd/MM/yyyy[/]");

            if (data > DateTime.Now)
                return ValidationResult.Error("[red]A data de nascimento não pode ser no futuro.[/]");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidarPreferenciaViagem(string PreferenciaViagem)
        {
            if (string.IsNullOrWhiteSpace(PreferenciaViagem) || PreferenciaViagem.Length < 3)
                return ValidationResult.Error("[red]A preferência de viagem é obrigatória.[/]");
            return ValidationResult.Success();
        }

                public static ValidationResult ValidarNivelFidelidade(string nivelFidelidade)
        {
            if (string.IsNullOrWhiteSpace(nivelFidelidade))
                return ValidationResult.Error("[red]O nível de fidelidade é obrigatório.[/]");
            
            var niveisValidos = new List<string> { "Bronze", "Prata", "Ouro", "Platina" };
            if (!niveisValidos.Contains(nivelFidelidade))
                return ValidationResult.Error("[red]Nível inválido. Escolha entre Bronze, Prata, Ouro ou Platina.[/]");

            return ValidationResult.Success();
        }

        public static List<string> VerificarCamposVazios(CadastroCliente.CriarCadastro cliente)
        {
            var faltantes = new List<string>();

            if (string.IsNullOrWhiteSpace(cliente.Nome)) faltantes.Add("Nome");
            if (string.IsNullOrWhiteSpace(cliente.Cpf)) faltantes.Add("CPF");
            if (string.IsNullOrWhiteSpace(cliente.Email)) faltantes.Add("Email");
            if (string.IsNullOrWhiteSpace(cliente.Telefone)) faltantes.Add("Telefone");
            if (string.IsNullOrWhiteSpace(cliente.DataNascimento)) faltantes.Add("Data de Nascimento");
            if (string.IsNullOrWhiteSpace(cliente.PreferenciaViagem)) faltantes.Add("Preferência de Viagem");
            if (string.IsNullOrWhiteSpace(cliente.NivelFidelidade)) faltantes.Add("Nível de Fidelidade");

            return faltantes;
        }
    }
}