using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.Api.ViewModel
{
    public class ComunicadoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Resumo do Comunicado")]
        public string DescricaoResumida { get; set; }

        [Display(Name = "Data Inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Comunicado Completo")]
        public string DescricaoCompleta { get; set; }
        public string TurmaId { get; set; }

        public byte[] ImagemUpload { get; set; }

        [Display(Name = "Arquivo PDF")]
        public string CaminhoImagem { get; set; }
    }
}
