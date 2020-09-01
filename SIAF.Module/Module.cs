using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Validation;
using SIAF.Module.BusinessObjects;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.Security;
using Security.Extensions;
using SIAF.Module.BusinessObjects.Seguridad;

namespace SIAF.Module 
{
    public sealed partial class SIAFModule : ModuleBase {
        public static Type SecuritySystemUserType;
        public static CreateSecuritySystemUser CreateSecuritySystemUser;
        public SIAFModule() {
            InitializeComponent();
            this.AdditionalExportedTypes.Add(typeof(WMB.MensajeTexto));
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(ApplicationModulesManager moduleManager)
        {
            base.Setup(moduleManager);
            ValidationRulesRegistrator.RegisterRule(moduleManager,
                typeof(ClaveFuerte),
                typeof(IRuleBaseProperties));
        }
        public override void Setup(XafApplication application)
        {
            application.SetupComplete += new EventHandler<EventArgs>(application_SetupComplete);
            base.Setup(application);
            application.CreateCustomLogonWindowControllers += application_CreateCustomLogonWindowControllers;
        }
        void application_SetupComplete(object sender, EventArgs e)
        {
            ValidationModule module = (ValidationModule)((XafApplication)sender).Modules.FindModule(typeof(ValidationModule));
            if (module != null)
                module.InitializeRuleSet();
        }
        private void application_CreateCustomLogonWindowControllers(object sender, CreateCustomLogonWindowControllersEventArgs e)
        {
            XafApplication app = (XafApplication)sender;
            e.Controllers.Add(app.CreateController<ManageUsersOnLogonController>());
        }
    }
    public delegate IAuthenticationStandardUser CreateSecuritySystemUser(IObjectSpace objectSpace, string userName, string email, string password, bool isAdministrator);
}
