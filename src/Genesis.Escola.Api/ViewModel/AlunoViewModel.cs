using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.Api.ViewModel
{
    public class AlunoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Turma { get; set; }
        public string Serie { get; set; }
        public string Turno { get; set; }
        public string Curso { get; set; }
        public string Numero { get; set; }
        public bool Ativo { get; set; }
    }
}
