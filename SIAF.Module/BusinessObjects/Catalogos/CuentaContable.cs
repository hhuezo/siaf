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
    [DefaultClassOptions]
    [ImageName("Action_Grand_Totals_Row")]
    [XafDefaultProperty("Codigo")]
    public class CuentaContable : Entidad100
    {
        public CuentaContable(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private string _Codigo;

        [XafDisplayName("C�digo")]
        public string Codigo
        {
            get { return _Codigo; }
            set { SetPropertyValue("Codigo", ref _Codigo, value); }
        }
    }
}
