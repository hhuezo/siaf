using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.Data;
using SIAF.Module.BusinessObjects.Seguridad;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;

namespace SIAF.Module.BusinessObjects
{
    public class Hora
    {
        public static DateTime ObtenerHora()
        {
            //SqlConnection conn = new SqlConnection();
            //SqlCommand command = new SqlCommand();
            //string conexion = Conexion.ObternerConexion();
            //conn.ConnectionString = conexion.Substring(conexion.IndexOf(";") + 1);
            //command.Connection = conn;
            //command.CommandText = "SELECT CURRENT_TIMESTAMP";
            //conn.Open();
            //DateTime serverDateTime = (DateTime)command.ExecuteScalar();
            //conn.Close();
            //return serverDateTime;

            Session session = XpoHelper.GetNewSession();
            CriteriaOperator funcNow = new FunctionOperator(FunctionOperatorType.Now);
            DateTime serverDateTime = (DateTime)session.Evaluate(typeof(XPObjectType), funcNow, null);
            return serverDateTime;
        }
    }
}
