using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class TurmaAcadescValidacao : AbstractValidator<TurmaAcadesc>
    {
        public TurmaAcadescValidacao()
        {
            RuleFor(a => a.Nome)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
