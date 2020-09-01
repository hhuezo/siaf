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
using DevExpress.ExpressApp.Security;

namespace SIAF.Module.Controllers
{
    public partial class User : ViewController
    {
        public User()
        { InitializeComponent(); }

        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<MyDetailsController>().MyDetailsAction.Caption = "Mis datos (" + SecuritySystem.CurrentUserName + ")";
        }

        protected override void OnViewControlsCreated()
        { base.OnViewControlsCreated(); }

        protected override void OnDeactivated()
        { base.OnDeactivated(); }
    }
}