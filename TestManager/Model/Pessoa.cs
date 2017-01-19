using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManager
{
    class Pessoa
    {
        private String nome, email, cpf, login, senha;
        private int cod;

        public String Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public String Login
        {
            get { return login; }
            set { login = value; }
        }

        public String Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
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
