using Genesis.Escola.MVC.Functions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    [Area("Administrar")]
    public class AcadescController : Controller
    {
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos
        public AcadescController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }
        //Ret

        public IActionResult Index()
        {
            if (TempData != null) ViewData["Erro"] = TempData["Erro"];
            return View();
        }

        [Area("Administrar")]
        public async Task<IActionResult> EnviarArquivo(IFormFile arquivo)
        {

            //if (arquivo == null || arquivo.Length == 0)
            //{
            //    TempData["Erro"] = "Erro: Nenhum arquivo selecionado";
            //    return RedirectToAction("Index", "Acadesc");
            //}
            //long tamanhoArquivos = arquivo.Length;

            //string caminhoDestinoArquivoOriginal = $"{ _appEnvironment.WebRootPath}{"\\Arquivos\\Recebidos\\"}{arquivo.FileName}";

            //using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
            //{
            //    await arquivo.CopyToAsync(stream);
            //}

            //var connection = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" + @caminhoDestinoArquivoOriginal);
            //var queryText = "SELECT * FROM Alunos";
            //connection.Open();
            //DataSet ds1 = new DataSet();
            //OdbcDataAdapter adapter1 = new OdbcDataAdapter(queryText,connection);
            //adapter1.Fill(ds1);
            ////Use dapper to query with parameter
            ////It's also a good idea if you use a string as a parameter that you use DbString instead of sending the variable directly
            ////You will also need to specify the Length exactly as the length of the column in the Microsoft Access table
            ////   var data = connection.Query<Model>(queryText, new { Name = new DbString { Value = name, Length = 10, IsAnsi = true } });


            //using (OdbcConnection conexaoAccess = daoAccess.getInstancia().getConexao(@caminhoDestinoArquivoOriginal))
            //{
            //    DataSet ds = new DataSet();

            //    #region Tabela de Alunos
            //    // cria o adapter e preenche o dataset
            //  //  OdbcDataAdapter
            //    OdbcDataAdapter adapter = new OdbcDataAdapter("SELECT Matricula ,Nome,Senha,Curso,Serie,Turma,Turno,Numero,Dtnascimento,TextoFinalBoletim,RespNome,RespEndereco,RespCidade,RespEstado,RespCep,RespEmail,Simbolo FROM Alunos", conexaoAccess);

            //    adapter.Fill(ds);
            //    DataTable dt = ds.Tables[0];
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        //if (!sendingWorker.CancellationPending)
            //        //{
            //        //    contador++;
            //        //    StrBanco = "Gravando Tabela de Alunos ";
            //        //    GravarAluno(myConexao, dr["Matricula"].ToString(), dr["Nome"].ToString(), dr["Senha"].ToString(), dr["Curso"].ToString(), dr["Serie"].ToString(), dr["Turma"].ToString(), dr["Turno"].ToString(), dr["Numero"].ToString(), dr["Dtnascimento"].ToString(), dr["TextoFinalBoletim"].ToString(), dr["RespNome"].ToString(), dr["RespEndereco"].ToString(), dr["RespCidade"].ToString(), dr["RespEstado"].ToString(), dr["RespCep"].ToString(), dr["RespEmail"].ToString(), dr["Simbolo"].ToString());
            //        //    sendingWorker.ReportProgress(contador);
            //        //    e.Result = contador.ToString();
            //        //}
            //    }
            //    #endregion
            //}

            //ViewData["Resultado"] = $"{"Arquivo enviado ao servidor, "} {"com tamanho total de :"} {tamanhoArquivos} {"bytes"}";

            return View(ViewData);
        }

    }
}