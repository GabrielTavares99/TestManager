using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace TestManager.Model
{
    class Conexao
    {
        //fsdkfdsfdsfds
        public SqlConnection abrirConexao()
        {
            SqlConnection conn = new SqlConnection("Data Source=(Local);Initial catalog=bdPerguntasRespostas;Integrated Security=SSPI");
            conn.Open();
            return conn;
        }
    }
}
