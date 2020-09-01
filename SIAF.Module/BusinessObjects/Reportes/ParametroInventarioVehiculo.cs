using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;
using SIAF.Module.BusinessObjects.Enums;
using DevExpress.ExpressApp.Model;

namespace SIAF.Module.BusinessObjects.Reportes
{
    [DomainComponent]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/#Xaf/CustomDocument3594.
    public class ParametroInventarioVehiculo : ReportParametersObjectBase
    {
        public ParametroInventarioVehiculo(IObjectSpaceCreator provider) : base(provider) { }
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
            CriteriaOperator criteriaCuentaContable = new BinaryOperator("CuentaContable", CuentaContable);
            CriteriaOperator criteriaFuente = new BinaryOperator("Fuente", Fuente);

            //para filtrar los estados 1 y 2
            BinaryOperator ProcesoDescargo1 = new BinaryOperator("Estado", Estado.Disponible);
            BinaryOperator ProcesoDescargo2 = new BinaryOperator("Estado", Estado.Asignado);
            CriteriaOperator ProcesoDescargoFinal = CriteriaOperator.Or(ProcesoDescargo1, ProcesoDescargo2);

            BetweenOperator BetweenFechas = new BetweenOperator("FechaDeAdquisicion", FechaDesde, FechaHasta);

            string dateTime = "1980/01/01";
            DateTime dt = Convert.ToDateTime(dateTime);
            BetweenOperator BetweenFechas2 = new BetweenOperator("FechaDeAdquisicion", dt, FechaHasta);

            CriteriaOperator criteriaFinal = null;


            if (!ReferenceEquals(Clase, null))
            {
                if (ReferenceEquals(criteriaFinal, null))
                {
                    criteriaFinal = CriteriaOperator.And(criteriaClase);
                }
                else
                {
                    criteriaFinal = CriteriaOperator.And(criteriaClase, criteriaFinal);
                }
            }

            if (!ReferenceEquals(SubClase, null))
            {
                if (!ReferenceEquals(Clase, null))
                {

                    criteriaFinal = CriteriaOperator.And(criteriaSubClase, criteriaFinal);
                }

            }

            if (!ReferenceEquals(Ambiente, null))
            {
                if (ReferenceEquals(criteriaFinal, null))
                {
                    criteriaFinal = CriteriaOperator.And(criteriaAmbiente);
                }
                else
                {
                    criteriaFinal = CriteriaOperator.And(criteriaAmbiente, criteriaFinal);
                }

            }


            if (!ReferenceEquals(Empleado, null))
            {
                if (ReferenceEquals(criteriaFinal, null))
                {
                    criteriaFinal = CriteriaOperator.And(criteriaEmpleado);
                }
                else
                {
                    criteriaFinal = CriteriaOperator.And(criteriaEmpleado, criteriaFinal);
                }

            }


            if (!ReferenceEquals(CuentaContable, null))
            {
                if (ReferenceEquals(criteriaFinal, null))
                {
                    criteriaFinal = CriteriaOperator.And(criteriaCuentaContable);
                }
                else
                {
                    criteriaFinal = CriteriaOperator.And(criteriaCuentaContable, criteriaFinal);
                }

            }


            if (!ReferenceEquals(Fuente, null))
            {
                if (ReferenceEquals(criteriaFinal, null))
                {
                    criteriaFinal = CriteriaOperator.And(criteriaFuente);
                }
                else
                {
                    criteriaFinal = CriteriaOperator.And(criteriaFuente, criteriaFinal);
                }

            }



            int axoDesde = FechaDesde.Year;
            int axoHasta = FechaHasta.Year;

            if (axoDesde > 1 && axoHasta > 1)
            {
                criteriaFinal = CriteriaOperator.And(criteriaFinal, BetweenFechas);
            }
            else if (axoDesde == 1 && axoHasta > 1)
            {
                criteriaFinal = CriteriaOperator.And(criteriaFinal, BetweenFechas2);
            }


            if (Precio == Mayor600.Mayor)
            {
                BinaryOperator Valor = new BinaryOperator("ValorInicial", 600, BinaryOperatorType.GreaterOrEqual);
                criteriaFinal = CriteriaOperator.And(criteriaFinal, Valor);
            }
            else if (Precio == Mayor600.Menor)
            {
                BinaryOperator Valor = new BinaryOperator("ValorInicial", 600, BinaryOperatorType.Less);
                criteriaFinal = CriteriaOperator.And(criteriaFinal, Valor);
            }


            criteriaFinal = CriteriaOperator.And(criteriaFinal, ProcesoDescargoFinal);

            return criteriaFinal;
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

        public CuentaContable CuentaContable { get; set; }

        public Fuente Fuente { get; set; }

        [ModelDefault("Caption", "Fecha Desde")]
        public DateTime FechaDesde { get; set; }
        [ModelDefault("Caption", "Fecha Hasta")]
        public DateTime FechaHasta { get; set; }

        public Mayor600 Precio { get; set; }
    }
}
