using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace SIAF.Module.BusinessObjects
{
    public class RegistrarReseteo
    {
        public static void Registrar(Guid Usuario)
        {
          /*  Session session = XpoHelper.GetNewSession();
            HistorialDeReseteo historial = new HistorialDeReseteo(session);
            historial.UsuarioCreador = Usuario;
            historial.FechaDeIngreso = Hora.ObtenerHora();
            //historial.Equipo = System.Environment.MachineName;
            //historial.IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            historial.Save();*/
        }

    }
}
