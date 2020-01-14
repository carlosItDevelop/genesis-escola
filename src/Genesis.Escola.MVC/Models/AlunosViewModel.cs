using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class AlunosViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Serie { get; set; }
        public string Turma { get; set; }
        public string Turno { get; set; }
        public Guid TurmaId { get; set; }
        public string Numero { get; set; }

        public bool Ativo { get; set; }

        //public List<SelectListItem> TurmaList { get; set; }


    }
}
