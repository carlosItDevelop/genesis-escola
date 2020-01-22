using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.ViewModel
{
    public class ConfigViewModel
    {
        [Key]
        public Guid Id { get; set; }
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
        public string EmailRetContato { get; set; }
        public string LinkYoutube { get; set; }
        public string ImagemYoutube { get; set; }
        public byte[] ImagemUpload { get; set; }
    }
}
