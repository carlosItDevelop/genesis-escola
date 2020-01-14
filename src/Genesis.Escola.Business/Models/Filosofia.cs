using Genesis.Escola.Business.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Escola.Business.Models
{
    public class Filosofia : Entity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
