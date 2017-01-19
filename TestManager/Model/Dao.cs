using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TestManager.Model
{
    class Dao
    {
        String sql = "";
        public DataTable preencherGrid()
        {
            SqlConnection conn = new Conexao().abrirConexao();

            SqlCommand comando = new SqlCommand("SELECT * FROM tbDisciplina", conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataProduto = new DataTable();
            adaptador.Fill(dataProduto);


            return dataProduto;

        }

    }
}
