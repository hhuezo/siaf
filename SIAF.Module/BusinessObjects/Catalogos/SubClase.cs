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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Action_Change_State")]
    [RuleCombinationOfPropertiesIsUnique("KeyUniqueSubClase", DefaultContexts.Save, "Clase.Grupo, Clase, Codigo")]
    public class SubClase : Entidad100
    {
        public SubClase(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Clase _Clase;
        private string _Codigo;
        private string _CodAnterior2;

        [XafDisplayName("Código")]
        public string Codigo
        {
            get { return _Codigo; }
            set { SetPropertyValue("Codigo", ref _Codigo, value); }
        }
        
        [Association("Clase-SubClase")]
        public Clase Clase
        {
            get { return _Clase; }
            set { SetPropertyValue("Clase", ref _Clase, value); }
        }
        
        [Appearance("CodAnterior2", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string CodAnterior2
        {
            get { return _CodAnterior2; }
            set { SetPropertyValue("CodAnterior2", ref _CodAnterior2, value); }
        }

        private string _CodAnterior3;

        [Appearance("CodAnterior3", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string CodAnterior3
        {
            get { return _CodAnterior3; }
            set { SetPropertyValue("CodAnterior3", ref _CodAnterior2, value); }
        }
    }
}
