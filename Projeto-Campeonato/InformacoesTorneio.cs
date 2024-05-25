using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class InformacoesTorneio
    {
        public static void Visualizar(DbCampeonato campeonato)
        {
            var repetir = true;
            do
            {
                ExibirOpcoesMenu();
                var opcao = InputOpcaoMenu();
                switch (opcao)
                {
                    case 1:
                        campeonato.ExibirEstatisticasDoCampeonato();
                        break;
                    case 2:
                        campeonato.ExibirClassificacaoFinal();
                        break;
                    case 3:
                        campeonato.ExibirDadosDoCampeao();
                        break;
                    case 0:
                        repetir = false;
                        break;
                }
            } while (repetir);
        }

        public static void ExibirOpcoesMenu()
        {
            Console.Clear();
            Console.Write("┌[  - Visuaçização das informações do Campeonato -  ]\n" +
                          "│\n" +
                          "├[ 1 ]─ Estatísticas do campeonato\n" +
                          "├[ 2 ]─ Classifição Final do Campeonato\n" +
                          "├[ 3 ]─ Informações do Campeão\n" +
                          "├[ 0 ]─ Voltar ao menu\n" +
                          "│\n" +
                          "└[ Digite uma das opções acima:   ");
        }

        public static int InputOpcaoMenu()
        {
            var input = 0;
            var solicitar = true;
            while (solicitar)
            {
                var entradaValida = int.TryParse(Console.ReadLine(), out var entrada);
                if (entradaValida && entrada is <= 3 and >= 0)
                {
                    input = entrada;
                    solicitar = false;
                }
                else
                {
                    Console.WriteLine("\n─[ Opção inválida, aperte qualquer tecla para voltar ]─\n");
                    Console.ReadKey();
                }
            }
            return input;
        }
    }
}
