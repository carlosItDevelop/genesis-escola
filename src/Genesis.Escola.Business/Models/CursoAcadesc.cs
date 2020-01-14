using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models
{
    public class CursoAcadesc : Entity
    {
        public string Ciclo { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int NumSeries { get; set; }
    }
}
