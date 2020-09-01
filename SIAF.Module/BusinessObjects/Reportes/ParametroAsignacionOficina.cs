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
    public class ParametroAsignacionOficina : ReportParametersObjectBase
    {
        public ParametroAsignacionOficina(IObjectSpaceCreator provider) : base(provider) { }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));
        }
        public override CriteriaOperator GetCriteria()
        {
            CriteriaOperator criteriaUnidad = new BinaryOperator("Unidad", Unidad);
            CriteriaOperator criteriaAmbiente = new BinaryOperator("Ambiente", Ambiente);
            CriteriaOperator criteriaFinal = null;
            criteriaFinal = CriteriaOperator.And(criteriaAmbiente);

            return criteriaFinal;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("Oid", SortingDirection.Descending) };
            return sorting;
        }

        [ImmediatePostData()]
        public Unidad Unidad { get; set; }

        [DataSourceProperty("Unidad.Ambiente")]
        public Ambiente Ambiente { get; set; }
    }
}
