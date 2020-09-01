using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using DevExpress.ExpressApp.SystemModule;

namespace SIAF.Module.BusinessObjects
{
    public class EnviarCorreo
    {
        public static void Enviar(System.Net.Mail.MailMessage email, TipoCorreo tipo)
        {
           /* try
            { SendEmail(email, tipo); }
            catch
            { throw new NotImplementedException("No se pudo enviar correo. Favor comunicarse con informática");}
            * */
        }
        
        public static void SendEmail(System.Net.Mail.MailMessage m, TipoCorreo tipo)
        {
            string NetWorkEmail = System.Configuration.ConfigurationManager.AppSettings["NetEmail"];
            string NetWorkPassword = System.Configuration.ConfigurationManager.AppSettings["NetPassword"];

            if (tipo == TipoCorreo.ReseteoDeClave)
                m.From = new MailAddress(NetWorkEmail, "Nueva clave " + AboutInfo.Instance.ProductName);
            else if (tipo == TipoCorreo.Informativo)
                m.From = new MailAddress(NetWorkEmail, "Informativo " + AboutInfo.Instance.ProductName);
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = System.Configuration.ConfigurationManager.AppSettings["NetHost"];
            smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NetPort"]);
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(NetWorkEmail, NetWorkPassword);

            SendEmailDelegate sd = new SendEmailDelegate(smtp.Send);
            AsyncCallback cb = new AsyncCallback(SendEmailResponse);
            sd.BeginInvoke(m, cb, sd);
        }

        private delegate void SendEmailDelegate(System.Net.Mail.MailMessage m);
        private static void SendEmailResponse(IAsyncResult ar)
        {
            SendEmailDelegate sd = (SendEmailDelegate)(ar.AsyncState);
            sd.EndInvoke(ar);
        }
    }
}
