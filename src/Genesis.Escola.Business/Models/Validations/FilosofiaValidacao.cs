using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class FilosofiaValidacao : AbstractValidator<Filosofia>
    {
        public FilosofiaValidacao()
        {
            RuleFor(a => a.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(5, 80).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
