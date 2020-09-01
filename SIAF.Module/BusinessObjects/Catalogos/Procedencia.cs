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
    [ImageName("BO_Invoice")]
    public class Procedencia : Entidad100
    {
        public Procedencia(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association("Procedencia-Fuente")]
        public XPCollection<Fuente> Fuente
        {
            get { return GetCollection<Fuente>("Fuente"); }
        }
    }
}
