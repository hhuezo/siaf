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

namespace SIAF.Module.BusinessObjects
{
    [NonPersistent]
    [XafDefaultProperty("Descripcion")]
    public class Entidad256 : Entidad
    {
        public Entidad256(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private string _Descripcion;

        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField]
        [XafDisplayName("Descripción")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { SetPropertyValue("Descripcion", ref _Descripcion, value); }
        }
    }
}
