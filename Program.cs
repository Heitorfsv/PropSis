using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PrototipoSistema
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cria e exibe o login como um diálogo modal
            using (login login = new login())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // Se o login for bem-sucedido, inicia o MDI
                    Application.Run(new MDI_tela());
                }
            }
        }
    }
}
