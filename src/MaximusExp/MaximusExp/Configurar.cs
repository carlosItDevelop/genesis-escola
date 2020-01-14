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
    public partial class Configurar : Form
    {
        IniFile meuIniFile = new IniFile();
        public Configurar()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            meuIniFile.IniWriteString("Host", Criptografia.Encrypt(txtHost.Text));
            meuIniFile.IniWriteString("Porta", Criptografia.Encrypt(txtPorta.Value.ToString()));
            meuIniFile.IniWriteString("Usuario", Criptografia.Encrypt(txtUsuario.Text));
            meuIniFile.IniWriteString("Senha", Criptografia.Encrypt(txtSenha.Text));
            meuIniFile.IniWriteString("NomeBanco", Criptografia.Encrypt(txtNomeBanco.Text));
            MessageBox.Show("Salvo com Sucesso!!", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Configurar_Load(object sender, EventArgs e)
        {
            txtHost.Text = Criptografia.Decrypt(meuIniFile.IniReadString("Host", ""));
            txtPorta.Text = Criptografia.Decrypt(meuIniFile.IniReadString("Porta", ""));
            txtUsuario.Text = Criptografia.Decrypt(meuIniFile.IniReadString("Usuario", ""));
            txtSenha.Text = Criptografia.Decrypt(meuIniFile.IniReadString("Senha", ""));
            txtNomeBanco.Text = Criptografia.Decrypt(meuIniFile.IniReadString("NomeBanco", ""));

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
