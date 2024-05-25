using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class DbCampeonato
    {
        public SqlConnection Conexao;

        public DbCampeonato()
        {
            Conexao = new SqlConnection(
                "Data Source=127.0.0.1; " +
                "Initial Catalog=DB_Futebol; " +
                "User Id=sa; " +
                "Password=SqlServer2019!; " +
                "TrustServerCertificate=True");
        }

        public void ExibirClassificacaoFinal()
        {
            try
            {
                Conexao.Open();
                SqlCommand cmd = new();
                cmd.CommandText = "SELECT b.Apelido, a.Pontuacao, a.Vitorias, a.Derrotas, a.Empates, a.SaldoGols " +
                                  "FROM Classificacao a " +
                                  "JOIN Clube b " +
                                  "ON a.Clube_id = b.Clube_id " +
                                  "ORDER BY a.Pontuacao DESC;";
                cmd.Connection = Conexao;
                cmd.ExecuteNonQuery();

                using (var reader = cmd.ExecuteReader())
                {
                    Console.Clear();
                    Console.WriteLine("        ┌[   * Tabela Final do Campeonato *   ]┐        ");
                    Console.WriteLine("┌──────────────────────┬──────┬─────┬─────┬─────┬──────┐");
                    Console.WriteLine("│ Time                 │ Ptos │  V  │  D  │  E  │  SG  │");
                    Console.WriteLine("├──────────────────────┼──────┼─────┼─────┼─────┼──────┤");

                    while (reader.Read())
                    {
                        Console.Write($"│{reader.GetString(0)}".PadRight(23));
                        Console.Write($"│{reader.GetInt32(1)}".PadRight(7));
                        Console.Write($"│{reader.GetInt32(2)}".PadRight(6));
                        Console.Write($"│{reader.GetInt32(3)}".PadRight(6));
                        Console.Write($"│{reader.GetInt32(4)}".PadRight(6));
                        Console.Write($"│{reader.GetInt32(5)}".PadRight(7));
                        Console.Write("│\n");
                    }
                    Console.Write($"└──────────────────────┴──────┴─────┴─────┴─────┴──────┘\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro de conexão: " + e.Message);
            }
            finally
            {
                Conexao.Close();
                Console.WriteLine("\n─[ Pessione qualquer tecla para voltar a navegação ]─\n");
                Console.ReadKey();
            }
        }

        public void ExibirDadosDoCampeao()
        {
            try
            {
                string nome = string.Empty, apelido = string.Empty;
                var dataCriacao = new DateTime();
                int pontuacao = 0, 
                    vitorias = 0, 
                    derrotas = 0, 
                    empates = 0, 
                    golsContra = 0, 
                    golsPro = 0, 
                    saldoGols = 0;

                Conexao.Open();
                SqlCommand cmd = new()
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = Conexao
                };

                // Procedure Estatisticas do campeão
                cmd.CommandText = "ESTATISTICAS_CAMPEAO";
                cmd.ExecuteScalar();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nome = reader.GetString(0);
                        apelido = reader.GetString(1);
                        dataCriacao = reader.GetDateTime(2);
                        vitorias = reader.GetInt32(3);
                        derrotas = reader.GetInt32(4);
                        empates = reader.GetInt32(5);
                        pontuacao = reader.GetInt32(6);
                        golsContra = reader.GetInt32(7);
                        golsPro = reader.GetInt32(8);
                        saldoGols = reader.GetInt32(9);
                    }
                }

                var dataFormatada = $"{dataCriacao.Day}/{dataCriacao.Month}/{dataCriacao.Year}";
                Console.Clear();
                Console.WriteLine(
                    "        ┌[     PARABÉNS AO CAMPEÃO     ]┐        \n" +
                    "┌──[*****************************************]──┐\n" +
                    "│                                               │\n" +
                    "│       ┌───────────────────────────────┐       │\n" +
                    "│       │ "+$"Clube: {nome}".PadRight(30)+"│       │\n" +
                    "│       ├───────────────────────────────┤       │\n" +
                    "│       │ "+$"Apelido: {apelido}".PadRight(30)+"│       │\n" +
                    "│       │ "+$"Data de criação: {dataFormatada}".PadRight(30)+"│       │\n" +
                    "│       ├───────────────────────────────┤       │\n" +
                    "│       │ "+$"Pontuação total: {pontuacao}".PadRight(30)+"│       │\n" +
                    "│       │ "+$"Vitórias :{vitorias}".PadRight(30)+"│       │\n" +
                    "│       │ "+$"Derrotas: {derrotas}".PadRight(30)+"│       │\n" +
                    "│       │ "+$"Empates: {empates}".PadRight(30)+"│       │\n" +
                    "│       ├───────────────────────────────┤       │\n" +
                    "│       │ " + $"Total de gols feitos: {golsPro}".PadRight(30)+"│       │\n" +
                    "│       │ "+$"Total de gols Tomados: {golsContra}".PadRight(30)+"│       │\n" +
                    "│       │ "+$"Saldo de gols Final: {saldoGols}".PadRight(30)+"│       │\n" +
                    "│       └───────────────────────────────┘       │\n" +
                    "│                                               │\n" +
                    "└──[*****************************************]──┘\n"
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Conexao.Close();
                Console.WriteLine("\n─[ Pessione qualquer tecla para voltar a navegação ]─\n");
                Console.ReadKey();
            }
        }

        public void ExibirEstatisticasDoCampeonato()
        {
            Console.Clear();
            try
            {
                Conexao.Open();
                SqlCommand cmd = new()
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = Conexao
                };

                // Procedure Estatisticas do campeão
                cmd.CommandText = "ESTATISTICAS_CAMPEAO";
                cmd.ExecuteScalar();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                        Console.WriteLine("┌[ O Campeão ");
                        Console.WriteLine("┌──────────────────────┬────────────────┐");
                        Console.WriteLine("│ Clube Vencedor       │ Pontos somados │");
                        Console.WriteLine("├──────────────────────┼────────────────┤");

                        while (reader.Read())
                        {
                            Console.Write($"│ {reader.GetString(1)}".PadRight(23));
                            Console.Write($"│ {reader.GetInt32(6)}".PadRight(17) + "│\n");
                        }
                        Console.Write($"└──────────────────────┴────────────────┘\n\n");
                }

                // Procedure Partida com mais gols
                cmd.CommandText = "PARTIDA_COM_MAIS_GOLS";
                cmd.ExecuteScalar();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("┌[ Maior quantidade de gols feitos em uma única partida");
                    Console.WriteLine("┌──────────────────────┬──────────────────────┬─────────────┐");
                    Console.WriteLine("│ Clube Mandante       │ Clube Visitante      │ Gols Totais │");
                    Console.WriteLine("├──────────────────────┼──────────────────────┼─────────────┤");
                    while (reader.Read())
                    {
                        Console.Write($"│ {reader.GetString(0)} ({reader.GetInt32(2)})".PadRight(23));
                        Console.Write($"│ {reader.GetString(1)} ({reader.GetInt32(3)})".PadRight(23));
                        Console.Write($"│ {reader.GetInt32(4)}".PadRight(14));
                        Console.Write("│\n");
                    }
                    Console.Write($"└──────────────────────┴──────────────────────┴─────────────┘\n\n");
                }

                // Procedure Clube que fez mais gols
                cmd.CommandText = "CLUBE_QUE_FEZ_MAIS_GOLS";
                cmd.ExecuteScalar();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("┌[ Clube que mais fez gols no campeonato");
                    Console.WriteLine("┌──────────────────────┬─────────────┐");
                    Console.WriteLine("│ Clube                │ Gols Feitos │");
                    Console.WriteLine("├──────────────────────┼─────────────┤");

                    while (reader.Read())
                    {
                        Console.Write($"│ {reader.GetString(0)}".PadRight(23));
                        Console.Write($"│ {reader.GetInt32(1)}".PadRight(14));
                        Console.Write("│\n");
                    }
                    Console.Write($"└──────────────────────┴─────────────┘\n\n");
                }

                // Procedure Clube que tomou mais gols
                cmd.CommandText = "CLUBE_QUE_TOMOU_MAIS_GOLS";
                cmd.ExecuteScalar();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("┌[ Clube que mais tomou gols no campeonato");
                    Console.WriteLine("┌──────────────────────┬──────────────┐");
                    Console.WriteLine("│ Clube                │ Gols Tomados │");
                    Console.WriteLine("├──────────────────────┼──────────────┤");

                    while (reader.Read())
                    {
                        Console.Write($"│ {reader.GetString(0)}".PadRight(23));
                        Console.Write($"│ {reader.GetInt32(1)}".PadRight(15));
                        Console.Write("│\n");
                    }
                    Console.Write($"└──────────────────────┴──────────────┘\n\n");
                }

                // Procedure Maior quantidade de gols marcados por clube
                cmd.CommandText = "MAIOR_NUMERO_GOLS_POR_CLUBE";
                cmd.ExecuteScalar();

                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("┌[ Maior quantidade de gols feitos por cada clube em uma partida");
                    Console.WriteLine("┌──────────────────────┬─────────────┐");
                    Console.WriteLine("│ Clube                │ Gols Feitos │");
                    Console.WriteLine("├──────────────────────┼─────────────┤");

                    while (reader.Read())
                    {
                        Console.Write($"│ {reader.GetString(0)}".PadRight(23));
                        Console.Write($"│ {reader.GetInt32(1)}".PadRight(14));
                        Console.Write("│\n");
                    }
                    Console.Write($"└──────────────────────┴─────────────┘\n\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Conexao.Close();
                Console.WriteLine("\n─[ Pessione qualquer tecla para voltar ]─\n");
                Console.ReadKey();
            }
        }
    }
}
