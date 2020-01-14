using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class CarrosselViewModel
    {
        public CarrosselViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }
        [Display(Name = "Descrição Resumida")]
        public string Resumo { get; set; }
        [Display(Name = "Imagem")]
        public string CaminhoImagem { get; set; }
        public string Imagem { get; set; }

        [Display(Name = "Status")]
        public string Ativo { get; set; }
        public byte[] ImagemUpload { get; set; }

        //[Display(Name = "Data Inicial")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //public DateTime? DataInicio { get; set; }

        //[Display(Name = "Data Final")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //public DateTime? DataFinal { get; set; }
    }
}
