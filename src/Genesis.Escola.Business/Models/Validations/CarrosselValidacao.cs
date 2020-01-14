using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class CarrosselValidacao : AbstractValidator<Carrossel>
    {
        public CarrosselValidacao()
        {
            RuleFor(a => a.Titulo)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(10, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Resumo)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(10, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            //RuleFor(a => a.DataInicio).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty()
            //        .Must(date => date != default(DateTime))
            //        .WithMessage("O campo {PropertyName} precisa ser fornecido");

            //RuleFor(a => a.DataFinal)
            //    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            //    .GreaterThanOrEqualTo(a => a.DataInicio.Value)
            //    .WithMessage("A Data Final deve ser maior da Data de Início")
            //    .When(a => a.DataInicio.HasValue);
        }
    }
}
