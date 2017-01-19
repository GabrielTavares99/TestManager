using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using TestManager.View;
using System.Data;

namespace TestManager.Model.DaoUsuario
{
    class DaoUsuario
    {
        SqlConnection conn = new Conexao().abrirConexao();

        String sql = "";
        public DataTable preencherGrid()
        {
            sql = "SELECT tbUsuario.codUsuario,tbUsuario.nomeUsuario, tbUsuario.loginUsuario, tbUsuario.senhaUsuario, tbTipoUsuario.descTipoUsuario, tbStatus.descricaoStatus FROM tbUsuario INNER JOIN tbTipoUsuario ON tbUsuario.codTipoUsuario = tbTipoUsuario.codTipoUsuario JOIN tbStatus ON tbStatus.codStatus = tbUsuario.codStatus";
            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataUsuario = new DataTable();
            adaptador.Fill(dataUsuario);


            return dataUsuario;

        }

        public DataTable preencherGridConsulta(String consulta)
        {
            sql = "SELECT tbUsuario.codUsuario,tbUsuario.nomeUsuario, tbUsuario.loginUsuario, tbUsuario.senhaUsuario, tbTipoUsuario.descTipoUsuario, tbStatus.descricaoStatus FROM tbUsuario INNER JOIN tbTipoUsuario ON tbUsuario.codTipoUsuario = tbTipoUsuario.codTipoUsuario JOIN tbStatus ON tbStatus.codStatus = tbUsuario.codStatus WHERE tbUsuario.nomeUsuario LIKE '" + consulta+"%'";
            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataUsuario = new DataTable();
            adaptador.Fill(dataUsuario);


            return dataUsuario;

        }

        public Boolean cadastrar(Usuario usuario)
        {
             sql = "INSERT INTO tbUsuario (nomeUsuario,loginUsuario,senhaUsuario,codTipoUsuario,codStatus) VALUES('"+usuario.Nome+"','"+usuario.Login+"','"+usuario.Senha+"',"+usuario.TipoUsuario+",3)";
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

        public Boolean consultarAdm(int codUsuario)
        {
            sql = "SElECT codTipoUsuario FROM tbUsuario WHERE codUsuario =  " + codUsuario;
            SqlCommand comando = new SqlCommand(sql, conn);
            comando.ExecuteNonQuery();
            int cod;
            cod = Convert.ToInt32(comando.ExecuteScalar());

            if (cod == 1)
            {
                //MessageBox.Show(""+cod);
                return false;
            }
            else
            {
                return true;
            }

        }

        public Boolean remover(int codUsuario) {
            sql = "SELECT tbUsuario.codStatus FROM tbUsuario WHERE codUsuario = "+codUsuario+"";

            SqlCommand comando = new SqlCommand(sql,conn);
            int codigo = Convert.ToInt16(comando.ExecuteScalar());


            try
            {
                if (codigo == 3)
                {
                    sql = "UPDATE tbUsuario SET codStatus = 4 WHERE codUsuario = " + codUsuario + "";
                    comando = new SqlCommand(sql, conn);
                    comando.ExecuteNonQuery();
                    
                }
                else if (codigo == 4)
                {
                    sql = "UPDATE tbUsuario SET codStatus = 3 WHERE codUsuario = " + codUsuario + "";
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
     
        public void abrirTela(int cod, Aluno alu) {
            if (cod == 3)
            {
                ViewAluno aluno = new ViewAluno(alu);
 
                aluno.Show();
            }
            else if (cod == 2)
            {
                ViewProfessor professor = new ViewProfessor();
                professor.Show();
            }
            else {
                ViewAdministrador adm = new ViewAdministrador();
                adm.Show();
            
            }

 
        } 

        public Boolean Login(String loginUsuario, String senhaUsuario)
        {
            Aluno aluno = new Aluno();
            bool status = false;

                sql = "SELECT * FROM tbUsuario ";
                
                SqlCommand comando = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = comando.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["loginUsuario"].ToString().Equals(loginUsuario) 
                    && sqlDataReader["senhaUsuario"].ToString().Equals(senhaUsuario)
                    && Convert.ToInt16( sqlDataReader["codStatus"] ) == 3)
                    {
                    aluno.Cod =Convert.ToInt16( sqlDataReader["codUsuario"] );
                    aluno.Nome = Convert.ToString(sqlDataReader["nomeUsuario"]);
                    aluno.Login = Convert.ToString(sqlDataReader["loginUsuario"]);
                    aluno.TipoUsuario = Convert.ToInt16(sqlDataReader["codTipoUsuario"]);

                        int cod = Convert.ToInt16(sqlDataReader["codTipoUsuario"]);
                        abrirTela(cod,aluno);
                        status = true;

                        
                        break;
                    }
                    else {
                        status = false;
                        }
                }
            return status;
        }

        public Boolean Editar(Usuario usuario)
        {
            //MessageBox.Show(""+usuario.TipoUsuario);

            if (!consultarAdm(usuario.Cod))
            {
                sql = "UPDATE tbUsuario SET nomeUsuario = '" + usuario.Nome + "', loginUsuario = '" + usuario.Login + "', senhaUsuario = '" + usuario.Senha + "' WHERE codUsuario =" + usuario.Cod + "";
            }
            else
            {
                sql = "UPDATE tbUsuario SET nomeUsuario = '" + usuario.Nome + "', loginUsuario = '" + usuario.Login + "', senhaUsuario = '" + usuario.Senha + "', codTipoUsuario = " + usuario.TipoUsuario + " WHERE codUsuario =" + usuario.Cod + "";
            }

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
