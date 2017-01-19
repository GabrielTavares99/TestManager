using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TestManager.Model.DaoDisciplina
{
    class DaoDisciplina
    {
        String sql = "";
        SqlConnection conn = new Conexao().abrirConexao();
        public DataTable preencherGrid()
        {
            
            SqlCommand comando = new SqlCommand("SELECT tbDisciplina.codDisciplina, tbDisciplina.descDisciplina, tbStatus.descricaoStatus FROM tbDisciplina INNER JOIN tbStatus ON tbStatus.codStatus = tbDisciplina.codStatus", conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataProduto = new DataTable();
            adaptador.Fill(dataProduto);

            return dataProduto;

        }
        public Boolean Editar(Disciplina disciplina)
        {
            sql = "UPDATE tbDisciplina SET descDisciplina = '" + disciplina.Descricao + "' WHERE codDisciplina =" + disciplina.Cod + "";

            try
            {
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public DataTable preencherGridConsulta(String consulta)
        {
            SqlCommand comando = new SqlCommand("SELECT tbDisciplina.codDisciplina, tbDisciplina.descDisciplina, tbStatus.descricaoStatus FROM tbDisciplina INNER JOIN tbStatus ON tbStatus.codStatus = tbDisciplina.codStatus WHERE descDisciplina LIKE '" + consulta+"%'", conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataProduto = new DataTable();
            adaptador.Fill(dataProduto);
            return dataProduto;

        }


      
        public Boolean remover(int cod)
        {
            sql = "SELECT tbDisciplina.codStatus FROM tbDisciplina WHERE codDisciplina = " + cod + "";

            SqlCommand comando = new SqlCommand(sql, conn);
            int codigo = Convert.ToInt16(comando.ExecuteScalar());

            try
            {
                if (codigo == 3)
                {
                    sql = "UPDATE tbDisciplina SET codStatus = 4 WHERE codDisciplina = " + cod + "";
                    comando = new SqlCommand(sql, conn);
                    comando.ExecuteNonQuery();

                }
                else if (codigo == 4)
                {
                    sql = "UPDATE tbDisciplina SET codStatus = 3 WHERE codDisciplina = " + cod + "";
                    comando = new SqlCommand(sql, conn);
                    comando.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public Boolean Inserir(Disciplina disciplina) {
            sql = "INSERT INTO tbDisciplina (descDisciplina,codStatus) VALUES ('"+disciplina.Descricao+"',3)";

            try
            {
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
