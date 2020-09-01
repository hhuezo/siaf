using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using System.Security.Cryptography;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using SIAF.Module.BusinessObjects.Seguridad;
using DevExpress.ExpressApp.SystemModule;
using SIAF.Module.BusinessObjects;
using System.IO;
using System.Web;
using System.Net.Mime;
using System.Windows.Forms;
using SIAF.Module;
namespace Security.Extensions {
    [DomainComponent]
    public abstract class LogonActionParametersBase {        
        [RuleRequiredField()]
        public string Usuario { get; set; }
        public abstract void ExecuteBusinessLogic(IObjectSpace objectSpace);
    }

    [DomainComponent]
    [ModelDefault("Caption", "Restauración de clave")]
    [ImageName("Action_ResetPassword")]
    public class RestorePasswordParameters : LogonActionParametersBase {
        public override void ExecuteBusinessLogic(IObjectSpace objectSpace) {
            if (string.IsNullOrEmpty(Usuario))
                throw new ArgumentException("Usuario no especificado");
            if (Usuario== "Admin")
                throw new ArgumentException("Usuario no especificado");
            Usuario usuario = objectSpace.FindObject<Usuario>(new BinaryOperator("UserName", Usuario));
            if (usuario == null)
                throw new ArgumentException("No se encontró el usuario proporcionado");
            byte[] randomBytes = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(randomBytes);
            string password = Convert.ToBase64String(randomBytes);
            usuario.SetPassword(password);
            usuario.ChangePasswordOnFirstLogon = true;
            objectSpace.CommitChanges();
           // EmailLoginInformation(usuario.Email, password);
            RegistrarReseteo.Registrar(usuario.Oid);
        }

        private void PrepararCorreo(string direccion, string password)
        {
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(direccion));
            email.Subject = "Reseteo de clave";
            email.Priority = MailPriority.High;
            email.Body = LlenarBody(password);
            email.IsBodyHtml = true;
            //EnviarCorreo.Enviar(email, TipoCorreo.ReseteoDeClave);
        }

        public static void EmailLoginInformation(string direccion, string password)
        {
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(direccion));
            email.Subject = "Reseteo de clave";
            email.Body = "<center><table style='width:600px;border: 1px solid LightSkyBlue ;border-collapse: collapse;padding: 15px'>" +
                         "<tr><td style='padding: 15px;' colspan=2><center><img alt='' src='cid:siaf.png' /></center></tr>" +
                         "<tr><td style='padding: 15px; text-align:justify' colspan=2><br><br><font face='Calibri'>" + 
                         "De acuerdo a su solicitud, se ha procedido a resetear su clave del " + AboutInfo.Instance.ProductName +
                         "<br> La nueva clave es: <b>" + password + "</b><br>" +
                         "El sistema le solicitará que ingrese una nueva clave personalizada en el siguiente inicio de sesión.<br><br>" +
                         "Le deseamos un feliz día.</font></td></tr>" +
                         "<tr><td style='padding: 15px;'><center><img alt='' src='cid:logo.jpg' /></center>" +
                         "<td style='padding: 15px;'><br><center><img alt='' src='cid:desarrollo.png' /></center>" + 
                         "</tr></table></center>";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.High;
            string file = HttpContext.Current.Server.MapPath("Images\\siaf.png") ;
            Attachment data1 = new Attachment(file, MediaTypeNames.Application.Octet);
            file = HttpContext.Current.Server.MapPath("Images\\logo.jpg");
            Attachment data2 = new Attachment(file, MediaTypeNames.Application.Octet);
            file = HttpContext.Current.Server.MapPath("Images\\desarrollo.png");
            Attachment data3 = new Attachment(file, MediaTypeNames.Application.Octet);
            email.Attachments.Add(data1);
            email.Attachments.Add(data2);
            email.Attachments.Add(data3);
            //EnviarCorreo.Enviar(email, TipoCorreo.ReseteoDeClave);
        }

        private string LlenarBody(string password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Mail/ReseteoClave.htm")))
                body = reader.ReadToEnd();
            body = body.Replace("{Producto}", AboutInfo.Instance.ProductName);
            body = body.Replace("{Clave}", password);
            return body;
        }
    }

    [DomainComponent]
    [ModelDefault("Caption", "Registro de usuario")]
    [ImageName("BO_User")]
    public class RegisterUserParameters : LogonActionParametersBase
    {
        [ModelDefault("IsPassword", "True")]
        public string Password { get; set; }
        public string Email { get; set; }
        public override void ExecuteBusinessLogic(IObjectSpace objectSpace)
        {
            Usuario usuario = objectSpace.FindObject<Usuario>(new BinaryOperator("UserName", Usuario));
            if (usuario != null)
                throw new ArgumentException("Las credenciales digitadas ya existen en el sistema");
            else
                SIAFModule.CreateSecuritySystemUser(objectSpace, Usuario, Email, Password, false);
        }
    }
}
