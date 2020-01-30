using Genesis.Escola.MVC.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using static Genesis.Escola.MVC.Functions.Enumeradores;

namespace Genesis.Escola.MVC.services
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        readonly ILogger<EmailService> _logger;
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task SendEmail(string email, string nome, string assunto, string menssagem, string para, ConfigViewModel config, EnumTipoEmail tipo, IFormFile anexo = null)
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
            _logger.LogWarning("Erro :: Email -> Iniciando");
            message.Body = builder.ToMessageBody();
            //Configure the e-mail
            try
            {
                using (var emailClient = new SmtpClient())
                {
                    emailClient.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Ssl2;

                    emailClient.Connect(config.EmailHost, config.EmailPorta, config.EmailSsl);
                    emailClient.Authenticate(config.EmailEnvio.Trim(), config.EmailSenha.Trim());

                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning("Erro :: Email -> " + ex.Message);
                
            }



            await Task.CompletedTask;
        }




    }
}
