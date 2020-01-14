using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models
{
    public class Notas : Entity
    {
        public string Matricula { get; set; }
        public string Disciplina { get; set; }
        public string Nota1 { get; set; }
        public string Nota2 { get; set; }
        public string Nota3 { get; set; }
        public string Nota4 { get; set; }
        public string Nt1 { get; set; }
        public string Nt2 { get; set; }
        public string Nt3 { get; set; }
        public string Nt4 { get; set; }
        public string NotaRec1 { get; set; }
        public string NotaRec2 { get; set; }
        public string NotaRec3 { get; set; }
        public string NotaRec4 { get; set; }
        public string NotaRecSem1 { get; set; }
        public string NotaRecSem2 { get; set; }
        public string MediaSem1 { get; set; }
        public string MediaSem2 { get; set; }
        public bool Cor1 { get; set; }
        public bool Cor2 { get; set; }
        public bool Cor3 { get; set; }
        public bool Cor4 { get; set; }
        public bool CorRec { get; set; }
        public string MediaBim { get; set; }
        public string NotaRec { get; set; }
        public string MediaFinal { get; set; }
        public string Situacao { get; set; }
        public string CorSituacao { get; set; }
        public Int32 Falta1 { get; set; }
        public Int32 Falta2 { get; set; }
        public Int32 Falta3 { get; set; }
        public Int32 Falta4 { get; set; }
        public double PerFalta { get; set; }
        public double PerFrequencia { get; set; }
        public Int32 TotalFaltas { get; set; }
        public string Observacoes { get; set; }
    }
}
