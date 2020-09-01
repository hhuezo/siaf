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
    public class ParametroInventarioPorOficina : ReportParametersObjectBase
    {
        public ParametroInventarioPorOficina(IObjectSpaceCreator provider) : base(provider) { }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));
        }
        public override CriteriaOperator GetCriteria()
        {
            CriteriaOperator criteriaAmbiente = new BinaryOperator("Ambiente", Ambiente);
            CriteriaOperator criteriaSubClase = new BinaryOperator("SubClase", SubClase);
            CriteriaOperator criteriaFinal = null;


            if (ReferenceEquals(SubClase, null))
            {
               criteriaFinal = CriteriaOperator.And(criteriaAmbiente);
            }
            else
            {
                criteriaFinal = CriteriaOperator.And(criteriaAmbiente, criteriaSubClase);
            }

            return criteriaFinal;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("CodigoDeActivo", SortingDirection.Descending) };
            return sorting;
        }

        [DataSourceCriteria("Clase.Codigo!=05")]
        public SubClase SubClase { get; set; }

        public Ambiente Ambiente { get; set; }

    }
}
