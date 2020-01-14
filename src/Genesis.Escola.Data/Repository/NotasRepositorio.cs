using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class NotasRepositorio : RepositorioGenerico<Notas>, INotasRepositorio
    {
        public NotasRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<IEnumerable<Notas>> PegarPorAluno(string turma)
        {
            return await Db.Notas.Where(n => n.Matricula == turma)
                               .Select(n => n).ToListAsync();
        }
    }
}
