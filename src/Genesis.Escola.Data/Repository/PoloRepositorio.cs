using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class PoloRepositorio : RepositorioGenerico<Polo>, IPoloRepositorio
    {
        public PoloRepositorio(Contexto contexto) : base(contexto) { }
        public async Task<Polo> PegarDados()
        {
            return await (from x in Db.Polo select x).FirstOrDefaultAsync();
        }
    }
}
