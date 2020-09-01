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
    public class ParametroPorEmpleado : ReportParametersObjectBase
    {
        public ParametroPorEmpleado(IObjectSpaceCreator provider) : base(provider) {
        
            // para cargar datos default
         //BinaryOperator BinaryUnidad = new BinaryOperator("Descripcion", "UFI", BinaryOperatorType.Equal);
         //   Unidad u = this.ObjectSpace.FindObject<Unidad>(BinaryUnidad);
         //   if(!ReferenceEquals(u,null))
         //   {
         //       Unidad = u;
         //   }
        
        
        }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));           

        }
        public override CriteriaOperator GetCriteria()
        {
            //CriteriaOperator criteria = new BinaryOperator("MyPropertyName", "MyValue");
            //return criteria;
            CriteriaOperator criteriaFinal = null;

            CriteriaOperator criteriaUnidad = new BinaryOperator("Unidad", Unidad);
            CriteriaOperator criteriaAmbiente = new BinaryOperator("Ambiente", Ambiente);
            CriteriaOperator criteriaEmpleado = new BinaryOperator("Empleado", Empleado);
 
            if (!ReferenceEquals(Empleado, null))
            {
                criteriaFinal = CriteriaOperator.And(criteriaEmpleado);
            }


            criteriaFinal = CriteriaOperator.And(criteriaFinal);
            return criteriaFinal;
        }
        public override SortProperty[] GetSorting()
        {
            //SortProperty[] sorting = { new SortProperty("MyPropertyName", SortingDirection.Descending) };
            //return sorting;  
            SortProperty[] sorting = { new SortProperty("Empleado", SortingDirection.Descending) };
            return sorting;

        }
    

        [ImmediatePostData()]
        public Unidad Unidad { get; set; }

        [ImmediatePostData()]
        [DataSourceProperty("Unidad.Ambiente")]
        public Ambiente Ambiente { get; set; }

        [DataSourceProperty("Ambiente.Empleado")]
        public Empleado Empleado { get; set; }

    }
}
