using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TestManager.Model.Resultado
{
    class DaoResultado
    {
        String sql;
        SqlConnection conn = new Conexao().abrirConexao();

        List<Aluno> alunos;

        public DataTable preencherGrid(Aluno aluno)
        {
            //sql = "SELECT tbDisciplina.descDisciplina,tbAvaliacao.descricaoAvaliacao, COUNT(tbProva.codResposta) AS 'Quantidade de respostas ' FROM tbAvaliacao INNER JOIN tbPergunta ON tbAvaliacao.codAvaliacao = tbPergunta.codAvaliacao INNER JOIN tbResposta ON tbPergunta.codPergunta = tbResposta.codPergunta INNER JOIN tbProva ON tbProva.codResposta = tbResposta.codResposta INNER JOIN tbTipoResposta ON tbResposta.codTipoResposta = tbTipoResposta.codTipoResposta INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina WHERE tbProva.codUsuario = " +aluno.Cod+ "  GROUP BY tbAvaliacao.descricaoAvaliacao, tbDisciplina.descDisciplina";

            //sql = "SELECT tbDisciplina.descDisciplina, tbAvaliacao.descricaoAvaliacao, COUNT(tbProva.codResposta) AS 'Quantidade de respostas certas' FROM tbAvaliacao INNER JOIN tbPergunta ON tbAvaliacao.codAvaliacao = tbPergunta.codAvaliacao INNER JOIN tbResposta ON tbPergunta.codPergunta = tbResposta.codPergunta INNER JOIN tbProva ON tbProva.codResposta = tbResposta.codResposta INNER JOIN tbTipoResposta ON tbResposta.codTipoResposta = tbTipoResposta.codTipoResposta INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina WHERE tbTipoResposta.codTipoResposta = 1 AND tbProva.codUsuario = " + aluno.Cod + " GROUP BY tbAvaliacao.descricaoAvaliacao, tbDisciplina.descDisciplina";

            //sql = "SELECT tbDisciplina.descDisciplina, tbAvaliacao.descricaoAvaliacao, COUNT(tbPergunta.codPergunta) AS 'Quantidade de perguntas' FROM tbPergunta INNER JOIN tbAvaliacao ON tbPergunta.codAvaliacao = tbAvaliacao.codAvaliacao INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina WHERE tbPergunta.codDisciplina = 1 GROUP BY tbAvaliacao.descricaoAvaliacao,tbDisciplina.descDisciplina UNION SELECT tbDisciplina.descDisciplina, tbAvaliacao.descricaoAvaliacao, COUNT(tbProva.codResposta) AS 'Quantidade de respostas certas' FROM tbAvaliacao INNER JOIN tbPergunta ON tbAvaliacao.codAvaliacao = tbPergunta.codAvaliacao INNER JOIN tbResposta ON tbPergunta.codPergunta = tbResposta.codPergunta INNER JOIN tbProva ON tbProva.codResposta = tbResposta.codResposta INNER JOIN tbTipoResposta ON tbResposta.codTipoResposta = tbTipoResposta.codTipoResposta INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina WHERE tbTipoResposta.codTipoResposta = 1 AND tbProva.codUsuario = "+aluno.Cod+" GROUP BY tbAvaliacao.descricaoAvaliacao, tbDisciplina.descDisciplina ";

            sql = "SELECT tbDisciplina.descDisciplina, tbAvaliacao.descricaoAvaliacao, COUNT(tbPergunta.codPergunta) AS 'Quantidade de perguntas' FROM tbPergunta INNER JOIN tbAvaliacao ON tbPergunta.codAvaliacao = tbAvaliacao.codAvaliacao INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina GROUP BY tbAvaliacao.descricaoAvaliacao,tbDisciplina.descDisciplina";

            SqlCommand comando = new SqlCommand(sql, conn);
         
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);

            return dataTable;

        }

        public DataTable preencherCertas(Aluno aluno)
        {

            sql = "SELECT tbDisciplina.descDisciplina, tbAvaliacao.descricaoAvaliacao, COUNT(tbProva.codResposta) AS 'Quantidade de respostas certas' FROM tbAvaliacao INNER JOIN tbPergunta ON tbAvaliacao.codAvaliacao = tbPergunta.codAvaliacao INNER JOIN tbResposta ON tbPergunta.codPergunta = tbResposta.codPergunta INNER JOIN tbProva ON tbProva.codResposta = tbResposta.codResposta INNER JOIN tbTipoResposta ON tbResposta.codTipoResposta = tbTipoResposta.codTipoResposta INNER JOIN tbDisciplina ON tbPergunta.codDisciplina = tbDisciplina.codDisciplina WHERE tbTipoResposta.codTipoResposta = 1 AND tbProva.codUsuario = " + aluno.Cod + " GROUP BY tbAvaliacao.descricaoAvaliacao, tbDisciplina.descDisciplina";


            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable dataTable = new DataTable();
            adaptador.Fill(dataTable);

            return dataTable;


        }

        public List<Aluno> preencherAluno()
        {
            alunos = new List<Aluno>();

            sql = "SELECT * FROM tbUsuario WHERE codTipoUsuario = 3";

            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            SqlDataReader dr = comando.ExecuteReader();

            Aluno a;

            while (dr.Read())
            {
                a = new Aluno {
                    Cod = Convert.ToInt16(dr["codUsuario"]),
                    Nome = Convert.ToString(dr["nomeUsuario"])
            };
                alunos.Add(a);
                
            }

            dr.Close();
            return alunos;
        }

        public List<Aluno> preencherAluno(String consulta)
        {
            alunos = new List<Aluno>();

            sql = "SELECT * FROM tbUsuario WHERE codTipoUsuario = 3 AND nomeUsuario LIKE '"+consulta+"%' ";

            SqlCommand comando = new SqlCommand(sql, conn);

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            SqlDataReader dr = comando.ExecuteReader();

            Aluno a;

            while (dr.Read())
            {
                a = new Aluno
                {
                    Cod = Convert.ToInt16(dr["codUsuario"]),
                    Nome = Convert.ToString(dr["nomeUsuario"])
                };
                alunos.Add(a);

            }

            dr.Close();
            return alunos;
        }


    }
}
