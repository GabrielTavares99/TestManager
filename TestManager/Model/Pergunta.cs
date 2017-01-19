using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManager.Model.DaoPergunta
{
    class Pergunta
    {
        public int Cod { get; set; }
        public String Descricao { get; set; }
        public int Disciplina { get; set; }


       Avaliacao avaliacao = new Avaliacao();

        public Avaliacao Avaliacao
        {
            get
            {
                return avaliacao;
            }

            set
            {
                avaliacao = value;
            }
        }


    }
}
