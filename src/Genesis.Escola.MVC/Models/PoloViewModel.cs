using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class PoloViewModel
    {
        public PoloViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Titulo { get; set; }

        [Display(Name = "Imagem Curso 1")]
        public string CaminhoImagem1 { get; set; }

        [Display(Name = "Imagem Curso 2")]
        public string CaminhoImagem2 { get; set; }

        [Display(Name = "Imagem Curso 3")]
        public string CaminhoImagem3 { get; set; }

        [Display(Name = "Nome do Curso 1")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string NomeCurso1 { get; set; }

        [Display(Name = "Nome do Curso 2")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string NomeCurso2 { get; set; }

        [Display(Name = "Nome do Curso 3")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string NomeCurso3 { get; set; }

        [Display(Name = "Descrição Curso 1")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string DescricaoCurso1 { get; set; }

        [Display(Name = "Descrição Curso 2")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string DescricaoCurso2 { get; set; }
        [Display(Name = "Descrição Curso 3")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string DescricaoCurso3 { get; set; }


        [Display(Name = "Link da Imagem Curso 1")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string LinkCurso1 { get; set; }

        [Display(Name = "Link da Imagem Curso 2")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string LinkCurso2 { get; set; }

        [Display(Name = "Link da Imagem Curso 3")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string LinkCurso3 { get; set; }
        public byte[] ImagemUpload1 { get; set; }
        public byte[] ImagemUpload2 { get; set; }
        public byte[] ImagemUpload3 { get; set; }
        //Tatiana 98292-6300
    }
}
