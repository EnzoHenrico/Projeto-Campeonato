using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class DbCampeonato
    {
        private readonly string _stringConexao = "Data Source=127.0.0.1; Initial Catalog=DB_Futebol; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True";
        public SqlConnection Conexao;

        public DbCampeonato()
        {
            Conexao = new SqlConnection(_stringConexao);
        }

        public void MostrarClassificacao()
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
            }
        }
    }
}
