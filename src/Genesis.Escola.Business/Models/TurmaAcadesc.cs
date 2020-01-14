using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models
{
    public class TurmaAcadesc : Entity
    {
        public string Ciclo { get; set; }
        public string Serie { get; set; }
        public string Turma { get; set; }
        public string Turno { get; set; }
        public string Nome { get; set; }
    }
}
