using Genesis.Escola.Business.Models.Base;
using System;

namespace Genesis.Escola.Business.Models
{

    public class Noticia : Entity
    {
        public string DescricaoResumida { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string DescricaoCompleta { get; set; }
    }
}
