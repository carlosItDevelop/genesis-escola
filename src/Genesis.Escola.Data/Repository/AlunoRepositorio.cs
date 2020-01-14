using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;

namespace Genesis.Escola.Data.Repository
{
    public class AlunoRepositorio : RepositorioGenerico<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<Aluno> AlunoExiste(string matricula, string senha)
        {
            return await Db.Aluno.Where(b => b.Matricula.GetHashCode() == matricula.GetHashCode() && b.Senha.GetHashCode() == senha.GetHashCode()).FirstOrDefaultAsync();
        }

        public async Task<Aluno> AlunoExiste(string matricula)
        {
            return await Db.Aluno.Where(b => b.Matricula.GetHashCode() == matricula.GetHashCode()).FirstOrDefaultAsync();
        }
    }
}
