using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Messages
{
    public class EmailServiceMessage
    {
        private string _conta = "cotiaulasnoreply@gmail.com";
        private string _senha = "@coti123456";
        private string _smtp = "smtp.gmail.com";
        private int _porta = 587;

        public void EnviarMensagem(string to, string subject, string body)
        {
            //configurando o conteudo do email que será enviado
            var message = new MailMessage(_conta, to); //remetente / destinatario
            message.Subject = subject; //assunto 
            message.Body = body;
            message.IsBodyHtml = true;

            //realizando o envio do email
            var smtp = new SmtpClient(_smtp, _porta);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_conta, _senha);
            smtp.Send(message);
        }
    }
}





