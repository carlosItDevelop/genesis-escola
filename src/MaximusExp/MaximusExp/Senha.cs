using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaximusExp
{
    public partial class Senha : Form
    {
        
        public Senha()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == "102030")
            {
                this.Hide();
                Configurar f2 = new Configurar();
               // teste.passwordFound = true;
                //Form2 f2 = new Form2();
                f2.MdiParent = Principal.ActiveForm;
                f2.Show();

                // passwordFound = true;
            }
            else MessageBox.Show("Senha digitada esta errada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
