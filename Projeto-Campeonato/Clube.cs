using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Campeonato
{
    internal class Clube
    {
        public string Nome { get; private set; }
        public string Apelido { get; private set; }
        public DateOnly DataCriacao { get; private set; }

        public Clube(string nome, string apelido, DateOnly dataCriacao)
        {
            Nome = nome;
            Apelido = apelido;
            DataCriacao = dataCriacao;
        }
    }
}
