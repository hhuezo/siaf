using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Validation;

namespace SIAF.Module.BusinessObjects.Reportes
{
    [DomainComponent]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/#Xaf/CustomDocument3594.
    public class ParametroDepreciacionMensual : ReportParametersObjectBase
    {
        public ParametroDepreciacionMensual(IObjectSpaceCreator provider) : base(provider) { }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));
        }
        public override CriteriaOperator GetCriteria()
        {

           

            CriteriaOperator criteriaFinal = null;

            DeleteData();

            if(!ReferenceEquals(Vehiculo,null))
            GenerateData(Vehiculo.FechaDeAdquisicion, Fecha);
            else
                GenerateDataEquipo(Equipo.FechaDeAdquisicion, Fecha);

           /* CriteriaOperator criteriaEquipo = new BinaryOperator("Equipo", Equipo);

            if (ReferenceEquals(Fecha, null))
            {
                Fecha = DateTime.Now;
            }

            CriteriaOperator criteriaFecha = new BinaryOperator("Fecha", Fecha);
           

            

            criteriaFinal = CriteriaOperator.And(criteriaEquipo, criteriaFecha);*/

 
            //BetweenOperator BetweenFechas = new BetweenOperator("FechaDeAdquisicion", FechaDesde, FechaHasta);

            return criteriaFinal;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("Oid", SortingDirection.Descending) };
            return sorting;
        }

        [RuleRequiredField]
        public Equipo Equipo { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public DateTime Fecha { get; set; }



        private void GenerateDataEquipo(DateTime FechaAdq, DateTime FechaFinal)
        {
            decimal Meses = MonthDifference(FechaFinal, FechaAdq) + 2;

            Decimal DepreciacionAcumulada = 0;
            DateTime FechaEvaluar = FechaAdq;

            for (int i = 1; i <= Meses; i++)
            {

                DepreciacionMensual Objreporte = this.ObjectSpace.CreateObject<DepreciacionMensual>();
                Objreporte.Numero = i;
                Objreporte.SubClase = Equipo.SubClase;
                Objreporte.CodigoDeActivo = Equipo.CodigoDeActivo;
                Objreporte.Eliminar = true;
                Objreporte.FechaInicio = FechaAdq;
                Objreporte.FechaFinal = FechaFinal;

                if (i == 1)
                {
                    Objreporte.Fecha = FechaAdq;
                    Objreporte.ValorActual = (decimal)Equipo.ValorInicial;
                    Objreporte.Depreciacion = 0;

                    var startDate = new DateTime(FechaAdq.Year, FechaAdq.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);
                    FechaEvaluar = endDate;
                }


                else if (i == 2)
                {
                    Objreporte.Fecha = FechaEvaluar;

                    TimeSpan tSpan = FechaEvaluar - FechaAdq;
                    int dias = tSpan.Days;


                    Objreporte.ValorActual = (decimal)Equipo.ValorInicial - (decimal)(Equipo.DepreciacionDiaria * dias);
                    Objreporte.Depreciacion = (decimal)Equipo.DepreciacionDiaria * dias;

                    var startDate = new DateTime(FechaAdq.Year, FechaAdq.Month, 1);
                    var endDate = startDate.AddMonths(2).AddDays(-1);
                    FechaEvaluar = endDate;
                }

                else if (i == Meses)
                {

                    Objreporte.Fecha = FechaFinal;

                    var startDate = new DateTime(FechaFinal.Year, FechaFinal.Month, 1);

                    TimeSpan tSpan = FechaFinal - startDate;
                    int dias = tSpan.Days;

                    DepreciacionAcumulada += (decimal)(Equipo.DepreciacionDiaria * dias);
                    Objreporte.Depreciacion = (decimal)(Equipo.DepreciacionDiaria * dias);
                    Objreporte.ValorActual = (decimal)Equipo.ValorInicial - DepreciacionAcumulada;

                }

                else
                {
                    Objreporte.Fecha = FechaEvaluar;

                    Objreporte.Depreciacion = (decimal)Equipo.DepreciacionMensual;
                    DepreciacionAcumulada += (decimal)Equipo.DepreciacionMensual;
                    Objreporte.ValorActual = (decimal)Equipo.ValorInicial - DepreciacionAcumulada;

                    var startDate = new DateTime(FechaEvaluar.Year, FechaEvaluar.Month, 1);
                    var endDate = startDate.AddMonths(2).AddDays(-1);
                    FechaEvaluar = endDate;
                }


            }

            this.ObjectSpace.CommitChanges();


        }




        private void GenerateData(DateTime FechaAdq,DateTime FechaFinal)
        {   
                decimal Meses = MonthDifference(FechaFinal, FechaAdq) + 2;

                Decimal DepreciacionAcumulada = 0;
                DateTime FechaEvaluar = FechaAdq;
                
                for (int i = 1; i <= Meses; i++)
                {

                    DepreciacionMensual Objreporte = this.ObjectSpace.CreateObject<DepreciacionMensual>();
                    Objreporte.Numero = i;
                    Objreporte.SubClase = Vehiculo.SubClase;
                    Objreporte.CodigoDeActivo = Vehiculo.CodigoDeActivo;
                    Objreporte.Eliminar = true;
                    Objreporte.FechaInicio = FechaAdq;
                    Objreporte.FechaFinal = FechaFinal;

                    if (i == 1)
                    {
                        Objreporte.Fecha = FechaAdq;
                        Objreporte.ValorActual = (decimal)Vehiculo.ValorInicial;
                        Objreporte.Depreciacion = 0;

                        var startDate = new DateTime(FechaAdq.Year, FechaAdq.Month, 1);
                        var endDate = startDate.AddMonths(1).AddDays(-1);
                        FechaEvaluar = endDate;
                    }


                   else  if (i == 2)
                    {
                        Objreporte.Fecha = FechaEvaluar;

                        TimeSpan tSpan = FechaEvaluar - FechaAdq;
                        int dias = tSpan.Days;


                        Objreporte.ValorActual = (decimal)Vehiculo.ValorInicial - (decimal)(Vehiculo.DepreciacionDiaria * dias);
                        Objreporte.Depreciacion = (decimal)Vehiculo.DepreciacionDiaria * dias;

                        var startDate = new DateTime(FechaAdq.Year, FechaAdq.Month, 1);
                        var endDate = startDate.AddMonths(2).AddDays(-1);
                        FechaEvaluar = endDate;
                    }

                    else if (i == Meses)
                    {

                        Objreporte.Fecha = FechaFinal;

                        var startDate = new DateTime(FechaFinal.Year, FechaFinal.Month, 1);

                        TimeSpan tSpan = FechaFinal - startDate;
                        int dias = tSpan.Days;

                        DepreciacionAcumulada += (decimal)(Vehiculo.DepreciacionDiaria * dias);
                        Objreporte.Depreciacion = (decimal)(Vehiculo.DepreciacionDiaria * dias);
                        Objreporte.ValorActual = (decimal)Vehiculo.ValorInicial - DepreciacionAcumulada;

                        }

                    else 
                    {
                        Objreporte.Fecha = FechaEvaluar;

                        Objreporte.Depreciacion = (decimal)Vehiculo.DepreciacionMensual;
                        DepreciacionAcumulada += (decimal)Vehiculo.DepreciacionMensual;
                        Objreporte.ValorActual = (decimal)Vehiculo.ValorInicial - DepreciacionAcumulada;

                        var startDate = new DateTime(FechaEvaluar.Year, FechaEvaluar.Month, 1);
                        var endDate = startDate.AddMonths(2).AddDays(-1);
                        FechaEvaluar = endDate;
                    }
                           

                }

                this.ObjectSpace.CommitChanges(); 

                
        }






        private decimal MonthDifference(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

        }

        


        private void DeleteData()
        {
            BinaryOperator Eliminar = new BinaryOperator("Eliminar", true);
            bool Seguir = true;
            while (Seguir)
            {
                if (!ReferenceEquals(this.ObjectSpace.FindObject<DepreciacionMensual>(Eliminar), null))
                {
                    this.ObjectSpace.Delete(this.ObjectSpace.FindObject<DepreciacionMensual>(Eliminar));
                    this.ObjectSpace.CommitChanges();
                }
                else
                {
                    Seguir = false;
                }
            }

        }

    }
}
