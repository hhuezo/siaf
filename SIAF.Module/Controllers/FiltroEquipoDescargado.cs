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
using SIAF.Module.BusinessObjects;
using SIAF.Module.BusinessObjects.Seguridad;

namespace SIAF.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class FiltroEquipoDescargado : ViewController
    {
        public FiltroEquipoDescargado()
        {
            InitializeComponent();
            this.TargetObjectType = typeof(SIAF.Module.BusinessObjects.Equipo);
            this.TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
      
            ListView Vista = (ListView)this.View;
            BinaryOperator CriteriaUsuario = new BinaryOperator("UserName", SecuritySystem.CurrentUserName);
            Usuario Usuario = this.ObjectSpace.FindObject<Usuario>(CriteriaUsuario);

            if (!Usuario.UsuarioAdministrador)
            {
                BinaryOperator Descargados = new BinaryOperator("Estado", Estado.Descargado, BinaryOperatorType.NotEqual);
                Vista.CollectionSource.Criteria["Filtro Descargados"] = Descargados;
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
