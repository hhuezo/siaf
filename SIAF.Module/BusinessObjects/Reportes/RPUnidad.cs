using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;

namespace SIAF.Module.BusinessObjects.Reportes
{
    [DomainComponent]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/#Xaf/CustomDocument3594.
    public class RPUnidad : ReportParametersObjectBase
    {
        public RPUnidad(IObjectSpaceCreator provider) : base(provider) { }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));
        }
        public override CriteriaOperator GetCriteria()
        {
            CriteriaOperator criteria = new
           BinaryOperator("Unidad", Unidad);

            CriteriaOperator criteria2 = null;
            criteria2 = CriteriaOperator.And(criteria);
            return criteria2;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("Unidad", SortingDirection.Descending) };
            return sorting;
        }

        public Unidad Unidad { get; set; }
    }
}
