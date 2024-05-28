using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class GerenciadorCampeonato
    {
        private DbCampeonato _database;
        private List<Clube> _clubes = new();
        private List<Partida> _partidas = new();
        private int _rodadaAtual = 1;
        private bool _emAndamento = false;
        private readonly int _minimoClubes = 3;
        private readonly int _maximoClubes = 5;

        public GerenciadorCampeonato()
        {
            _database = new();
            if (_database.ExistemPartidasRegistradas())
            {
                _emAndamento = true;
            }
        }

        public void IniciarCampeonato()
        {
            if (_emAndamento)
            {
                Console.WriteLine("\n─[ Um campeonato está em andamento, finalize antes de poder iniciar outro ]─\n");
                return;
            }

            // Inclui clubes no campeonato
            var inserir = true;
            while (inserir && _clubes.Count < _maximoClubes)
            {
                inserir = InputNovoClube();
            }
            if (_clubes.Count < _minimoClubes)
            {
                Console.WriteLine("\n─[ Para o campeonato são necessários de 3 a 5 times, tente novamente ]─\n");
                return;
            }

            // Gera as partidas dos times registrados
            GerarPartidas();

            Console.WriteLine("\n─[ Pressione qualquer tecla para gerar as rodadas ]─\n");
            Console.ReadKey();
            IniciarRodadas();
            
            // Armazena os dados no banco
            _database.RegistrarClubes(_clubes);
            _database.RegistrarPartidas(_partidas);
        }

        public void LimparDados()
        {
            _database.LimparRegistrosClassificacao();
            _database.LimparRegistrosPartidas();
            _database.LimparRegistrosClubes();
        }

        private bool InputNovoClube()
        {
            if (_clubes.Count >= _maximoClubes)
            {
                return false;
            }
            if (_clubes.Count >= _minimoClubes)
            {
                Console.WriteLine($"\n─[ Deseja adicionar mais um clube? digite [ 1 ] para 'Sim' e [ 2 ] para 'Não') ]─\n");
                int resposta = int.Parse(Console.ReadLine());

                if (resposta != 1)
                {
                    return false;
                }
            }
            Console.Clear();
            Console.Write("┌[   - Cadastrar novo time no campeoanto -   ]\n" +
                          "│\n");
            Console.Write($"├[ Clubes cadastrados ({_clubes.Count}/5 - Mínimo 3)\n");

            var repetir = false;
            string nome = string.Empty;
            string apelido = string.Empty;
            DateOnly dataCriacao = new();
            do
            {
                Console.Write("├[ Nome do clube: ");
                nome = Console.ReadLine();

                if (_clubes.Find(clube => clube.Nome == nome) != null)
                {
                    Console.WriteLine("\n─[ Nome já cadastrado, tente novamente ]─\n");
                    repetir = true;
                }
                else
                {
                    repetir = false;
                }
            } while (repetir);

            do
            {
                Console.Write("├[ Apelido do clube: ");
                apelido = Console.ReadLine();

                if (_clubes.Find(clube => clube.Apelido == apelido) != null)
                {
                    Console.WriteLine("\n─[ Apelido já cadastrado, tente novamente ]─\n");
                    repetir = true;
                }
                else
                {
                    repetir = false;
                }
            } while (repetir);

            do
            {
                Console.Write("└[ Data de fundação do time (DD/MM/AAAA): ");
                if (DateOnly.TryParse(Console.ReadLine(), out var entrada))
                {
                    dataCriacao = entrada;
                    repetir = false;
                }
                else
                {
                    Console.WriteLine("\n─[ Data inválida, tente novamente ]─\n");
                    repetir = true;
                }
            } while (repetir);

            _clubes.Add(new(nome, apelido, dataCriacao));

            Console.WriteLine("\n─[ Novo clube cadstrado! ]─\n");
            Console.ReadKey();
            return true;
        }
        
        private void GerarPartidas()
        {
            var quantidadeDeClubes = _clubes.Count;
            for (int i = 0; i < quantidadeDeClubes; i++)
            {
                var clubeMandante = _clubes[i];
                for (int j = 0; j < quantidadeDeClubes; j++)
                {
                    var clubeVisitante = _clubes[j];
                    if (clubeVisitante != clubeMandante)
                    {
                        _partidas.Add(new Partida(clubeMandante, clubeVisitante, _rodadaAtual));
                    }
                    _rodadaAtual++;
                }
                _rodadaAtual = 0;
            }
        }

        private void IniciarRodadas()
        {
            Console.Clear();
            Console.WriteLine("─[   * Início do Campeonato *   ]─\n");
            var partidasPorRodada = _partidas.OrderBy(partida => partida.Rodada);
            int contador = 0;
            
            foreach (var partida in partidasPorRodada)
            {
                Console.Write(
                    $"┌[ Partida {contador}: {partida.Mandante.Apelido} ({partida.GolsMandante}) X ({partida.GolsVisitante}) {partida.Visitante.Apelido}\n" +
                    "│\n" +
                    "└[");

                if (partida.GolsMandante == partida.GolsVisitante)
                {
                    Console.WriteLine("Empate!\n");
                }
                else if (partida.GolsMandante > partida.GolsVisitante)
                {
                    Console.WriteLine($"Vitória de {partida.Mandante.Apelido}\n");
                }
                else
                {
                    Console.WriteLine($"Vitória de {partida.Visitante.Apelido}\n");
                }
                contador++;
                Thread.Sleep(200);
            }
            Console.WriteLine("─[   - Fim das rodadas! Pressione qualquer tecla para voltar e visualizar mais informações -   ]─");
            Console.ReadKey();
        }
    }
}
