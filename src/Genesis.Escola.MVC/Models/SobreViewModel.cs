using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class SobreViewModel
    {
        public SobreViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(800, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        [Display(Name = "Descrição Resumida")]
        public string SobreResumido { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição Completa")]
        public string SobreCompleto { get; set; }

        public string CaminhoImagemPrincipal { get; set; }
        public byte[] ImagemUpload { get; set; }
    }
}
