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
    [ImageName("BO_StateMachine")]
    [RuleCombinationOfPropertiesIsUnique("KeyUniqueClase", DefaultContexts.Save, "Grupo, Codigo")]
    public class Clase : Entidad100
    {
        public Clase(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private int _VidaUtil;
        private Grupo _Grupo;
        private string _Codigo;
        private string _CodAnterior2;

        [XafDisplayName("Código")]
        public string Codigo
        {
            get { return _Codigo; }
            set { SetPropertyValue("Codigo", ref _Codigo, value); }
        }
        
        [Association("Grupo-Clase")]
        public Grupo Grupo
        {
            get { return _Grupo; }
            set { SetPropertyValue("Grupo", ref _Grupo, value); }
        }

        [XafDisplayName("Vida útil")]
        public int VidaUtil
        {
            get { return _VidaUtil; }
            set { SetPropertyValue("VidaUtil", ref _VidaUtil, value); }
        }
        
        [Association("Clase-SubClase")]
        public XPCollection<SubClase> SubClase
        {
            get { return GetCollection<SubClase>("SubClase"); }
        }

        [Appearance("CodAnterior2", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string CodAnterior2
        {
            get { return _CodAnterior2; }
            set { SetPropertyValue("CodAnterior2", ref _CodAnterior2, value); }
        }
    }
}