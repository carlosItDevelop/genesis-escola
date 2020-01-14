using Genesis.Escola.Business.Models.Base;

namespace Genesis.Escola.Business.Models
{
    public class Sobre : Entity
    {
        public string SobreResumido { get; set; }
        public string SobreCompleto { get; set; }
        public string CaminhoImagemPrincipal { get; set; }
    }
}
