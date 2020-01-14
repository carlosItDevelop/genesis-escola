using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class TurmaValidacao : AbstractValidator<Turma>
    {
        public TurmaValidacao()
        {
            RuleFor(a => a.Nome)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Sigla)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(1, 10).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
