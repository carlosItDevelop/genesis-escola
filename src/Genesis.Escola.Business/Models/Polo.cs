using Genesis.Escola.Business.Models.Base;

namespace Genesis.Escola.Business.Models
{
    public class Polo : Entity
    {
        public string Titulo { get; set; }
        public string CaminhoImagem1 { get; set; }
        public string CaminhoImagem2 { get; set; }
        public string CaminhoImagem3 { get; set; }
        public string NomeCurso1 { get; set; }
        public string NomeCurso2 { get; set; }
        public string NomeCurso3 { get; set; }
        public string DescricaoCurso1 { get; set; }
        public string DescricaoCurso2 { get; set; }
        public string DescricaoCurso3 { get; set; }
        public string LinkCurso1 { get; set; }
        public string LinkCurso2 { get; set; }
        public string LinkCurso3 { get; set; }
    }
}
