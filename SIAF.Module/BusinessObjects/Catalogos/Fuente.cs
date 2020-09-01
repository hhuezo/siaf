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
    [ImageName("Action_Debug_Step")]
    public class Fuente : Entidad100
    {
        public Fuente(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association("Procedencia-Fuente")]
        public XPCollection<Procedencia> Procedencia
        {
            get { return GetCollection<Procedencia>("Procedencia"); }
        }
    }
}
