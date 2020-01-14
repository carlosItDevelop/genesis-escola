using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Genesis.Escola.MVC.Functions.Enumeradores;

namespace Genesis.Escola.MVC.services
{
    public interface IEmailService
    {
        Task SendEmail(string email, string nome, string assunto, string menssagem, string para, ConfigViewModel config, EnumTipoEmail tipo, IFormFile file=null);
    }
}
