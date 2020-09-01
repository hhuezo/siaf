using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using SIAF.Module.BusinessObjects.Seguridad;
using DevExpress.Xpo.DB;
using System.Net;

namespace SIAF.Web {
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
        }
        protected void Application_Start(Object sender, EventArgs e) {
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
#if EASYTEST
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif
        }
        protected void Session_Start(Object sender, EventArgs e) {
            WebApplication.SetInstance(Session, new SIAFAspNetApplication());
            ErrorHandling.CustomSendMailMessage +=new EventHandler<CustomSendMailMessageEventArgs>(ErrorHandling_CustomSendMailMessage);
            if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = Conexion.ObternerConexion();
                //WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //WebApplication.Instance.ConnectionString = MSSqlConnectionProvider.GetConnectionString("SRVDAT\\ISTA", "siaf", "ActivoF1jo", "siaf2");
                //WebApplication.Instance.ConnectionString = OracleConnectionProvider.GetConnectionString("INFOR-09", "c1", "1234");
                //WebApplication.Instance.ConnectionString = MySqlConnectionProvider.GetConnectionString("192.162.3.19", "root", "Pazzword-007", "siaf");
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            WebApplication.Instance.Settings.LogonTemplateContentPath = "LogonTemplateContent1.ascx";
            AuditTrailConfig.Initialize();
            WebApplication.Instance.SetFormattingCulture("es-SV");
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
        }
        protected void Application_BeginRequest(Object sender, EventArgs e) {
        }
        protected void Application_EndRequest(Object sender, EventArgs e) {
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
        }
        protected void Application_Error(Object sender, EventArgs e) {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(Object sender, EventArgs e) {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e) {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
        private void ErrorHandling_CustomSendMailMessage(object sender, CustomSendMailMessageEventArgs e)
        {
            e.Smtp.UseDefaultCredentials = false;
            e.Smtp.Credentials = new NetworkCredential("sistemas@ista.gob.sv", "sistemas-1$TA-3711574371");
            e.Smtp.EnableSsl = false;
            e.Smtp.Port = 465;
        }
    }
}
