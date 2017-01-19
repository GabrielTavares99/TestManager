using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TestManager.Model.DaoResultado
{
    class DaoResultado
    {

        SqlConnection conn = new Conexao().abrirConexao();
        SqlDataReader dr;
        String sql = "";
        SqlCommand comando;
        
        public List<Aluno> preencherAlunos ()
            {
                List<Aluno> listaAlunos = new List<Aluno>();

            sql = "SELECT tbUsuario.codUsuario, tbUsuario.nomeUsuario FROM tbUsuario WHERE codTipoUsuario = 3";

            comando = new SqlCommand(sql);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            return listaAlunos;        
            }


    }
}
