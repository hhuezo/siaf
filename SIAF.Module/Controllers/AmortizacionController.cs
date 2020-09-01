using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using SIAF.Module.BusinessObjects;

namespace SIAF.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class AmortizacionController : ViewController
    {
        public AmortizacionController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void Amortizacion_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Software ObjSoftware = (Software)e.CurrentObject;

            if (ObjSoftware.Licencia != true)
            {
                int meses = ObjSoftware.VidaUtil * 12;
                DateTime FechaInicio = ObjSoftware.FechaDeAdquisicion;

                decimal SumaAmortizacion = 0;
                decimal Saldo = 0;
                decimal SaldoPendiente = (decimal)ObjSoftware.ValorInicial;

                for (int i = 1; i <= meses; i++)
                {
                    Amortizacion ObjAmortizacion = this.ObjectSpace.CreateObject<Amortizacion>();
                    ObjAmortizacion.Software = ObjSoftware;
                    ObjAmortizacion.Numero = i;
                    ObjAmortizacion.Axo = FechaInicio.Year;
                    ObjAmortizacion.Mes = FechaInicio.Month;



                    if (i < meses)
                    {
                        ObjAmortizacion.AmortizacionMensual = (decimal)ObjSoftware.DepreciacionMensual;

                        Saldo += Math.Round((decimal)ObjSoftware.DepreciacionMensual, 2);
                        SaldoPendiente -= Saldo;

                        ObjAmortizacion.Saldo = Saldo;
                        ObjAmortizacion.SaldoPendiente = (decimal)ObjSoftware.ValorInicial - Saldo;
                    }
                    else
                    {
                        ObjAmortizacion.Saldo = (decimal)ObjSoftware.ValorInicial;

                        ObjAmortizacion.SaldoPendiente = 0;

                        ObjAmortizacion.AmortizacionMensual = (decimal)ObjSoftware.ValorInicial - Saldo;
                    }

                    FechaInicio = FechaInicio.AddMonths(1);

                }

               

            }

            ObjSoftware.Guardado = true;

            this.ObjectSpace.CommitChanges();

        }

        private void Costeo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

     

            Costeo ObjCosteo = (Costeo)e.CurrentObject;

            if (ObjCosteo.VidaUtil > 0)
            {
                CriteriaOperator Criteria = CriteriaOperator.Parse("Numero = [<Amortizacion>][Software=?].Max(Numero)", ObjCosteo.Software);
                Amortizacion ObjAmortizacionTemporal = this.ObjectSpace.FindObject<Amortizacion>(Criteria);

                DateTime FechaTemporal = DateTime.Now;

                if (ReferenceEquals(ObjAmortizacionTemporal, null))
                {
                    FechaTemporal = Convert.ToDateTime("01-" + ObjAmortizacionTemporal.Mes.ToString() + '-' + ObjAmortizacionTemporal.Axo.ToString());
                }
                else
                {
                    FechaTemporal = ObjCosteo.Fecha;
                }


                for (int i = 0; i < ObjCosteo.VidaUtil * 12; i++)
                {
                    FechaTemporal = FechaTemporal.AddMonths(1);

                    Amortizacion ObjAmortizacionNuevo = this.ObjectSpace.CreateObject<Amortizacion>();
                    ObjAmortizacionNuevo.Software = ObjCosteo.Software;
                    ObjAmortizacionNuevo.Numero = ObjAmortizacionTemporal.Numero + i;
                    ObjAmortizacionNuevo.Axo = FechaTemporal.Year;
                    ObjAmortizacionNuevo.Mes = FechaTemporal.Month;


                }


            }






            BinaryOperator BinarySoftware = new BinaryOperator("Software", ObjCosteo.Software);
            BinaryOperator BinaryAxo = new BinaryOperator("Axo", ObjCosteo.Fecha.Year);
            BinaryOperator BinaryMes = new BinaryOperator("Mes", ObjCosteo.Fecha.Month);
            CriteriaOperator criteriaFinal = CriteriaOperator.And(BinarySoftware, BinaryAxo, BinaryMes);

            Amortizacion ObjAmortizacion = this.ObjectSpace.FindObject<Amortizacion>(criteriaFinal);


           

            
            if (!ReferenceEquals(ObjAmortizacion, null))
            {


                decimal NuevoSaldo = (decimal)ObjAmortizacion.Software.ValorInicial + ObjCosteo.Monto;
                int meses = ObjAmortizacion.Software.VidaUtil * 12;

                decimal AmortizacionMensual = Math.Round(NuevoSaldo / meses, 2);

                decimal SaldoPendienteAmortizar = (AmortizacionMensual - ObjAmortizacion.AmortizacionMensual) * (ObjAmortizacion.Numero - 1);

                decimal Saldo = ObjAmortizacion.Saldo - ObjAmortizacion.AmortizacionMensual;

                BinaryOperator BinaryMayor = new BinaryOperator("Numero", ObjAmortizacion.Numero, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteriaFinal2 = CriteriaOperator.And(BinaryMayor, BinarySoftware);
                ICollection<Amortizacion> ListadoSAmortizacion = ObjectSpace.GetObjects<Amortizacion>(criteriaFinal2);
                var newListadoSAmortizacion = ListadoSAmortizacion.OrderBy(o => o.Numero);


                if (!ReferenceEquals(newListadoSAmortizacion, null))
                {


                    foreach (Amortizacion ObjAmortizacion2 in newListadoSAmortizacion)
                    {
                        if (ObjAmortizacion2.Numero == ObjAmortizacion.Numero)
                        {
                            ObjAmortizacion2.AmortizacionMensual = SaldoPendienteAmortizar + AmortizacionMensual;
                            ObjAmortizacion2.Saldo = Saldo + SaldoPendienteAmortizar + AmortizacionMensual;

                            Saldo = Saldo + SaldoPendienteAmortizar + AmortizacionMensual;

                            ObjAmortizacion2.SaldoPendiente = NuevoSaldo - Saldo;

                        }
                        else if (ObjAmortizacion2.Numero < ObjAmortizacion.Software.VidaUtil * 12)
                        {
                            Saldo += AmortizacionMensual;

                            ObjAmortizacion2.AmortizacionMensual = AmortizacionMensual;
                            ObjAmortizacion2.Saldo = Saldo;
                            ObjAmortizacion2.SaldoPendiente = NuevoSaldo - Saldo;

                        }
                        else
                        {
                            ObjAmortizacion2.AmortizacionMensual = NuevoSaldo - Saldo;
                            Saldo = NuevoSaldo;
                            ObjAmortizacion2.Saldo = Saldo;
                            ObjAmortizacion2.SaldoPendiente = 0;
                        }

                    }

                    ObjCosteo.Finalizado = true;

                  //  this.ObjectSpace.CommitChanges();

                }
                else
                {





                }

            }
           

        }
    }
}
