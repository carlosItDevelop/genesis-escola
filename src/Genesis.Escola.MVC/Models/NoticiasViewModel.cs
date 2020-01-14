using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class NoticiasViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Resumo da Notícia")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(800, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]

        public string DescricaoResumida { get; set; }

        [Display(Name = "Data Inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Notícia Completa")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string DescricaoCompleta { get; set; }

    }
}
