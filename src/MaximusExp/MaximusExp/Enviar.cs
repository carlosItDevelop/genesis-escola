using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaximusExp
{
    public partial class Enviar : Form
    {
        string StrBanco;
        DateTime tInicio, tFinal;
        public Enviar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //define as propriedades do controle OpenFileDialog
            this.ofd1.Multiselect = false;
            this.ofd1.Title = "Selecionar Arquivo Acadesc";
            //filtra para exibir somente arquivos de imagens
            ofd1.Filter = "Access (*.MDB;*.MDBX" + "Todos os Arquivos (*.*)|*.*";
            ofd1.CheckFileExists = true;
            ofd1.CheckPathExists = true;
            ofd1.FilterIndex = 2;
            ofd1.RestoreDirectory = true;
            ofd1.ReadOnlyChecked = true;
            ofd1.ShowReadOnly = true;

            DialogResult ddr = this.ofd1.ShowDialog();

            if (ddr == System.Windows.Forms.DialogResult.OK)
            {

                txtArquivo.Text = ofd1.FileName;
            }
        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            btnSair.Enabled = false;
            button1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            IniFile meuIniFile = new IniFile();
            string myConexao;
            tInicio = DateTime.Now;
            myConexao = "server = " + Criptografia.Decrypt(meuIniFile.IniReadString("Host", ""));
            myConexao += ";Port= " + Criptografia.Decrypt(meuIniFile.IniReadString("Porta", ""));
            myConexao += ";User Id=" + Criptografia.Decrypt(meuIniFile.IniReadString("Usuario", ""));
            myConexao += ";password=" + Criptografia.Decrypt(meuIniFile.IniReadString("Senha", ""));
            myConexao += ";database=" + Criptografia.Decrypt(meuIniFile.IniReadString("NomeBanco", ""));
            int contador = 0;
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
            try
            {

                #region Limpar Tabelas 
                MySqlConnection conexao = new MySqlConnection(myConexao);
                MySqlCommand comando = new MySqlCommand("TRUNCATE TABLE TurmaAcadesc", conexao);
                MySqlCommand comando5 = new MySqlCommand("TRUNCATE TABLE CursoAcadesc", conexao);
                MySqlCommand comando6 = new MySqlCommand("TRUNCATE TABLE Disciplinas", conexao);
                MySqlCommand comando10 = new MySqlCommand("TRUNCATE TABLE Notas", conexao);

                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    comando5.ExecuteNonQuery();
                    comando6.ExecuteNonQuery();
                    comando10.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não é possível Acessar o Servidor de Internet " +
                                  " Favor Verificar as Configurações.\n\nErro reportado : " + ex.Message);
                }
                finally
                {
                    conexao.Close();
                }
                #endregion

                //using (MySqlConnection connection = new MySqlConnection(myConexao))
                using (OleDbConnection conexaoAccess = daoAccess.getInstancia().getConexao(ofd1.FileName))
                {
                    DataSet ds = new DataSet();

                    #region Tabela de Alunos
                    // cria o adapter e preenche o dataset
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT Matricula ,Nome,Senha,Curso,Serie,Turma,Turno,Numero,Dtnascimento,TextoFinalBoletim,RespNome,RespEndereco,RespCidade,RespEstado,RespCep,RespEmail,Simbolo FROM Alunos", conexaoAccess);

                    adapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!sendingWorker.CancellationPending)
                        {
                            contador++;
                            StrBanco = "Gravando Tabela de Alunos ";
                            GravarAluno(myConexao, dr["Matricula"].ToString(), dr["Nome"].ToString(), dr["Senha"].ToString(), dr["Curso"].ToString(), dr["Serie"].ToString(), dr["Turma"].ToString(), dr["Turno"].ToString(), dr["Numero"].ToString(), dr["Dtnascimento"].ToString(), dr["TextoFinalBoletim"].ToString(), dr["RespNome"].ToString(), dr["RespEndereco"].ToString(), dr["RespCidade"].ToString(), dr["RespEstado"].ToString(), dr["RespCep"].ToString(), dr["RespEmail"].ToString(), dr["Simbolo"].ToString());
                            sendingWorker.ReportProgress(contador);
                            e.Result = contador.ToString();
                        }
                    }
                    #endregion

                    #region Tabela Cursos
                    adapter = new OleDbDataAdapter("SELECT Codigo,Nome,NumSeries FROM Cursos", conexaoAccess);
                    ds = new DataSet();
                    dt = new DataTable();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                    contador = 0;
                    sendingWorker.ReportProgress(contador);
                    e.Result = contador.ToString();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!sendingWorker.CancellationPending)
                        {
                            contador++;
                            StrBanco = "Gravando Tabela de Cursos ";
                            GravarCursos(myConexao, dr["Codigo"].ToString(), dr["Nome"].ToString(), dr["NumSeries"].ToString());
                            sendingWorker.ReportProgress(contador);
                            e.Result = contador.ToString();
                        }
                    }

                    #endregion

                    #region Tabela Turma Acadesc
                    adapter = new OleDbDataAdapter("select distinct serie,turma,turno,curso from alunos", conexaoAccess);
                    ds = new DataSet();
                    dt = new DataTable();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                    contador = 0;
                    sendingWorker.ReportProgress(contador);
                    e.Result = contador.ToString();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!sendingWorker.CancellationPending)
                        {
                            contador++;
                            StrBanco = "Gravando Tabela de Turmas ";
                            GravarTurmaAcadesc(myConexao, dr["Serie"].ToString(), dr["Turma"].ToString(), dr["Turno"].ToString(),dr["Curso"].ToString());
                            sendingWorker.ReportProgress(contador);
                            e.Result = contador.ToString();
                        }
                    }

                    #endregion

                    #region Tabela Disciplinas
                    adapter = new OleDbDataAdapter("SELECT Codigo,Nome,Sigla,CargaHoraria,Professor,Cargo,Curso,Serie,Turma FROM Disciplinas", conexaoAccess);
                    ds = new DataSet();
                    dt = new DataTable();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                    contador = 0;
                    sendingWorker.ReportProgress(contador);
                    e.Result = contador.ToString();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!sendingWorker.CancellationPending)
                        {
                            contador++;
                            StrBanco = "Gravando Tabela de Disciplinas ";
                            GravarDisciplina(myConexao, dr["Codigo"].ToString(), dr["Nome"].ToString(), dr["Sigla"].ToString(), dr["CargaHoraria"].ToString(), dr["Professor"].ToString(), dr["Cargo"].ToString(), dr["Curso"].ToString(), dr["Serie"].ToString(), dr["Turma"].ToString());
                            sendingWorker.ReportProgress(contador);
                            e.Result = contador.ToString();
                        }
                    }

                    #endregion

                    #region Tabela Notas
                    adapter = new OleDbDataAdapter("SELECT Matricula,Disciplina,Nota1,Nota2,Nota3,Nota4,Nt1,Nt2,Nt3,Nt4,NotaRec1,NotaRec2,NotaRec3,NotaRec4,NotaRecSem1,NotaRecSem2,MediaSem1,MediaSem2,Cor1,Cor2,Cor3,Cor4,CorRec,MediaBim,NotaRec,MediaFinal,Situacao,CorSituacao,Falta1,Falta2,Falta3,Falta4,PerFalta,PerFrequencia,TotalFaltas,Observacoes FROM Notas", conexaoAccess);
                    ds = new DataSet();
                    dt = new DataTable();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                    contador = 0;
                    sendingWorker.ReportProgress(contador);
                    e.Result = contador.ToString();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!sendingWorker.CancellationPending)
                        {
                            contador++;
                            StrBanco = "Gravando Tabela de Notas ";
                            GravarNotas(myConexao, dr["Matricula"].ToString(), dr["Disciplina"].ToString(), dr["Nota1"].ToString(), dr["Nota2"].ToString(), dr["Nota3"].ToString(), dr["Nota4"].ToString(), dr["Nt1"].ToString(), dr["Nt2"].ToString(), dr["Nt3"].ToString(), dr["Nt4"].ToString(), dr["NotaRec1"].ToString(), dr["NotaRec2"].ToString(), dr["NotaRec3"].ToString(), dr["NotaRec4"].ToString(), dr["NotaRecSem1"].ToString(), dr["NotaRecSem2"].ToString(), dr["MediaSem1"].ToString(), dr["MediaSem2"].ToString(), dr["Cor1"].ToString(), dr["Cor2"].ToString(), dr["Cor3"].ToString(), dr["Cor4"].ToString(), dr["CorRec"].ToString(), dr["MediaBim"].ToString(), dr["NotaRec"].ToString(), dr["MediaFinal"].ToString(), dr["Situacao"].ToString(), dr["CorSituacao"].ToString(), dr["Falta1"].ToString(), dr["Falta2"].ToString(), dr["Falta3"].ToString(), dr["Falta4"].ToString(), dr["PerFalta"].ToString(), dr["PerFrequencia"].ToString(), dr["TotalFaltas"].ToString(), dr["Observacoes"].ToString());
                            sendingWorker.ReportProgress(contador);
                            e.Result = contador.ToString();
                        }
                    }

                    #endregion


                }
            }
            catch (SecurityException ex)
            {
                // O usuário  não possui permissão para ler arquivos
                MessageBox.Show("Erro de segurança Contate o administrador de segurança da rede.\n\n" +
                                            "Mensagem : " + ex.Message + "\n\n" +
                                            "Detalhes (enviar ao suporte):\n\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                // Não pode carregar a imagem (problemas de permissão)
                MessageBox.Show("Não é possível Carregar o arquivo " + ofd1.FileName.Substring(ofd1.FileName.LastIndexOf('\\'))
                                           + ". Você pode não ter permissão para ler o arquivo , ou " +
                                           " ele pode estar corrompido.\n\nErro reportado : " + ex.Message);
            }
        }

        private void GravarTurmaAcadesc(string myConexao, string v1, string v2, string v3,string v4)
        {
            var Id = Guid.NewGuid();
            MySqlConnection conexao = new MySqlConnection(myConexao);
            string strComando = "Select * from TurmaAcadesc where Serie = '" + v1.Trim() + "' and Turma = '" + v2.Trim() + "' and Turno = '" + v3.Trim() + "' and Ciclo = '" + v4.Trim() + "'";
            MySqlCommand comando = new MySqlCommand(strComando, conexao);
            conexao.Open();
            MySqlDataReader reader = comando.ExecuteReader();
            string Turno;
            if (v3 == "M") Turno = "Manhã";
            else if (v3 == "T") Turno = "Tarde";
            else Turno = "Noite";
            var nome = v1.Trim() + " - " + v2.Trim() + " " + Turno.Trim();
            if (!reader.HasRows)
            {
                strComando = "INSERT INTO TurmaAcadesc (Id,Serie,Turma,Turno,Nome,Ciclo) VALUES ";
                strComando += "(@id,@serie,@turma,@turno,@nome,@curso)";
            }
            conexao.Close();
            comando = new MySqlCommand(strComando, conexao);
            comando.Parameters.AddWithValue("@id", Id);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@serie", v1);
            comando.Parameters.AddWithValue("@turma", v2);
            comando.Parameters.AddWithValue("@turno", v3);
            comando.Parameters.AddWithValue("@curso", v4);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não é possível Acessar o Servidor de Internet " +
                                " Favor Verificar as Configurações.\n\nErro reportado : " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label2.Text = string.Format("{0} {1}...", StrBanco, e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnOk.Enabled = true;
            btnSair.Enabled = true;
            button1.Enabled = true;
            TimeSpan tDif;
            tFinal = DateTime.Now;
            tDif = tFinal.Subtract(tInicio);
            MessageBox.Show("Terminado em " + tDif.TotalSeconds.ToString("0") + " segundos", "Mensagem do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Gravar Aluno 
        public void GravarAluno(string myConexao, string Matricula, string Nome, string Senha, string Curso, string Serie, string Turma, string Turno, string Numero, string Dtnascimento, string TextoFinalBoletim, string RespNome, string RespEndereco, string RespCidade, string RespEstado, string RespCep, string RespEmail, string Simbolo)
        {
            var Id = Guid.NewGuid();
            MySqlConnection conexao = new MySqlConnection(myConexao);
            string strComando = "Select * from Alunos where Matricula =" + Matricula;
            MySqlCommand comando = new MySqlCommand(strComando, conexao);
            conexao.Open();
            MySqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                strComando = "UPDATE Alunos Set Matricula = @matricula, Nome=@nome,Senha=@senha,Curso=@curso,Serie=@serie,Turma=@turma,Turno=@turno,Numero=@numero,Dtnascimento=@dtnascimento,TextoFinalBoletim=@textoFinalBoletim,RespNome=@respNome,RespEndereco=@respEndereco,RespCidade=@respCidade,RespEstado=@respEstado,RespCep=@respCep,RespEmail=@respEmail,Simbolo=@simbolo where Matricula = @matricula";
            }
            else
            {
                strComando = "INSERT INTO Alunos (Id,Matricula,Nome,Senha,Curso,Serie,Turma,Turno,Numero,Dtnascimento,TextoFinalBoletim,RespNome,RespEndereco,RespCidade,RespEstado,RespCep,RespEmail,Simbolo ) VALUES ";
                strComando += "(@id,@matricula,@nome,@senha,@curso,@serie,@turma,@turno,@numero,@dtnascimento,@textoFinalBoletim,@respNome,@respEndereco,@respCidade,@respEstado,@respCep,@respEmail,@simbolo)";
            }
            conexao.Close();
            comando = new MySqlCommand(strComando, conexao);
            comando.Parameters.AddWithValue("@id", Id);
            comando.Parameters.AddWithValue("@matricula", Matricula);
            comando.Parameters.AddWithValue("@nome", Nome);
            comando.Parameters.AddWithValue("@senha", Senha);
            comando.Parameters.AddWithValue("@curso", Curso);
            comando.Parameters.AddWithValue("@serie", Serie);
            comando.Parameters.AddWithValue("@turma", Turma);
            comando.Parameters.AddWithValue("@turno", Turno);
            comando.Parameters.AddWithValue("@numero", Numero);
            DateTime Datanasc;
            DateTime.TryParse(Dtnascimento, out Datanasc);
            comando.Parameters.AddWithValue("@dtnascimento", Datanasc);
            comando.Parameters.AddWithValue("@textoFinalBoletim", TextoFinalBoletim);
            comando.Parameters.AddWithValue("@respNome", RespNome);
            comando.Parameters.AddWithValue("@respEndereco", RespEndereco);
            comando.Parameters.AddWithValue("@respCidade", RespCidade);
            comando.Parameters.AddWithValue("@respEstado", RespEstado);
            comando.Parameters.AddWithValue("@respCep", RespCep);
            comando.Parameters.AddWithValue("@respEmail", RespEmail);
            comando.Parameters.AddWithValue("@simbolo", Simbolo);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não é possível Acessar o Servidor de Internet " +
                                " Favor Verificar as Configurações.\n\nErro reportado : " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        #endregion

        #region Gravar Cursos 
        private void GravarCursos(string myConexao, string v1, string v2, string v3)
        {
            var Id = Guid.NewGuid();
            MySqlConnection conexao = new MySqlConnection(myConexao);
            string strComando = "INSERT INTO CursoAcadesc (Id,Codigo,Nome,NumSeries ) VALUES "; // versao nova cursoAcadesc
            strComando += "(@id,@v1,@v2,@v3)";
            MySqlCommand comando = new MySqlCommand(strComando, conexao);
            comando.Parameters.AddWithValue("@id", Id);
            comando.Parameters.AddWithValue("@v1", v1);
            comando.Parameters.AddWithValue("@v2", v2);
            comando.Parameters.AddWithValue("@v3", v3);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não é possível Acessar o Servidor de Internet " +
                                " Favor Verificar as Configurações.\n\nErro reportado : " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        #endregion

        #region Gravar Disciplinas 
        private void GravarDisciplina(string myConexao, string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9)
        {
            var Id = Guid.NewGuid();
            MySqlConnection conexao = new MySqlConnection(myConexao);
            string strComando = "INSERT INTO Disciplinas (Id,Codigo,Nome,Sigla,CargaHoraria,Professor,Cargo,Curso,Serie,Turma ) VALUES ";
            strComando += "(@id,@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9)";
            MySqlCommand comando = new MySqlCommand(strComando, conexao);
            comando.Parameters.AddWithValue("@id", Id);
            comando.Parameters.AddWithValue("@v1", v1);
            comando.Parameters.AddWithValue("@v2", v2);
            comando.Parameters.AddWithValue("@v3", v3);
            comando.Parameters.AddWithValue("@v4", v4);
            comando.Parameters.AddWithValue("@v5", v5);
            comando.Parameters.AddWithValue("@v6", v6);
            comando.Parameters.AddWithValue("@v7", v7);
            comando.Parameters.AddWithValue("@v8", v8);
            comando.Parameters.AddWithValue("@v9", v9);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não é possível Acessar o Servidor de Internet " +
                                " Favor Verificar as Configurações.\n\nErro reportado : " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        #endregion

        #region Gravar Notas 
        private void GravarNotas(string myConexao, string v1, string v2, string v3, string v4, string v5, string v6, string v7, string v8, string v9, string v10, string v11, string v12, string v13, string v14, string v15, string v16, string v17, string v18, string v19, string v20, string v21, string v22, string v23, string v24, string v25, string v26, string v27, string v28, string v29, string v30, string v31, string v32, string v33, string v34, string v35, string v36)
        {
            var Id = Guid.NewGuid();
            MySqlConnection conexao = new MySqlConnection(myConexao);
            string strComando = "INSERT INTO Notas (Id,Matricula,Disciplina,Nota1,Nota2,Nota3,Nota4,Nt1,Nt2,Nt3,Nt4,NotaRec1,NotaRec2,NotaRec3,NotaRec4,NotaRecSem1,NotaRecSem2,MediaSem1,MediaSem2,Cor1,Cor2,Cor3,Cor4,CorRec,MediaBim,NotaRec,MediaFinal,Situacao,CorSituacao,Falta1,Falta2,Falta3,Falta4,PerFalta,PerFrequencia,TotalFaltas,Observacoes) VALUES";
            strComando += "(@id,@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9,@v10,@v11,@v12,@v13,@v14,@v15,@v16,@v17,@v18,@v19,@v20,@v21,@v22,@v23,@v24,@v25,@v26,@v27,@v28,@v29,@v30,@v31,@v32,@v33,@v34,@v35,@v36)";
            MySqlCommand comando = new MySqlCommand(strComando, conexao);
            comando.Parameters.AddWithValue("@id", Id);
            comando.Parameters.AddWithValue("@v1", v1);
            comando.Parameters.AddWithValue("@v2", v2);
            comando.Parameters.AddWithValue("@v3", v3);
            comando.Parameters.AddWithValue("@v4", v4);
            comando.Parameters.AddWithValue("@v5", v5);
            comando.Parameters.AddWithValue("@v6", v6);
            comando.Parameters.AddWithValue("@v7", v7);
            comando.Parameters.AddWithValue("@v8", v8);
            comando.Parameters.AddWithValue("@v9", v9);
            comando.Parameters.AddWithValue("@v10", v10);
            comando.Parameters.AddWithValue("@v11", v11);
            comando.Parameters.AddWithValue("@v12", v12);
            comando.Parameters.AddWithValue("@v13", v13);
            comando.Parameters.AddWithValue("@v14", v14);
            comando.Parameters.AddWithValue("@v15", v15);
            comando.Parameters.AddWithValue("@v16", v16);
            comando.Parameters.AddWithValue("@v17", v17);
            comando.Parameters.AddWithValue("@v18", v18);
            bool cor;
            bool.TryParse(v19, out cor);
            comando.Parameters.AddWithValue("@v19", cor);
            bool.TryParse(v20, out cor);
            comando.Parameters.AddWithValue("@v20", cor);
            bool.TryParse(v21, out cor);
            comando.Parameters.AddWithValue("@v21", cor);
            bool.TryParse(v22, out cor);
            comando.Parameters.AddWithValue("@v22", cor);
            bool.TryParse(v23, out cor);
            comando.Parameters.AddWithValue("@v23", cor);
            comando.Parameters.AddWithValue("@v24", v24);
            comando.Parameters.AddWithValue("@v25", v25);
            comando.Parameters.AddWithValue("@v26", v26);
            comando.Parameters.AddWithValue("@v27", v27);
            comando.Parameters.AddWithValue("@v28", v28);
            comando.Parameters.AddWithValue("@v29", v29);
            comando.Parameters.AddWithValue("@v30", v30);
            comando.Parameters.AddWithValue("@v31", v31);
            comando.Parameters.AddWithValue("@v32", v32);
            double val;
            double.TryParse(v33, out val);
            comando.Parameters.AddWithValue("@v33", val);
            double.TryParse(v34, out val);
            comando.Parameters.AddWithValue("@v34", val);
            comando.Parameters.AddWithValue("@v35", v35);
            comando.Parameters.AddWithValue("@v36", v36);
            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não é possível Acessar o Servidor de Internet " +
                                " Favor Verificar as Configurações.\n\nErro reportado : " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        #endregion


    }
}
