using Genesis.Escola.MVC.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.IO;
using System.Threading.Tasks;
using static Genesis.Escola.MVC.Functions.Enumeradores;

namespace Genesis.Escola.MVC.services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string email, string nome, string assunto, string menssagem, string para, ConfigViewModel config, EnumTipoEmail tipo, IFormFile anexo= null)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(nome, email));
            if (tipo == EnumTipoEmail.TrabalheConosco)
            {
                message.To.Add(new MailboxAddress("E-mail Enviado pelo Site", config.EmailRetTrabalhe));
                message.Subject = "Enviado Pelo Site: Curriculum ";
            }
            else
            {
                message.To.Add(new MailboxAddress("E-mail Enviado pelo Site", para));
                message.Subject = "Enviado Pelo Site: " + assunto;
            }

            var builder = new BodyBuilder { HtmlBody = menssagem + "<br/><br/> A mensagem foi enviada por: " + nome + " E-mail: " + email };

            if (anexo != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await anexo.CopyToAsync(memoryStream);
                    builder.Attachments.Add(anexo.FileName, memoryStream.ToArray());
                }
            }
            message.Body = builder.ToMessageBody();

            //Configure the e-mail
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(config.EmailHost , config.EmailPorta , config.EmailSsl);
                emailClient.Authenticate(config.EmailEnvio ,config.EmailSenha);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }


            await Task.CompletedTask;
        }


    }
}
