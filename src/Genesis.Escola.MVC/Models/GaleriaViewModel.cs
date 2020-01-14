using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.MVC.Models
{
    public class GaleriaViewModel
    {
        public GaleriaViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(400, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Descricao { get; set; }

        [Display(Name = "Imagem")]
        public string CaminhoImagem { get; set; }
        public byte[] ImagemUpload { get; set; }
        public virtual ICollection<GaleriaItensViewModel> galeriaItens { get; set; }
    }
}
