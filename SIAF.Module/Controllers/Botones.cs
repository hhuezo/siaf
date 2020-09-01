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
using DevExpress.ExpressApp.Web.SystemModule;
using SIAF.Module.BusinessObjects;

namespace SIAF.Module.Controllers
{
    public partial class Botones : ViewController
    {
        public Botones()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetObjectType = typeof(Entidad);
            TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            ListViewController controller = Frame.GetController<ListViewController>();
            if(controller != null)
                controller.EditAction.Caption = "Editar";

            ExportController controller2 = Frame.GetController<ExportController>();
            if (controller2 != null) 
                controller2.ExportAction.Caption = "Exportar";

        }
        protected override void OnViewControlsCreated()
        { base.OnViewControlsCreated(); }

        protected override void OnDeactivated()
        { base.OnDeactivated(); }

    
    }
}
