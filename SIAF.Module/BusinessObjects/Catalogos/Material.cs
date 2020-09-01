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
    [ImageName("Action_CloneMerge_Merge_Object")]
    public class Material : Entidad100
    {
        public Material(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
