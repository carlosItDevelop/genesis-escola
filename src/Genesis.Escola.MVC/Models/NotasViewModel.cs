using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class NotasViewModel
    {
        public string Materia { get; set; }
        public string Nota1b { get; set; }
        public string Nota2b { get; set; }
        public string Nota3b { get; set; }
        public string Nota4b { get; set; }
        public string MediaNota { get; set; }
        public string RecuperacaoResultFinal { get; set; }
        public string MediaFinalResultFinal { get; set; }
        public string SituacaoResultFinal { get; set; }
        public int Faltas1b { get; set; }
        public int Faltas2b { get; set; }
        public int Faltas3b { get; set; }
        public int Faltas4b { get; set; }
        public int FaltasTotal { get; set; }

    }
}
