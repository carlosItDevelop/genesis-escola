using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
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


        public async Task SendEmail(string email, string nome, string assunto, string mensagem, string para, ConfigViewModel config, EnumTipoEmail tipo, IFormFile anexo = null)
        {

            using (MailMessage mm = new MailMessage(email, para))
            {
                //mm.Subject = "Curriculum Enviado pelo site ";
                mm.IsBodyHtml = true;
                var corpoEmail = new StringBuilder();
                var estilo1 = " style=" + '"' + "font-family:Tahoma, Geneva, sans-serif !important;" + ";font-size:36px;font-weight:normal;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:inherit;background-color:#34696d;color:#ffffff;text-align:center;vertical-align:top" + '"' + "colspan=" + '"' + "2" + '"';
                var estilo2 = " style=" + '"' + "font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:inherit;text-align:right;vertical-align:top" + '"';
                var estilo3 = " style=" + '"' + "font-family:Arial, sans-serif;font-size:14px;padding:10px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:inherit;text-align:left;vertical-align:top" + '"';

                if (tipo == EnumTipoEmail.Contato)
                {
                    corpoEmail = new StringBuilder();
                    corpoEmail.AppendLine("<html>");
                    corpoEmail.AppendLine("<body style=" + '"' + "margin: 0; padding: 0; " + '"' + ">");
                    corpoEmail.AppendLine("<table style=" + '"' + "border-collapse:collapse; border-spacing:0" + '"' + " class=" + '"' + "tg" + '"' + ">");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<th" + estilo1 + ">" + "Contato do Site" + "</th>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<td" + estilo2 + ">" + "Nome:" + "</td>");
                    corpoEmail.AppendLine("<td" + estilo3 + ">" + nome + "</td>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<td" + estilo2 + ">" + "E-Mail:" + "</td>");
                    corpoEmail.AppendLine("<td" + estilo3 + ">" + email + "</td>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<td" + estilo2 + ">" + "Assunto:" + "</td>");
                    corpoEmail.AppendLine("<td" + estilo3 + ">" + assunto + "</td>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<td" + estilo2 + ">" + "Mensagem:" + "</td>");
                    corpoEmail.AppendLine("<td" + estilo3 + ">" + mensagem + "</td>");
                    corpoEmail.AppendLine("</tr>");

                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("</table>");
                    corpoEmail.AppendLine("</body>");
                    mm.Subject = "Enviado Pelo Site - Contato " ;
                }
                else
                {
                    corpoEmail = new StringBuilder();
                    corpoEmail.AppendLine("<html>");
                    corpoEmail.AppendLine("<body style=" + '"' + "margin: 0; padding: 0; " + '"' + ">");
                    corpoEmail.AppendLine("<table style=" + '"' + "border-collapse:collapse; border-spacing:0" + '"' + " class=" + '"' + "tg" + '"' + ">");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<th" + estilo1 + ">" + "Contato do Site" + "</th>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<td" + estilo2 + ">" + "Nome:" + "</td>");
                    corpoEmail.AppendLine("<td" + estilo3 + ">" + nome + "</td>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<td" + estilo2 + ">" + "E-Mail:" + "</td>");
                    corpoEmail.AppendLine("<td" + estilo3 + ">" + email + "</td>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("<td" + estilo2 + ">" + "Mensagem:" + "</td>");
                    corpoEmail.AppendLine("<td" + estilo3 + ">" + mensagem + "</td>");
                    corpoEmail.AppendLine("</tr>");
                    corpoEmail.AppendLine("<tr>");
                    corpoEmail.AppendLine("</table>");
                    corpoEmail.AppendLine("</body>");
                    mm.Subject = "Enviado Pelo Site - Trabalhe Conosco";
                }



                mm.Body = corpoEmail.ToString();
                if (anexo != null)
                {
                    if (anexo.Length > 0)
                    {
                        string fileName = Path.GetFileName(anexo.FileName);
                        mm.Attachments.Add(new Attachment(anexo.OpenReadStream(), fileName));
                    }
                }
                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.Host = config.EmailHost;
                    smtp.EnableSsl = config.EmailSsl;
                    NetworkCredential NetworkCred = new NetworkCredential(config.EmailEnvio, config.EmailSenha);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = config.EmailPorta;
                    smtp.Send(mm);
                }
            }


            await Task.CompletedTask;
        }

    }
}
