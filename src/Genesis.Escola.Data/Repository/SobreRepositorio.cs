using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class SobreRepositorio : RepositorioGenerico<Sobre>, ISobreRepositorio
    {
        public SobreRepositorio(Contexto contexto) : base(contexto) { }
        public async Task<Sobre> PegarDadosSobre()
        {
            return await (from x in Db.Sobre select x).FirstOrDefaultAsync();
        }
    }
}
