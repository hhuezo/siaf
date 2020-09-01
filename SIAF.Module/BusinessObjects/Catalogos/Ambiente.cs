using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using SIAF.Module.BusinessObjects.Seguridad;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Organization")]
    public class Ambiente : Entidad100 
    {
        public Ambiente(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Unidad _Unidad;
        private string _CodAnterior2;
        
        [Association("Usuario-Ambiente")]
        public XPCollection<Usuario> Usuario
        {
            get {return GetCollection<Usuario>("Usuario");}
        }
        
        [Association("Unidad-Ambiente")]
        public Unidad Unidad
        {
            get { return _Unidad; }
            set { SetPropertyValue("Unidad", ref _Unidad, value); }
        }

        [Association("Ambiente-Empleado")]
        public XPCollection<Empleado> Empleado
        {
            get { return GetCollection<Empleado>("Empleado"); }
        }

        [Appearance("CodAnterior2", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string CodAnterior2
        {
            get { return _CodAnterior2; }
            set { SetPropertyValue("CodAnterior2", ref _CodAnterior2, value); }
        }

        
    }
}
