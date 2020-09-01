using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Configuration;
using SIAF.Module.BusinessObjects.Seguridad;

namespace SIAF.Module.Controllers
{
    public partial class About : ViewController
    {
        private string NombreBase = "";
        public About()
        {
            InitializeComponent();
            //CargarNombreBase();
            NombreBase = Conexion.ObtenerServidor() + "\\" + Conexion.ObtenerBase();
            AboutInfo.Instance.ProductName = "Sistema SIAF";
            AboutInfo.Instance.Version = "2.00";
            AboutInfo.Instance.Description = NombreBase;
        }
        protected override void OnActivated()
        { base.OnActivated(); }

        protected override void OnViewControlsCreated()
        { base.OnViewControlsCreated(); }

        protected override void OnDeactivated()
        { base.OnDeactivated(); }

        private void CargarNombreBase()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string[] connectionArray = connection.Split(';');
            bool next = true;
            int i = 0;
            int max = connectionArray.Length;
            while (next)
            {
                if (i < max)
                {
                    if (connectionArray[i].IndexOf("Data Source") > -1)
                    {
                        next = false;
                        NombreBase = connectionArray[i].Substring(connectionArray[i].IndexOf("=") + 1);
                    }
                }
                i++;
            }
        }
    }
}
