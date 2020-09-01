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
using DevExpress.ExpressApp.CloneObject;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo;

namespace SIAF.Module.Controllers
{
    public partial class ClonadorController : ObjectViewController 
    {
        public ClonadorController() { InitializeComponent(); }
        protected override void OnActivated()
        {
            base.OnActivated();
            var cloneObjectController = Frame.GetController<CloneObjectViewController>();
            cloneObjectController.CustomCloneObject += cloneObjectController_CustomCloneObject;
        }
        void cloneObjectController_CustomCloneObject(object sender, CustomCloneObjectEventArgs e)
        {
            var cloner = new MiClonador();
            var defaultCloner = new Cloner();
            e.TargetObjectSpace = e.CreateDefaultTargetObjectSpace();
            object objectFromTargetObjectSpace = e.TargetObjectSpace.GetObject(e.SourceObject);
            if ((e.TargetType).Name == "Activo"   || (e.TargetType).Name == "Equipo" || 
                (e.TargetType).Name == "Vehiculo" || (e.TargetType).Name == "Software")
                e.ClonedObject = cloner.CloneTo(objectFromTargetObjectSpace, e.TargetType);
            else
                e.ClonedObject = defaultCloner.CloneTo(objectFromTargetObjectSpace, e.TargetType);
        }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }
    }
    public class MiClonador : Cloner
    {
        public override void CopyMemberValue(XPMemberInfo memberInfo, IXPSimpleObject sourceObject, IXPSimpleObject targetObject)
        {
            if (!((memberInfo.MappingField == "Correlativo") || (memberInfo.MappingField == "CodigoDeActivo")))
                base.CopyMemberValue(memberInfo, sourceObject, targetObject);
        }
    }
}
