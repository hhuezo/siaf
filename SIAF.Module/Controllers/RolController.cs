using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Xpo;
using SIAF.Module.BusinessObjects.Seguridad;

namespace SIAF.Module.Controllers
{
    public partial class RolController : ViewController
    {
        private IObjectSpace _ObjectSpaceCore;
        private DetailView _VistaPopup;

        public RolController()
        {
            InitializeComponent();
            RegisterActions(components);
            this.TargetObjectType = typeof(Rol);
            this.TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated() { base.OnActivated(); }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }
        
        private void GenerarPerfil_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            RolParametros parametros = (RolParametros)_VistaPopup.CurrentObject;
            
            Rol rol = (Rol)e.CurrentObject;
            rol.SetTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
            rol.SetTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
            
            Session LocalSesion = ((XPObjectSpace)this.ObjectSpace).Session;
            ICollection TodasLasClasesPersistentes = LocalSesion.Dictionary.Classes;
            CrearPermisos(LocalSesion, TodasLasClasesPersistentes, rol, false);
            if (this.ObjectSpace.IsModified)
                this.ObjectSpace.CommitChanges();
        }

        private void CrearPermisos(Session LocalSesion, ICollection TodasLasClasesPersistentes, Rol rol, bool BorrarPermisos)
        {
            RolParametros parametros = (RolParametros)_VistaPopup.CurrentObject;
            if (BorrarPermisos)
            {
                QuitarPermisos(rol);
            }
            ReflectionClassInfo RefInfo = null;
            foreach (System.Object item in TodasLasClasesPersistentes)
            {
                RefInfo = item as ReflectionClassInfo;
                if (ReferenceEquals(null, RefInfo))
                    continue;
                if (!(RefInfo.FullName.StartsWith("DevExpress") || RefInfo.ClassType.Name.StartsWith("RolParametros") || RefInfo.FullName.EndsWith("TextMessage") ||
                     RefInfo.ClassType.Name.StartsWith("Entidad") || RefInfo.FullName.EndsWith("Parameters")))
                {
                    Boolean faltante = true;
                    foreach (SecuritySystemTypePermissionObject p in rol.TypePermissions)
                        if (p.TargetType.Name == RefInfo.ClassType.Name)
                        {
                            faltante = false;
                            break;
                        }
                    if (faltante)
                    {
                        if (RefInfo.ClassType.Name.StartsWith("Usuario"))
                        {
                            rol.AddObjectAccessPermission<Usuario>("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess);
                            rol.AddMemberAccessPermission<Usuario>("ChangePasswordOnFirstLogon", SecurityOperations.Write);
                            rol.AddMemberAccessPermission<Usuario>("StoredPassword", SecurityOperations.Write);
                        }
                        else if (RefInfo.ClassType.Name.StartsWith("Rol"))
                            rol.SetTypePermissionsRecursively<Rol>(SecurityOperations.Read, SecuritySystemModifier.Allow);
                        else
                            if (!parametros.RolVacio)
                                AgregarPermiso(rol, parametros, RefInfo);
                    }
                }
            }
        }
        
        private void AgregarPermiso(Rol rol, RolParametros parametros, ReflectionClassInfo RefInfo)
        {
            SecuritySystemTypePermissionObject PermisoDeObjeto = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
            PermisoDeObjeto.TargetType = RefInfo.ClassType;
            if (parametros.Leer) PermisoDeObjeto.AllowRead = true;
            if (parametros.Escribir) PermisoDeObjeto.AllowWrite = true;
            if (parametros.Crear) PermisoDeObjeto.AllowCreate = true;
            if (parametros.Borrar) PermisoDeObjeto.AllowDelete = true;
            if (parametros.Navegar) PermisoDeObjeto.AllowNavigate = true;
            rol.TypePermissions.Add(PermisoDeObjeto);
        }

        private void QuitarPermisos(Rol rol)
        {
            this.ObjectSpace.Delete(rol.TypePermissions);
            if (this.ObjectSpace.IsModified)
                this.ObjectSpace.CommitChanges();
        }

        private void GenerarPerfil_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _ObjectSpaceCore = this.Application.CreateObjectSpace();
            _VistaPopup = this.Application.CreateDetailView(_ObjectSpaceCore, _ObjectSpaceCore.CreateObject<RolParametros>());
            _VistaPopup.ViewEditMode = ViewEditMode.Edit;
            e.View = _VistaPopup;
        }

    }
}
