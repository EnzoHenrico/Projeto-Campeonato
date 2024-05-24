using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Projeto_Campeonato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new DbCampeonato();
            bool repetir = true;
            do
            {
                Menu.ExibirOpcoes();
                int opcao = Menu.InputOpcao();
                switch (opcao)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:
                        db.MostrarClassificacao();
                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 0:

                        repetir = false;
                        break;
                }
            } while (repetir);
        }
    }
}
