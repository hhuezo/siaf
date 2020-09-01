using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using SIAF.Module.BusinessObjects.Seguridad;

namespace SIAF.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            Usuario sampleUser = ObjectSpace.FindObject<Usuario>(new BinaryOperator("UserName", "User"));
            if (sampleUser == null)
            {
                sampleUser = ObjectSpace.CreateObject<Usuario>();
                sampleUser.UserName = "User";
                sampleUser.SetPassword("");
            }

            Rol defaultRole = CreateDefaultRole();
            //sampleUser.Roles.Add(defaultRole);

            Usuario userAdmin = ObjectSpace.FindObject<Usuario>(new BinaryOperator("UserName", "Admin"));
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<Usuario>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            Rol adminRole = ObjectSpace.FindObject<Rol>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<Rol>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
            userAdmin.Roles.Add(adminRole);
            ObjectSpace.CommitChanges();
        }
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private Rol CreateDefaultRole()
        {
            Rol defaultRole = ObjectSpace.FindObject<Rol>(new BinaryOperator("Name", "Default1"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<Rol>();
                defaultRole.Name = "Default1";

                defaultRole.AddObjectAccessPermission<Usuario>("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess);
                //defaultRole.AddMemberAccessPermission<Usuario>("ChangePasswordOnFirstLogon", SecurityOperations.Write);
                defaultRole.AddMemberAccessPermission<Usuario>("StoredPassword", SecurityOperations.Write);
                defaultRole.SetTypePermissionsRecursively<Rol>(SecurityOperations.Read, SecuritySystemModifier.Allow);
                defaultRole.SetTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
                defaultRole.SetTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
            }
            return defaultRole;
        }
    }
}