using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class ConfigRepositorio : RepositorioGenerico<Config>, IConfigRepositorio
    {
        public ConfigRepositorio(Contexto contexto) : base(contexto) { }
        public async Task<Config> PegarDados()
        {
            return await (from x in Db.Config select x).FirstOrDefaultAsync();
        }
    }
}
