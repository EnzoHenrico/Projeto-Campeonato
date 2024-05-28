using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class MenuCampeonato
    {
        public static void Visualizar()
        {
            var database = new DbCampeonato();
            var repetir = true;
            do
            {
                ExibirOpcoesVisualizar();
                var opcao = InputOpcaoMenu();
                switch (opcao)
                {
                    case 1:
                        database.ExibirEstatisticasDoCampeonato();
                        break;
                    case 2:
                        database.ExibirClassificacaoFinal();
                        break;
                    case 3:
                        database.ExibirDadosDoCampeao();
                        break;
                    case 0:
                        repetir = false;
                        break;
                }
            } while (repetir);
        }

        public static void Gerenciar()
        {
            var gerenciador = new GerenciadorCampeonato();
            var repetir = true;
            do
            {
                ExibirOpcoesGerenciar();
                var opcao = InputOpcaoMenu();
                switch (opcao)
                {
                    case 1:
                        gerenciador.IniciarCampeonato();
                        break;
                    case 2:
                        Console.WriteLine($"\n─[ Deseja realmente limpar os dados do campeonato? digite [ 1 ] para 'Sim' e [ 2 ] para 'Não') ]─\n");
                        var confirma = int.Parse(Console.ReadLine()) == 1;
                        if (confirma)
                        {
                            gerenciador.LimparDados();
                        }
                        Console.WriteLine("\n─[ Dados deletados com sucesso! ]─\n");
                        Console.ReadLine();
                        break;
                    case 0:
                        repetir = false;
                        break;
                }
            } while (repetir);
        }

        public static void ExibirOpcoesVisualizar()
        {
            Console.Clear();
            Console.Write("┌[  - Visualizar Campeonato -  ]\n" +
                          "│\n" +
                          "├[ 1 ]─ Estatísticas do campeonato\n" +
                          "├[ 2 ]─ Classifição Final do campeonato\n" +
                          "├[ 3 ]─ Informações do Campeão\n" +
                          "├[ 0 ]─ Voltar ao menu\n" +
                          "│\n" +
                          "└[ Digite uma das opções acima:   ");
        }

        public static void ExibirOpcoesGerenciar()
        {
            Console.Clear();
            Console.Write("┌[  - Opções do Campeonato -  ]\n" +
                          "│\n" +
                          "├[ 1 ]─ Iniciar novo campeonato\n" +
                          "├[ 2 ]─ Limpar informações do último campeonato\n" +
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
