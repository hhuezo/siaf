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
    [ImageName("Action_ModelDifferences_Export")]
    public class LineaSoftware : Entidad100
    {
        public LineaSoftware(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private AutorSoftware _Autor;

        public AutorSoftware Autor
        {
            get { return _Autor; }
            set { SetPropertyValue("Autor", ref _Autor, value); }
        }

        private string _CodAnterior2;

        [Appearance("CodAnterior2", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string CodAnterior2
        {
            get { return _CodAnterior2; }
            set { SetPropertyValue("CodAnterior2", ref _CodAnterior2, value); }
        }
    }
}
