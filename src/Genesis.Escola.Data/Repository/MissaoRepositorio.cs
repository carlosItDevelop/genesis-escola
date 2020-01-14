using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class MissaoRepositorio : RepositorioGenerico<Missao>, IMissaoRepositorio
    {
        public MissaoRepositorio(Contexto contexto) : base(contexto) { }
        public async Task<Missao> PegarDados()
        {
            return await (from x in Db.Missao select x).FirstOrDefaultAsync();
        }
    }
}
