using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class UsuarioViewModel : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public string Usuario { get; set; }
    }
}
