namespace Genesis.Escola.MVC.Functions
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
		
		//public OdbcConnection getConexao(string LocalArquivo)
		//{
  //          string conn = "Driver={Microsoft Access Driver (*.mdb, *.accdb)}; DBQ = " + @LocalArquivo;
		//	return new OdbcConnection(conn);
		//}
	}
}
