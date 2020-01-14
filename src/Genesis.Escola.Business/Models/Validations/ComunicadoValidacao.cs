using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class ComunicadoValidacao : AbstractValidator<Comunicado>
    {
        public ComunicadoValidacao()
        {
            RuleFor(a => a.DescricaoResumida)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 600).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            //RuleFor(a => a.DescricaoCompleta)
            //    .NotEmpty().WithMessage("Informe a Descrição Completa da notícia");


            RuleFor(a => a.DataInicio).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty()
                    .Must(date => date != default(DateTime))
                    .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(a => a.DataFinal)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThanOrEqualTo(a => a.DataInicio.Value)
                .WithMessage("A Data Final deve ser maior da Data de Início")
                .When(a => a.DataInicio.HasValue);
        }
    }
}
