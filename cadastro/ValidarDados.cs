using System.Text.RegularExpressions;
using Spectre.Console;  
using System.Globalization;

namespace validar
{
    public static class ValidarDados
    {
        public static ValidationResult ValidarNome(string Nome)
        {
            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3)
                return ValidationResult.Error("[red]O nome deve conter pelo menos 3 caracteres.[/]");

            return Regex.IsMatch(Nome, @"^[a-zA-ZÀ-ÿ\s]+$")
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]O nome deve conter apenas letras e espaços.[/]");
        }

        public static ValidationResult ValidarCpf(string Cpf)
        {
            if (string.IsNullOrWhiteSpace(Cpf))
                return ValidationResult.Error("[red]O CPF é obrigatório.[/]");

            var regex = new Regex(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$");
            return regex.IsMatch(Cpf) 
                ? ValidationResult.Success() 
                : ValidationResult.Error("[red]Formato de CPF inválido! Use: 000.000.000-00[/]");
        }

        public static ValidationResult ValidarEmail(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
                return ValidationResult.Error("[red]O email é obrigatório.[/]");

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(Email) 
                ? ValidationResult.Success() 
                : ValidationResult.Error("[red]Formato de email inválido! Use: exemplo@dominio.com[/]");
        }

        public static ValidationResult ValidarTelefone(string Telefone)
        {
            if (string.IsNullOrWhiteSpace(Telefone))
                return ValidationResult.Error("[red]O telefone é obrigatório.[/]");

            var regex = new Regex(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$");
            return regex.IsMatch(Telefone) 
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

            public static ValidationResult ValidarNivelFidelidade(string NivelFidelidade)
            {
                if (string.IsNullOrWhiteSpace(NivelFidelidade))
                    return ValidationResult.Error("[red]O nível de fidelidade é obrigatório.[/]");
                return ValidationResult.Success();
            }

    }
}