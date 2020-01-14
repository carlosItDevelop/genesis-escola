using FluentValidation;

namespace Genesis.Escola.Business.Models.Validations
{
    public class AlunoValidacao : AbstractValidator<Aluno>
    {
        public AlunoValidacao()
        {
            RuleFor(a => a.Nome)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(3, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


        }
    }
}
