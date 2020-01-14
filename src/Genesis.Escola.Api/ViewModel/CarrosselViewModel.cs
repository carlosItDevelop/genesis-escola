using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.Api.ViewModel
{
    public class CarrosselViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }
        [Display(Name = "Descrição Resumida")]
        public string Resumo { get; set; }
        [Display(Name = "Imagem")]
        public string CaminhoImagem { get; set; }
        public string Imagem { get; set; }
        public byte[] ImagemUpload { get; set; }
        public string Ativo { get; set; }
                                           //public DateTime? DataInicio { get; set; }
                                           //public DateTime? DataFinal { get; set; }
    }
}
