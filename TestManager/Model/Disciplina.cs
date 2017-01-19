using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManager.Model
{
    public class Disciplina
    {
        public int Cod { get; set; }
            public String Descricao { get; set; }

        public int pegarValor()
        {


            return Cod;
        }

        public override string ToString()
        {
            return Convert.ToString(this.Cod);
        }

        

    }

    
}
