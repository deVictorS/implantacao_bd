using DotNetEnv;
using MySqlConnector;
using System;
using Spectre.Console;

namespace conectar
{
    public static class Conectar
    {
        public static MySqlConnection ObterConexaoAberta()
        {
            Env.Load();
            string stringConexao = $"Server={Env.GetString("DB_SERVER")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_DATABASE")};Uid={Env.GetString("DB_USER")};Pwd={Env.GetString("DB_PASSWORD")};";
            
            var conexao = new MySqlConnection(stringConexao);
            
            try 
            {
                conexao.Open();
                AnsiConsole.MarkupLine("[green bold]Conectado ao banco RDS AWS com sucesso![/]");
                return conexao;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red bold]Falha ao conectar ao banco:[/] {ex.Message}");
                throw;
            }
        }
    }
}