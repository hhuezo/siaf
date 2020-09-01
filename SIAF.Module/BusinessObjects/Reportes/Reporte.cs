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

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Reportes")]
    [ImageName("Navigation_Item_Report")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class Reporte : ReportDataV2 
    {
        public Reporte(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association("Reporte-Rol")]
        public XPCollection<Rol> Rol
        {
            get { return GetCollection<Rol>("Rol"); }
        }
    }
}
