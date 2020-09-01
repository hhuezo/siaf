using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;
using System.Collections.Generic;
using SIAF.Module.BusinessObjects.Seguridad;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace SIAF.Module.BusinessObjects
{
    [DomainComponent]
    public class MovimientosParametros : ReportParametersObjectBase
    {
        public MovimientosParametros(IObjectSpaceCreator provider) : base(provider)
        {
            //BinaryOperator Criteria = new BinaryOperator("Oid", SecuritySystem.CurrentUserId);
            //this.Usuario = this.ObjectSpace.FindObject<Usuario>(Criteria);
        }
        protected override IObjectSpace CreateObjectSpace() { return objectSpaceCreator.CreateObjectSpace(typeof(object)); }

        public override CriteriaOperator GetCriteria()
        {
            BetweenOperator Rango = new BetweenOperator("FechaDeIngreso", FechaInicial, FechaFinal);
            return Rango;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("FechaDeIngreso", SortingDirection.Ascending) };
            return sorting;
        }
        
        // Fields...
        private DateTime _FechaFinal;
        private DateTime _FechaInicial;

        public DateTime FechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; }
        }
        
        public DateTime FechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; }
        }
        
        //[Appearance("Usuario", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //public Usuario Usuario { get; set; }
    }
}
