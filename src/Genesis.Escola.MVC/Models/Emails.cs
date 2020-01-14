using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class Emails
    {

        [Required(ErrorMessage = "O E-Mail é Obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "E-Mail Inválido!!!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Departamento é Obrigatório", AllowEmptyStrings = false)]
        public string EmailPara { get; set; }

        [Required(ErrorMessage = "O Assunto é Obrigatório", AllowEmptyStrings = false)]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "A Menssagem é Obrigatória", AllowEmptyStrings = false)]
        public string Mensagem { get; set; }


        [Required(ErrorMessage = "O Nome é Obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


    }
}
