using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.ViewModel
{
    public class MissaoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
