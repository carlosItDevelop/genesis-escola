using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface IAlunoRepositorio : IRepositorioGenerico<Aluno>
    {
        Task<Aluno> AlunoExiste(string aluno);
        Task<Aluno> AlunoExiste(string aluno, string senha);
    }
}
