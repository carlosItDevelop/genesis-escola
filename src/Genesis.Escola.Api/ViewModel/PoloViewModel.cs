using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.ViewModel
{
    public class PoloViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Imagem Curso 1")]
        public string CaminhoImagem1 { get; set; }

        [Display(Name = "Imagem Curso 2")]
        public string CaminhoImagem2 { get; set; }

        [Display(Name = "Imagem Curso 3")]
        public string CaminhoImagem3 { get; set; }

        [Display(Name = "Nome do Curso 1")]
        public string NomeCurso1 { get; set; }

        [Display(Name = "Nome do Curso 2")]
        public string NomeCurso2 { get; set; }

        [Display(Name = "Nome do Curso 3")]
        public string NomeCurso3 { get; set; }

        [Display(Name = "Descrição Curso 1")]
        public string DescricaoCurso1 { get; set; }

        [Display(Name = "Descrição Curso 2")]
        public string DescricaoCurso2 { get; set; }
        [Display(Name = "Descrição Curso 3")]
        public string DescricaoCurso3 { get; set; }

        public string LinkCurso1 { get; set; }
        public string LinkCurso2 { get; set; }
        public string LinkCurso3 { get; set; }
        public byte[] ImagemUpload1 { get; set; }
        public byte[] ImagemUpload2 { get; set; }
        public byte[] ImagemUpload3 { get; set; }
    }
}
