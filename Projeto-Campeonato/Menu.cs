using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class Menu
    {

        public static void ExibirOpcoes()
        {
            Console.WriteLine("┌[ Visuaçização das informações do campeonato, escolha uma opção: \n" +
                              "│\n" +
                              "├[ 1 ]─ Informações do Campeão\n" +
                              "├[ 2 ]─ Top 5 do campeonato\n" +
                              "├[ 3 ]─ Classifição completa\n" +
                              "├[ 4 ]─ Time com maior número de gols feitos\n" +
                              "├[ 5 ]─ Time com maior número de gols recebidos\n" +
                              "├[ 6 ]─ Partida com maior número de gols\n" +
                              "├[ 7 ]─ Maior número de gols em uma única partida, por equipe\n" +
                              "├[ 0 ]─ Sair do menu" +
                              "│\n" + 
                              "└[Opção: ");
        }

        public static int InputOpcao()
        {
            int inteiro = 0;
            bool valor = false;

            while (!valor)
            {
                if (int.TryParse(Console.ReadLine(), out int varint))
                {
                    inteiro = varint;
                    valor = true;
                }
                else
                {
                    Console.WriteLine("\n─[ Caractere inválido, digite novamente ]─\n");
                }
            }
            return inteiro;
        }
    }
}
