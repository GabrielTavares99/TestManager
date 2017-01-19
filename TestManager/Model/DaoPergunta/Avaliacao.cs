namespace TestManager.Model.DaoPergunta
{
    public class Avaliacao
    {

        private int cod;
        private string descricao;

        public Disciplina d = new Disciplina();

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

        public string Descricao
        {
            get
            {
                return descricao;
            }

            set
            {
                descricao = value;
            }
        }
    }
}