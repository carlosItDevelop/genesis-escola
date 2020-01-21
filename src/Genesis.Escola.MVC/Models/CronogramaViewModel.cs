using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class CronogramaViewModel : IValidatableObject
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Titulo")]
        [StringLength(600, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 3)]
        public string DescricaoResumida { get; set; }

        [Display(Name = "Data Inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataFinal { get; set; }


        [Display(Name = "Descrição do Cronograma")]
        public string DescricaoCompleta { get; set; }
        public string TurmaId { get; set; }

        public byte[] ImagemUpload { get; set; }

        [Display(Name = "Arquivo PDF")]
        public string CaminhoImagem { get; set; }
        public string selectedItems { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataFinal < DataInicio)
            {
                yield return
                  new ValidationResult(errorMessage: "Data Final menor que Data Inicial",
                                       memberNames: new[] { "DataFinal" });
            }
        }
    }
}
