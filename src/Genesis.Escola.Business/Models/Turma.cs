using Genesis.Escola.Business.Models.Base;
using System.Collections.Generic;

namespace Genesis.Escola.Business.Models
{
    public class Turma : Entity
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        //public int Ano { get; set; }

       // public ICollection<Aluno> Alunos { get; set; }
    }
}
