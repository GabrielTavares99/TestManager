using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManager.Model
{
    public class Resposta
    {
        private int tipo;
        private String descReposta;
        private int cod;
              
        
        public string DescReposta
        {
            get
            {
                return descReposta;
            }

            set
            {
                descReposta = value;
            }
        }

        public int Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        public int Cod
        {
            get
            {
                return cod;
            }

            set
            {
                cod = value;
            }
        }
    }
}
