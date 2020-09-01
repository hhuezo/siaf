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
    [ImageName("BO_WorkflowDefinition")]
    [ModelDefault("Caption", "Unidad")]
    public class Unidad : Entidad100 
    {
        public Unidad(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association("Unidad-Ambiente")]
        public XPCollection<Ambiente> Ambiente
        {
            get { return GetCollection<Ambiente>("Ambiente"); }
        }
    }

    
}
