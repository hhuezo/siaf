using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DevExpress.Xpo.DB;

namespace SIAF.Module.BusinessObjects.Seguridad
{
    public class Conexion
    {
        public static string ObtenerServidor() { return "SRVDAT01\\ISTA"; }

        public static string ObtenerBase() { return "siaf2"; }

        public static string ObternerConexion() { return MSSqlConnectionProvider.GetConnectionString(ObtenerServidor(), "siaf", "ActivoF1jo", ObtenerBase()); }
    }
}
