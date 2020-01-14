using Genesis.Escola.Business.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.Business.Models
{
    public class Carrossel : Entity
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string CaminhoImagem { get; set; }
        //public DateTime? DataInicio { get; set; }
        //public DateTime? DataFinal { get; set; }
        public string Ativo { get; set; }
    }
}
