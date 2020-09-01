using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Validation;
using SIAF.Module.BusinessObjects.Reportes.temporales;
using System.Collections.Generic;

namespace SIAF.Module.BusinessObjects.Reportes
{
    [DomainComponent]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/#Xaf/CustomDocument3594.
    public class ParametroAmortizacionIntangibles : ReportParametersObjectBase
    {
        public ParametroAmortizacionIntangibles(IObjectSpaceCreator provider) : base(provider) { }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));
        }
        public override CriteriaOperator GetCriteria()
        {

            DeleteData();
            if (!ReferenceEquals(Fecha, null))
            {
                GenerateData();
            }


            CriteriaOperator criteria = null;
            return criteria;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("Oid", SortingDirection.Descending) };
            return sorting;
        }

        [RuleRequiredField]
        public DateTime Fecha { get; set; }



        public static decimal MonthDifference(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

        }

        private void GenerateData()
        {
            CriteriaOperator criteriaFinal = null;
            BinaryOperator BinaryNulo = new BinaryOperator("CodigoDeActivo", "", BinaryOperatorType.NotEqual);
            BinaryOperator BinaryFecha = new BinaryOperator("FechaDeAdquisicion", Fecha, BinaryOperatorType.LessOrEqual);
            criteriaFinal = CriteriaOperator.And(BinaryNulo, BinaryFecha);

            ICollection<Software> ListadoSoftware = ObjectSpace.GetObjects <Software> (criteriaFinal);

            decimal meses = 0;
            int mesesTotales = 0;

            if (!ReferenceEquals(ListadoSoftware, null))
            {



                foreach (Software ObjSoftware in ListadoSoftware)
                {
                    if (ObjSoftware.FechaDeAdquisicion.AddYears(ObjSoftware.VidaUtil) > Fecha)
                    {


                        if (ObjSoftware.Licencia == true)
                        {
                            meses = MonthDifference(Fecha, ObjSoftware.FechaDeAdquisicion) + 1;
                            mesesTotales = ObjSoftware.VidaUtil * 12;

                            if (meses <= mesesTotales)
                            {

                                ReporteAmortizacion Objreporte = this.ObjectSpace.CreateObject<ReporteAmortizacion>();

                                Objreporte.Software = ObjSoftware;
                                Objreporte.Fecha = Fecha;
                                decimal Saldo = 0;
                                decimal SaldoPendiente = 0;

                                double ValorMes = Math.Round(ObjSoftware.DepreciacionMensual, 2);

                                if (meses < mesesTotales)
                                {
                                    Saldo = (decimal)ValorMes * meses;
                                    SaldoPendiente = (decimal)ObjSoftware.ValorInicial - Saldo;

                                    Objreporte.AmortizacionMensual = (decimal)ValorMes;
                                    Objreporte.Saldo = Saldo;
                                    Objreporte.SaldoPendiente = SaldoPendiente;
                                }
                                else if (meses == mesesTotales)
                                {
                                    Saldo = (decimal)ObjSoftware.ValorInicial;
                                    SaldoPendiente = 0;


                                    Objreporte.Saldo = Saldo;
                                    Objreporte.SaldoPendiente = SaldoPendiente;

                                    Objreporte.AmortizacionMensual = (decimal)ObjSoftware.ValorInicial - ((decimal)ValorMes * (meses - 1));
                                }

                            }
                        }
                        else
                        {
                            BinaryOperator BinaryAxo = new BinaryOperator("Axo", Fecha.Year);
                            BinaryOperator BinaryMes = new BinaryOperator("Mes", Fecha.Month);
                            BinaryOperator BinaryObj = new BinaryOperator("Software", ObjSoftware);

                            CriteriaOperator criteriaFinalObj = CriteriaOperator.And(BinaryAxo, BinaryMes, BinaryObj);

                            if (!ReferenceEquals(criteriaFinalObj, null))
                            {
                                Amortizacion ObjAmortizacion = this.ObjectSpace.FindObject<Amortizacion>(criteriaFinalObj);

                                ReporteAmortizacion Objreporte = this.ObjectSpace.CreateObject<ReporteAmortizacion>();
                                Objreporte.Software = ObjSoftware;
                                Objreporte.Fecha = Fecha;

                                Objreporte.AmortizacionMensual = ObjAmortizacion.AmortizacionMensual;
                                Objreporte.Saldo = ObjAmortizacion.Saldo;
                                Objreporte.SaldoPendiente = ObjAmortizacion.SaldoPendiente;
                            }


                        }

                    }

                    this.ObjectSpace.CommitChanges(); 
                }

            }

            


        }


        private void DeleteData()
        {
            BinaryOperator Eliminar = new BinaryOperator("Software.CodigoDeActivo", "", BinaryOperatorType.NotEqual);
            bool Seguir = true;
            while (Seguir)
            {
                if (!ReferenceEquals(this.ObjectSpace.FindObject<ReporteAmortizacion>(Eliminar), null))
                {
                    this.ObjectSpace.Delete(this.ObjectSpace.FindObject<ReporteAmortizacion>(Eliminar));
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
