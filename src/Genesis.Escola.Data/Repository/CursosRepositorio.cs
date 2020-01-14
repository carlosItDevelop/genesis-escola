using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static Genesis.Escola.Business.Enumeradores.Enumeradores;

namespace Genesis.Escola.Data.Repository
{
    public class CursosRepositorio : RepositorioGenerico<Cursos>, ICursosRepositorio
    {
        public CursosRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<Cursos> PegarDados(EnumCurso curso)
        {
            return await Db.Cursos.Where(b => b.Curso.GetHashCode() == curso.GetHashCode()).FirstOrDefaultAsync();
        }
    }
}
