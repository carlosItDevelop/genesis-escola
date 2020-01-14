using Genesis.Escola.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models
{
    public class EmailContato : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
