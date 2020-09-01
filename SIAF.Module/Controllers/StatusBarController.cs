using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace SIAF.Module.Controllers
{
    public partial class StatusBarController : ViewController
    {
        private WindowTemplateController windowTemplateController;
        private string NombreBase = "";
        public StatusBarController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewNesting = Nesting.Root;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            windowTemplateController = Frame.GetController<WindowTemplateController>();
            windowTemplateController.CustomizeWindowStatusMessages +=
                windowTemplateController_CustomizeWindowStatusMessages;
            CargarNombreBase();            
        }
        protected override void OnViewControlsCreated()
        {base.OnViewControlsCreated();}

        protected override void OnDeactivated()
        {
            windowTemplateController.CustomizeWindowStatusMessages -= windowTemplateController_CustomizeWindowStatusMessages;
            base.OnDeactivated();
        }
        void windowTemplateController_CustomizeWindowStatusMessages(object sender, CustomizeWindowStatusMessagesEventArgs e)
        {
            e.StatusMessages.Add(NombreBase);
        }
        private void CargarNombreBase()
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
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
                        NombreBase = "Base: " + connectionArray[i].Substring(connectionArray[i].IndexOf("=") + 1);
                    }
                }
                i++;
            }
            windowTemplateController.UpdateWindowStatusMessage();
        }
    }
}
