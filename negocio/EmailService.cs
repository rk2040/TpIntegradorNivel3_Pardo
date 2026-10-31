using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("pruebasconcsharp@gmail.com", ConfigurationManager.AppSettings["EmailPassword"]);
            //server.Credentials = new NetworkCredential("pruebasconcsharp@gmail.com", "xxxxxxxxxxxxxx");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";

            string usuario = ConfigurationManager.AppSettings["EmailPassword"];
            System.Diagnostics.Debug.WriteLine("Contraseña cargada: " + !string.IsNullOrEmpty(usuario));
        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@catalogo-web.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                // Guardo el error en la nueva carpeta con el nuevo metodo
                Seguridad.manejoError("enviarEmail", ex); //podría usar (nameof(enviarEmail), ex); 
                // Y relanzo el error limpio para que la capa superior pueda saber del fallo
                throw;
            }
        }
    }
}
