using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class FilosofiaRepositorio : RepositorioGenerico<Filosofia>, IFilosofiaRepositorio
    {
        public FilosofiaRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<Filosofia> PegarDados()
        {
            return await (from x in Db.Filosofia select x).FirstOrDefaultAsync();
        }
    }
}
