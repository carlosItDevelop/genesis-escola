using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class SobreValidacao : AbstractValidator<Sobre>
    {
        public SobreValidacao()
        {
            RuleFor(a => a.SobreResumido)
            .NotEmpty().WithMessage("Informe a Descrição Resumida é obrigatória")
            .Length(10, 800).WithMessage("O nome deve ter no minimo 10 caracteres e no maximo 800 caracteres");

            RuleFor(a => a.SobreCompleto)
            .NotEmpty().WithMessage("Informe a Descrição Completa é obrigatória");
        }
    }
}
