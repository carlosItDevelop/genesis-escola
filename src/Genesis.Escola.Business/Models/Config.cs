using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models
{
    public class Config : Entity
    {
        public string TituloContato { get; set; }
        public string DescricaoContato { get; set; }
        public string TituloTrabalhe { get; set; }
        public string DescricaoTrabalhe { get; set; }
        public string Endereco { get; set; }
        public string Telefones { get; set; }
        public string EmailRetTrabalhe { get; set; }
        public string EmailEnvio { get; set; }
        public string EmailSenha { get; set; }
        public string EmailHost { get; set; }
        public int EmailPorta { get; set; }
        public bool EmailSsl { get; set; }
        public string MensagemRetContato { get; set; }
        public string MensagemRetTrabalhe { get; set; }
    }
}
