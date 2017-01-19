using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestManager.View;

namespace TestManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ViewProva());
            Aluno aluno = new Aluno();
            aluno.Cod = 61;
            aluno.Login = "aluno";
           
            Application.Run(new telaInicial());

        }
    }
}
