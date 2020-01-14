using System;
using System.Data.OleDb;
using System.Configuration;

namespace MaximusExp
{
	/// <summary>
	/// Acesso a um banco de dados Microsoft Access
	/// usando o padrão Singleton
	/// </summary>
	public class daoAccess
	{
		private static readonly daoAccess instanciaAccess = new daoAccess();
		
		private daoAccess(){	}
		
		public static daoAccess getInstancia()
		{
			return instanciaAccess;
		}
		
		public OleDbConnection getConexao(string LocalArquivo)
		{
            string conn = "Provider = Microsoft.JET.OLEDB.4.0; data source = " + @LocalArquivo;
                //ConfigurationManager.ConnectionStrings["AccessConnectionString"].ToString();
			return new OleDbConnection(conn);
		}
	}
}
