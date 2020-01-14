using Genesis.Escola.Business.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.Business.Models
{
    public class Missao : Entity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
