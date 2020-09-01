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
    public class Establecimiento : Entidad100
    {
        public Establecimiento(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private DateTime _FechaDepresiacion;
        private string _Codigo;

        [XafDisplayName("Código")]
        public string Codigo
        {
            get { return _Codigo; }
            set { SetPropertyValue("Codigo", ref _Codigo, value); }
        }

        //[Appearance("NoVisible en FechaDepresiacion", Visibility = ViewItemVisibility.Hide)]
        public DateTime FechaDepresiacion
        {
            get
            {
                return _FechaDepresiacion;
            }
            set
            {
                SetPropertyValue("FechaDepresiacion", ref _FechaDepresiacion, value);
            }
        }
    }
}
