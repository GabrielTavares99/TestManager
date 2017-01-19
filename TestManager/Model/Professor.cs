using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManager
{
    class Professor:Pessoa
    {
        private String matricula;

        public String Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }
        private List<string> disciplina = new List<string>();

        public List<string> Disciplina
        {
            get { return disciplina; }
            set { disciplina = value; }
        }

        public String Apresentacao(Professor p)
        {
            String ap;
            return ap=p.Nome+"\n"+p.Matricula+"\n"+p.Login+"\n"+p.Cpf+"\n"+p.Senha+"\n";

        }

        
    }

}
