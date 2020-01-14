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
    public class DisciplinasRepositorio : RepositorioGenerico<Disciplinas>, IDisciplinasRepositorio
    {
        public DisciplinasRepositorio(Contexto contexto) : base(contexto) { }

        public Disciplinas PegarPorCodigo(string codigo)
        {
            //var student =  (from s in Db.Disciplinas
            //               where s.Codigo == codigo
            //               select s).FirstOrDefault<Disciplinas>();
            return Db.Disciplinas.Where(n => n.Codigo == codigo).FirstOrDefault<Disciplinas>();
        }
    }
}
