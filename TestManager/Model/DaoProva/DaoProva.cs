using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestManager.Model.DaoPergunta;

namespace TestManager.Model.DaoProva
{
    class DaoProva
    {

        public Resposta[] preencherAlternativas(int codPergunta)
        {

            Resposta[] alternativa = new Resposta[5];

            String sql = "SELECT resposta, codTipoResposta, codPergunta,codResposta  from tbResposta WHERE codPergunta = " + codPergunta + " order by NEWID()";

            SqlConnection conn = new Conexao().abrirConexao();
            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            SqlDataReader dr = comando.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                Resposta r = new Resposta();
                r.DescReposta = Convert.ToString(dr["resposta"]);
                r.Cod = Convert.ToInt16(dr["codResposta"]);
                r.Tipo = Convert.ToInt16(dr["codTipoResposta"]);
                
                //System.Windows.Forms.MessageBox.Show(""+r.DescReposta);
                alternativa[i] = r;
                i++;
            }
            return alternativa;
        }

        public List<Disciplina> preencherDisciplina()
        {
            SqlConnection conn = new Conexao().abrirConexao();

            SqlCommand comando = new SqlCommand("SELECT codDisciplina,descDisciplina,codStatus FROM tbDisciplina", conn);
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

            return disciplina;

        }

        public Boolean cadastrarResposta(Aluno aluno,Resposta resposta)
        {
            SqlConnection conn = new Conexao().abrirConexao();

            String sql = "INSERT INTO tbProva (codUsuario,data,codResposta,status) VALUES ("+aluno.Cod+",GETDATE(),"+resposta.Cod+",1)";

            SqlCommand comando = new SqlCommand(sql, conn);

            try
            {
                
                comando.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return false;
            }

        }

        public List<Pergunta> preencherPergunta(Avaliacao avaliacao)
        {
            SqlConnection conn = new Conexao().abrirConexao();

            SqlCommand comando = new SqlCommand("SELECT descPergunta,codPergunta FROM tbPergunta WHERE codDisciplina =" + avaliacao.d.Cod + " AND codAvaliacao = "+ avaliacao.Cod+"", conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            List<Pergunta> pergunta = new List<Pergunta>();

            SqlDataReader dr = comando.ExecuteReader();
            while (dr.Read())
            {
                Pergunta p = new Pergunta()
                {
                    Descricao = Convert.ToString(dr["descPergunta"]),
                    Cod = Convert.ToInt16(dr["codPergunta"])
                };
                pergunta.Add(p);

            }
            return pergunta;
        }
     
        public List<Pergunta> preencherCheck(int consulta)
        {
            List<Pergunta> pergunta = new List<Pergunta>();

            String sql = "SELECT * FROM tbPergunta WHERE codDisciplina = "+consulta;

            SqlConnection conn = new Conexao().abrirConexao();

            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            SqlDataReader dr = comando.ExecuteReader();

            while (dr.Read())
            {
                Pergunta p = new Pergunta()
                {
                    Descricao = Convert.ToString(dr["descPergunta"]),
                    Cod = Convert.ToInt16(dr["codPergunta"]),
                    Disciplina = Convert.ToInt16(dr["codDisciplina"])

                };
                pergunta.Add(p);
            }


            return pergunta;
        }

    }
}
