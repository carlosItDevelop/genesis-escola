using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class TurmaAcadescRepositorio : RepositorioGenerico<TurmaAcadesc>, ITurmaAcadescRepositorio
    {
        public TurmaAcadescRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<IEnumerable<TurmaAcadesc>> PegarTurmasCiclo(string ciclo)
        {
            return await Db.TurmaAcadesc.Where(n => n.Ciclo.GetHashCode() == ciclo.GetHashCode())
                                  .Select(n => n).ToListAsync();
        }

        public async Task<TurmaAcadesc> TurmaExiste(string serie, string turma, string turno)
        {
            return await Db.TurmaAcadesc.Where(b => b.Turma.GetHashCode() == turma.GetHashCode() && b.Serie.GetHashCode() == serie.GetHashCode() && b.Turno.GetHashCode() == turno.GetHashCode()).FirstOrDefaultAsync();
        }
    }
}
