using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestManager.Model.DaoPergunta
{
    class DaoPergunta : Dao
    {
        String sql = "";

        public void editarCerta(int codPergunta, String resposta)
        {
            sql = "UPDATE tbResposta SET resposta = '" + resposta + "' WHERE codPergunta = " + codPergunta + "AND codTipoResposta = 1";

            SqlCommand comando = new SqlCommand(sql, conn);
            comando.ExecuteNonQuery();
        }

        public Boolean editarAlternativa(int codPergunta, String[] novasRespostas)
        {
            SqlConnection conn = new Conexao().abrirConexao();
            List<Resposta> resposta = new List<Resposta>();
            sql = "SELECT * FROM tbResposta WHERE codPergunta = " + codPergunta + " AND codTipoResposta = 2";
            try
            {
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);

                SqlDataReader dr = comando.ExecuteReader();
                Resposta r;
                while (dr.Read())
                {
                    r = new Resposta();
                    r.DescReposta = Convert.ToString(dr["resposta"]);
                    r.Tipo = Convert.ToInt16(dr["codTipoResposta"]);
                    r.Cod = Convert.ToInt16(dr["codResposta"]);
                    resposta.Add(r);
                }
                dr.Close();
            }
            catch
            {
                MessageBox.Show("ERRO 1");
            }
            try
            {

                for (int i = 0; i < novasRespostas.Length; i++)
                {
                    
                    String sql = "UPDATE tbResposta set resposta = '" + novasRespostas[i] + "' WHERE  codResposta = " + Convert.ToInt16(resposta.ElementAt(i).Cod);
                    SqlCommand comando = new SqlCommand(sql, conn);
                    comando.ExecuteNonQuery();
            }
            return true;
            }
            catch
            {
                return false;
            }
        }
 
        public List<Avaliacao> preencherAvaliacao(int cod)
        {

            List<Avaliacao> avaliacao = new List<Avaliacao>();

            SqlConnection conn = new Conexao().abrirConexao();

           sql = " SELECT tbAvaliacao.codAvaliacao, tbAvaliacao.descricaoAvaliacao, tbDisciplina.codDisciplina, tbAvaliacao.descricaoAvaliacao FROM tbAvaliacao INNER JOIN tbDisciplina on tbAvaliacao.codDisciplina = tbDisciplina.codDisciplina WHERE tbDisciplina.codDisciplina = "+cod;


            SqlCommand comando = new SqlCommand(sql,conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            
            SqlDataReader dr = comando.ExecuteReader();

            while (dr.Read())
            {
                Avaliacao av = new Avaliacao() {
                    Cod = Convert.ToInt16(dr["codAvaliacao"]),
                    Descricao = Convert.ToString(dr["descricaoAvaliacao"])
                };

                avaliacao.Add(av);

            }
            dr.Close();

            return avaliacao;
        }

        public DataTable preencherAvaliacaoGrid(int cod)
        {
            SqlConnection conn = new Conexao().abrirConexao();

            String sql = " SELECT tbAvaliacao.codAvaliacao, tbAvaliacao.descricaoAvaliacao FROM tbAvaliacao INNER JOIN tbDisciplina on tbAvaliacao.codDisciplina = tbDisciplina.codDisciplina where tbDisciplina.codDisciplina = " + cod;
            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataUsuario = new DataTable();
            adaptador.Fill(dataUsuario);

            return dataUsuario;
        }

        public List<Disciplina> preencherDisciplina()
        {
            SqlConnection conn = new Conexao().abrirConexao();

            SqlCommand comando = new SqlCommand("SELECT tbDisciplina.codDisciplina, tbDisciplina.descDisciplina,tbDisciplina.codStatus FROM tbDisciplina", conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            List<Disciplina> disciplina = new List<Disciplina>();


            SqlDataReader dr = comando.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToInt16(dr["codStatus"]) == 3) {
                    Disciplina p = new Disciplina()
                    {
                        Descricao = Convert.ToString(dr["descDisciplina"]),
                        Cod = Convert.ToInt16(dr["codDisciplina"]),

                    };
                    disciplina.Add(p);
                }
            }
            dr.Close();
            return disciplina;

        }

        public DataTable preencherGrid()
        {
            SqlConnection conn = new Conexao().abrirConexao();

            sql = "SELECT tbPergunta.codPergunta,tbPergunta.descPergunta,tbDisciplina.descDisciplina, tbAvaliacao.descricaoAvaliacao,tbStatus.descricaoStatus FROM tbPergunta INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina JOIN tbAvaliacao ON tbAvaliacao.codAvaliacao = tbPergunta.codAvaliacao JOIN tbStatus ON tbStatus.codStatus = tbPergunta.codStatus";
            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataUsuario = new DataTable();
            adaptador.Fill(dataUsuario);

            return dataUsuario;

        }

        public DataTable preencherGrid(String consulta)
        {
            SqlConnection conn = new Conexao().abrirConexao();

            sql = "SELECT tbPergunta.codPergunta,tbPergunta.descPergunta,tbDisciplina.descDisciplina, tbAvaliacao.descricaoAvaliacao, tbStatus.descricaoStatus FROM tbPergunta INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina JOIN tbStatus ON tbStatus.codStatus = tbPergunta.codStatus WHERE tbPergunta.descPergunta LIKE '" + consulta+"%'";
            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataUsuario = new DataTable();
            adaptador.Fill(dataUsuario);

            return dataUsuario;

        }

        public String[] preencherAlternativas(int codPergunta)
        {
           
            String[] alternativa = new String[5];

            String sql = "SELECT resposta FROM tbResposta WHERE codPergunta = "+codPergunta+"";

            SqlConnection conn = new Conexao().abrirConexao();
            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            SqlDataReader dr = comando.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                alternativa[i] = Convert.ToString( dr["resposta"] );
                i++;
            }
            dr.Close();
            return alternativa;
        }

        SqlConnection conn = new Conexao().abrirConexao();

        public Boolean remover(int codPergunta)
        {
            sql = "SELECT tbPergunta.codStatus FROM tbPergunta WHERE codPergunta = " + codPergunta + "";

            SqlCommand comando = new SqlCommand(sql, conn);
            int codigo = Convert.ToInt16(comando.ExecuteScalar());


            try
            {
                if (codigo == 3)
                {
                    sql = "UPDATE tbPergunta SET codStatus = 4 WHERE codPergunta = " + codPergunta + "";
                    comando = new SqlCommand(sql, conn);
                    comando.ExecuteNonQuery();

                }
                else if (codigo == 4)
                {
                    sql = "UPDATE tbPergunta SET codStatus = 3 WHERE codPergunta = " + codPergunta + "";
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

        public Boolean removerAvaliacao(int cod)
        {
            try
            {
                sql = "DELETE FROM tbAvaliacao WHERE codAvaliacao = " + cod;
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
           
        }

        public void deletarAlternativas(int cod)
        {
            SqlCommand comando;
            sql = "DELETE FROM tbResposta WHERE codPergunta =" +cod;
            comando = new SqlCommand(sql, conn);
            comando.ExecuteNonQuery();
        }

        public Boolean Editar(Pergunta pergunta)
        {
            sql = "UPDATE tbPergunta SET descPergunta = '" + pergunta.Descricao + "', codDisciplina = "+pergunta.Disciplina+", codAvaliacao = "+pergunta.Avaliacao.Cod+" WHERE codPergunta =" +pergunta.Cod + "";
            SqlCommand comando;
            try
            {
                comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();      
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int codigoPergunta()
        {
            sql = "select  MAX(codPergunta) FROM tbPergunta";
            SqlCommand comando = new SqlCommand(sql, conn);
            comando.ExecuteNonQuery();
            int cod;
            cod = Convert.ToInt32(comando.ExecuteScalar());

            return cod;
        }

        public Boolean cadastroPerguntaCorreta(String resposta,int codPergunta,int codAvaliacao)
        {

            try
            {
                sql = "INSERT INTO tbResposta (resposta,codPergunta,codTipoResposta,codAvaliacao) VALUES ('" + resposta + "'," + codPergunta + ",1,"+ codAvaliacao + ") ";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean cadastroPerguntaCorreta(String resposta) {

            try
            {
                sql = "INSERT INTO tbResposta (resposta,codPergunta,codTipoResposta) VALUES ('"+resposta+"',"+codigoPergunta()+",1) ";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();
               
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean cadastroPerguntaErrada(String resposta)
        {

            try
            {
                sql = "INSERT INTO tbResposta (resposta,codPergunta,codTipoResposta) VALUES ('"+resposta+"',"+codigoPergunta()+",2)";
                SqlCommand comando = new SqlCommand(sql, conn);
                comando.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean inserirAvaliacao(Avaliacao avaliacao)
        {
            try
            {
                sql = "INSERT INTo tbAvaliacao(descricaoAvaliacao, codDisciplina) VALUES('"+avaliacao.Descricao+"', "+avaliacao.d.Cod+")";
                SqlCommand comando = new SqlCommand(sql,conn);
                comando.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean inserir(Pergunta pergunta, int codAvaliacao)
        {
            try
            {
                   sql = "INSERT INTO tbPergunta (descPergunta,codDisciplina,codAvaliacao,codStatus) VALUES ('"+pergunta.Descricao+"',"+pergunta.Disciplina+","+codAvaliacao+",3)";
                SqlCommand comando = new SqlCommand(sql,conn);
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
