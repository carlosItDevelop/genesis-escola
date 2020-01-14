using Genesis.Escola.Business.Models.Base;
using System.Collections.Generic;

namespace Genesis.Escola.Business.Models
{
    public class Galeria : Entity    {
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string CaminhoImagem { get; set; }
        public virtual ICollection<GaleriaItens> galeriaItens { get; set; }
    }
}
