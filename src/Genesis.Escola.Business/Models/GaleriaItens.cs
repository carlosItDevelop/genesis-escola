using Genesis.Escola.Business.Models.Base;
using System;

namespace Genesis.Escola.Business.Models
{
    public class GaleriaItens : Entity
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public Guid GaleriaId { get; set; }
        public virtual Galeria Galeria { get; set; }
        public string CaminhoImagem { get; set; }
    }
}
