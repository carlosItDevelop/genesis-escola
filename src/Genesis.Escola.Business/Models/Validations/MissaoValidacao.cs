using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class MissaoValidacao : AbstractValidator<Missao>
    {
        public MissaoValidacao()
        {
            RuleFor(a => a.Titulo)
                .NotEmpty().WithMessage("Informe o Título é obrigatório")
                .Length(5, 80).WithMessage("O nome deve ter no minimo 5 caracteres e no maximo 80 caracteres");

            RuleFor(a => a.Descricao)
                .NotEmpty().WithMessage("Informe a Descrição é obrigatória");
        }
    }
}
