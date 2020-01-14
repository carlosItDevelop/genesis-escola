using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models
{
    public class Cronograma : Entity
    {
        public string DescricaoResumida { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string DescricaoCompleta { get; set; }
        public string TurmaId { get; set; }
        public string CaminhoImagem { get; set; }
    }
}
