using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models
{
    public class Disciplinas : Entity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string CargaHoraria { get; set; }
        public string Professor { get; set; }
        public string Cargo { get; set; }
        public string Curso { get; set; }
        public string Serie { get; set; }
        public string Turma { get; set; }

    }
}
