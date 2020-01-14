using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class PoloValidacao : AbstractValidator<Polo>
    {
        public PoloValidacao()
        {
            RuleFor(a => a.Titulo)
                .NotEmpty().WithMessage("Informe o Nome do Polo")
                .Length(10, 60).WithMessage("O nome do Polo deve ter no minimo 10 caracteres e no maximo 60 caracteres");

            RuleFor(a => a.DescricaoCurso1)
                .NotEmpty().WithMessage("Informe a Descrição do Curso 1")
                .Length(10, 300).WithMessage("A Descrição do Curso 1 deve ter no minimo 10 caracteres e no maximo 300 caracteres");

            RuleFor(a => a.DescricaoCurso2)
                .NotEmpty().WithMessage("Informe a Descrição do Curso 2")
                .Length(10, 300).WithMessage("A Descrição do Curso 2 deve ter no minimo 10 caracteres e no maximo 300 caracteres");

            RuleFor(a => a.DescricaoCurso3)
                .NotEmpty().WithMessage("Informe a Descrição do Curso 3")
                .Length(10, 300).WithMessage("A Descrição do Curso 3 deve ter no minimo 10 caracteres e no maximo 300 caracteres");

            RuleFor(a => a.LinkCurso1)
                .NotEmpty().WithMessage("Informe o Link de acesso do Curso 1");
            RuleFor(a => a.LinkCurso2)
                .NotEmpty().WithMessage("Informe o Link de acesso do Curso 2");
            RuleFor(a => a.LinkCurso3)
                .NotEmpty().WithMessage("Informe o Link de acesso do Curso 3");

            RuleFor(a => a.NomeCurso1)
                .NotEmpty().WithMessage("Informe o Nome do Curso 1")
                .Length(5, 40).WithMessage("O Nome do Curso 1 deve ter no minimo 5 caracteres e no maximo 40 caracteres");

            RuleFor(a => a.NomeCurso2)
                .NotEmpty().WithMessage("Informe o Nome do Curso 2")
                .Length(5, 40).WithMessage("O Nome do Curso 2 deve ter no minimo 5 caracteres e no maximo 40 caracteres");

            RuleFor(a => a.NomeCurso3)
                .NotEmpty().WithMessage("Informe o Nome do Curso 3")
                .Length(5, 40).WithMessage("O Nome do Curso 3 deve ter no minimo 5 caracteres e no maximo 40 caracteres");

        }
    }
}
