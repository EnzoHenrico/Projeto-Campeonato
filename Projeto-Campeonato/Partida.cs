using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class Partida
    {
        public readonly Clube Mandante;
        public readonly Clube Visitante;
        public readonly int Rodada;
        public int GolsMandante { get; private set;  }
        public int GolsVisitante { get; private set; }

        public Partida(Clube mandante, Clube visitante, int rodada)
        {
            Mandante = mandante;
            Visitante = visitante;
            Rodada = rodada;
            GerarPlacar();
        }

        public void GerarPlacar()
        {
            GolsMandante = new Random().Next(0, 11);
            GolsVisitante = new Random().Next(0, 11);
        }
    }
}
