using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManager.Controller
{
    class Validador
    {
        

        public bool validaNome (String nome)
        {
            bool taCerto = false;
            if (nome.Length >= 3)
            {
                taCerto = true;
            }
            return taCerto;
        }

        


    }

    

}
