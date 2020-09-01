using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Model;
using SIAF.Module.BusinessObjects.Enums;

namespace SIAF.Module.BusinessObjects.Reportes
{
    [DomainComponent]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/#Xaf/CustomDocument3594.
    public class ParametroInventarioVehiculo2 : ReportParametersObjectBase
    {
        public ParametroInventarioVehiculo2(IObjectSpaceCreator provider) : base(provider) { }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));
        }
        public override CriteriaOperator GetCriteria()
        {

            CriteriaOperator criteriaClase = new BinaryOperator("Clase", Clase);
            CriteriaOperator criteriaSubClase = new BinaryOperator("SubClase", SubClase);
            CriteriaOperator criteriaAmbiente = new BinaryOperator("Ambiente", Ambiente);
            CriteriaOperator criteriaEmpleado = new BinaryOperator("Empleado", Empleado);



            CriteriaOperator criteria = new BinaryOperator("MyPropertyName", "MyValue");
            return criteria;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("CodigoDeActivo", SortingDirection.Descending) };
            return sorting;
        }


        [ImmediatePostData()]
        [DataSourceCriteria("Codigo=05")]
        public Clase Clase { get; set; }

        [DataSourceProperty("Clase.SubClase")]
        public SubClase SubClase { get; set; }

        [ImmediatePostData()]
        public Ambiente Ambiente { get; set; }

        [DataSourceProperty("Ambiente.Empleado")]
        public Empleado Empleado { get; set; }

        [ModelDefault("Caption", "Fecha Desde")]
        public DateTime FechaDesde { get; set; }
        [ModelDefault("Caption", "Fecha Hasta")]
        public DateTime FechaHasta { get; set; }

        public Mayor600 Precio { get; set; }

    }
}
