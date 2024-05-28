using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Projeto_Campeonato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repetir = true;
            do
            {
                Console.Clear();
                Console.Write("┌[   * Campeonato de futebol *   ]\n" +
                              "│\n" +
                              "├[ 1 ]─ Gerenciar campeonato\n" +
                              "├[ 2 ]─ Visualizar dados do último campeonato\n" +
                              "├[ 0 ]─ Sair do programa\n" +
                              "│\n" +
                              "└[ Digite uma das opções acima:   ");
                var opcao = MenuCampeonato.InputOpcaoMenu();
                switch (opcao)
                {
                    case 1:
                        MenuCampeonato.Gerenciar();
                        break;
                    case 2:
                        MenuCampeonato.Visualizar();
                        break;
                    case 0:
                        repetir = false;
                        break;
                }
            } while (repetir);
        }
    }
}
