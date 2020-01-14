using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System.Threading.Tasks;
using static Genesis.Escola.Business.Enumeradores.Enumeradores;

namespace Genesis.Escola.Business.Interfaces
{
    public interface ICursosRepositorio : IRepositorioGenerico<Cursos>
    {
        Task<Cursos> PegarDados(EnumCurso curso);
    }
}
