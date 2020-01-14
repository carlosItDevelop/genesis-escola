using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Validations
{
    public class ConfigValidacao : AbstractValidator<Config>
    {
        public ConfigValidacao()
        {
            RuleFor(a => a.TituloContato)
                          .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                          .Length(6, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.DescricaoContato)
                          .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                          .Length(10, 400).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Endereco)
                          .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                          .Length(10, 300).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Telefones)
                          .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                          .Length(10, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.EmailEnvio)
              .NotEmpty().WithMessage("O campo E-Mail de Envio precisa ser fornecido")
              .Length(10, 100).WithMessage("O campo E-Mail de Envio precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.EmailSenha)
                          .NotEmpty().WithMessage("O campo Senha do E-Mail precisa ser fornecido")
                          .Length(5, 100).WithMessage("O campo Senha do E-Mail precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.EmailHost)
                          .NotEmpty().WithMessage("O campo Smtp do Email precisa ser fornecido")
                          .Length(5, 100).WithMessage("O campo Smtp do Email precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.MensagemRetContato)
                          .NotEmpty().WithMessage("O campo Mensagem de Retorno do Contato precisa ser fornecido")
                          .MinimumLength(10).WithMessage("O campo Mensagem de Retorno do Contato precisa ter entre {MinLength} caracteres");

            RuleFor(a => a.MensagemRetTrabalhe)
                          .NotEmpty().WithMessage("O campo Mensagem de Retorno do Trabalhe Conosco precisa ser fornecido")
                          .MinimumLength(10).WithMessage("O campo Mensagem de Retorno do Trabalhe Conosco precisa ter entre {MinLength} caracteres");


            RuleFor(a => a.EmailRetTrabalhe)
              .NotEmpty().WithMessage("O campo E-Mail de Trabalhe Conosco precisa ser fornecido");



        }
    }
}
