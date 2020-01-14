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
    public partial class Principal : Form
    {
        public bool passwordFound = false;
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void configurarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Senha formulario = new Senha();
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void enviarParaOSIteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enviar formulario = new Enviar();
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Enviar formulario = new Enviar();
            formulario.MdiParent = this;
            formulario.Show();
        }
    }
}
