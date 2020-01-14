using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class GaleriaItensValidacao : AbstractValidator<GaleriaItens>
    {
        public GaleriaItensValidacao()
        {
            RuleFor(a => a.Titulo)
                 .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                 .Length(10, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Resumo)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(10, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
