using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Index
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            foreach (Control controle in this.Controls)
            {
                // Verifica se o controle é do tipo MdiClient (o espaço de fundo do MDI)
                if (controle is MdiClient)
                {
                    controle.BackColor = Color.FromArgb(212, 190, 228);// Define a cor de fundo do MDI
                }
            }
        }

        private void funcionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFuncionario formFuncionario = new FormFuncionario();
            formFuncionario.MdiParent = this;//ou FormPrincipal
            formFuncionario.Show();
        }


        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formLogin = new LoginAdmin();
            formLogin.Show();
        }

        private void produtoVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProduto formProduto = new FormProduto();
            formProduto.MdiParent = this;//ou FormPrincipal
            formProduto.Show();
        }
    }
}
