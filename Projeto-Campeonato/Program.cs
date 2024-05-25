using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Projeto_Campeonato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var campeonato = new DbCampeonato();
            var repetir = true;
            do
            {
                Console.Clear();
                Console.Write("┌[   * Campeonato de futebol *   ]\n" +
                              "│\n" +
                              "├[ 1 ]─ Iniciar novo torneio\n" +
                              "├[ 2 ]─ Visualizar dados do último torneio\n" +
                              "├[ 0 ]─ Sair do programa\n" +
                              "│\n" +
                              "└[ Digite uma das opções acima:   ");
                var opcao = InformacoesTorneio.InputOpcaoMenu();
                switch (opcao)
                {
                    case 1:
                        
                        break;
                    case 2:
                        InformacoesTorneio.Visualizar(campeonato);
                        break;
                    case 0:

                        repetir = false;
                        break;
                }
            } while (repetir);
        }
    }
}
