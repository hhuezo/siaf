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
using siaf.Module.BusinessObjects.Enums;

namespace SIAF.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class DepreciacionFecha : ViewController
    {
        private IObjectSpace _ObjectSpaceCore;
        private DetailView _VistaPopup;

        public DepreciacionFecha()
        {
            InitializeComponent();
            RegisterActions(components);
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
        private void DActivo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ParametroFecha FechaSeleccionada = (ParametroFecha)_VistaPopup.CurrentObject;           
            BinaryOperator Depreciar = new BinaryOperator("DepreciadoTotalmente", false);
            BinaryOperator NoDepreciable = new BinaryOperator("Depresiable", Depresiable.Depresiables);
            BinaryOperator Valor = new BinaryOperator("ValorInicial", 600, BinaryOperatorType.GreaterOrEqual);
           
            //para filtrar los estados 1 y 2
            BinaryOperator ProcesoDescargo1 = new BinaryOperator("Estado", Estado.Disponible);
            BinaryOperator ProcesoDescargo2 = new BinaryOperator("Estado", Estado.Asignado);
            CriteriaOperator ProcesoDescargoFinal = CriteriaOperator.Or(ProcesoDescargo1, ProcesoDescargo2);

            CriteriaOperator DepreciarOperator = CriteriaOperator.And(Depreciar, Valor, NoDepreciable, ProcesoDescargoFinal);
            ICollection<Activo> ActivoDepreciar = ObjectSpace.GetObjects<Activo>(DepreciarOperator);
            
            if (!ReferenceEquals(ActivoDepreciar, null))
            {                
                DateTime FechaEvaluar = FechaSeleccionada.FechaDepreciacion;
                foreach (Activo o in ActivoDepreciar)
                {
                    o.Establecimiento.FechaDepresiacion = FechaEvaluar;
                    string CodigoGrupo = o.Grupo.Codigo;
                    int CodigoGrupoInt = Convert.ToInt32(CodigoGrupo);
                    if (CodigoGrupoInt == 611)
                    {
                        double ValorInicial = o.ValorInicial;
                        double ValorResidual = ValorInicial * 0.10;
                        double ValorADepreciar = ValorInicial - ValorResidual;
                        double DepreciacionAnual = ValorADepreciar / o.VidaUtil;

                        o.ValorResidual = ValorResidual;
                        o.ValorADepreciar = ValorADepreciar;
                        o.DepreciacionAnual = DepreciacionAnual;

                        //calculo para determinar si ya ha pasado la vida util del activo
                        DateTime FechaFinal = o.FechaDeAdquisicion.AddYears(o.VidaUtil);
                        TimeSpan ValidarFecha = (FechaFinal - FechaEvaluar);
                        int differenceInDays = ValidarFecha.Days;

                        //if para determinar si ya ha pasado la vida util del activo                       
                        if (differenceInDays >= 0)
                        //esta dentro de su vida util
                        {
                            DateTime fec = o.FechaDeAdquisicion;
                            int years = (FechaEvaluar.Year - o.FechaDeAdquisicion.Year);

                            //calculo de total depreciacion anuales
                            double TotalYears = DepreciacionAnual * years;
                            
                            if (years == 0)
                            {
                                //ultimo dia del año
                                DateTime firstDayAd = new DateTime(o.FechaDeAdquisicion.Year, 01, 01);
                                DateTime lastDayAd = new DateTime(o.FechaDeAdquisicion.Year, 12, 31);

                                //calculo de la depreciacion diaria
                                TimeSpan DiasYear = (lastDayAd - firstDayAd);
                                int DaysYearAd = (DiasYear.Days) + 1;
                                double DepreciacionDiaria = DepreciacionAnual / DaysYearAd;

                                //calculo de dias a depreciar
                                TimeSpan DiasDepreciarAd = (FechaEvaluar - o.FechaDeAdquisicion);
                                int DiaDepreciarAd = (DiasDepreciarAd.Days) + 1;
                                o.ValorActual = o.ValorInicial - (DepreciacionDiaria * DiaDepreciarAd);
                                o.DepreciacionDiaria = DepreciacionDiaria;


                                DateTime PrimerDiaMes = new DateTime(FechaEvaluar.Year, FechaEvaluar.Month, 01);
                                DateTime UltimoDiaMes = PrimerDiaMes.AddMonths(1);
                                TimeSpan DiaMes = (UltimoDiaMes - PrimerDiaMes);
                                int DiasMes = (DiaMes.Days);
                                o.DepreciacionMensual = DiasMes * DepreciacionDiaria;

                            }
                            else if (years > 0)
                            {

                                //calculo depresicion del año de adquisicion
                                DateTime firstDayAd = new DateTime(o.FechaDeAdquisicion.Year, 01, 01);
                                DateTime lastDayAd = new DateTime(o.FechaDeAdquisicion.Year, 12, 31);

                                //calculo de la depreciacion diaria del año de adquisicion
                                TimeSpan DiasYear = (lastDayAd - firstDayAd);
                                int DaysYearAd = (DiasYear.Days) + 1;
                                double DepreciacionDiaria = DepreciacionAnual / DaysYearAd;

                                //calculo de dias a depreciar del año de adquisicion
                                TimeSpan DiasDepreciarAd = (lastDayAd - o.FechaDeAdquisicion);
                                int DiaDepreciarAd = (DiasDepreciarAd.Days) + 1;
                                double DepreciacionAdd = DepreciacionDiaria * DiaDepreciarAd;

                                //depreciacion de años completos
                                double DeprecYearCompleto = DepreciacionAnual * (years - 1);

                                

                                //DepreciacionAdd del año actual

                                //calculo de la depreciacion diaria
                                DateTime firstDayYearActual = new DateTime(FechaEvaluar.Year, 01, 01);
                                DateTime lastDayYearActual = new DateTime(FechaEvaluar.Year, 12, 31);
                                TimeSpan DiaYearActual = (lastDayYearActual - firstDayYearActual);
                                int DiasYearActual = (DiaYearActual.Days) + 1;
                                double DepreciacionDiariaLastYear = o.DepreciacionAnual / DiasYearActual;


                                //calculo de dias del ultimo mes evaluado
                                //DateTime FechaInterna = o.FechaDeAdquisicion.AddMonths(i);

                                DateTime PrimerDiaMes = new DateTime(FechaEvaluar.Year, FechaEvaluar.Month, 01);
                                DateTime UltimoDiaMes = PrimerDiaMes.AddMonths(1);
                                TimeSpan DiaMes = (UltimoDiaMes - PrimerDiaMes);
                                int DiasMes = (DiaMes.Days) ;
                                o.DepreciacionMensual = DiasMes * DepreciacionDiariaLastYear;
                                

                                //calculo de la depreciacion del año actual
                                TimeSpan TotalDiasYearActual = (FechaEvaluar - firstDayYearActual);
                                int TotalDiaYearActual = (TotalDiasYearActual.Days)+1;
                                double DepreciaciononLastYear = DepreciacionDiariaLastYear * TotalDiaYearActual;

                                o.ValorActual = ValorInicial - (DepreciacionAdd + DeprecYearCompleto + DepreciaciononLastYear);
                                o.DepreciacionAcumulada = DepreciacionAdd + DeprecYearCompleto + DepreciaciononLastYear;
                                o.DepreciacionDiaria = DepreciacionDiariaLastYear;
                                                                
                            }

                            
                        }
                        else
                        {
                            o.ValorActual = o.ValorResidual;
                            o.DepreciadoTotalmente = true;
                            o.DepreciacionAnual = 0;
                            o.DepreciacionDiaria = 0;
                            o.DepreciacionMensual = 0;
                        }

                        BinaryOperator DepreciarTrue = new BinaryOperator("DepreciadoTotalmente", true);
                        CriteriaOperator DepreciarTOperator = CriteriaOperator.And(DepreciarTrue);
                        ICollection<Activo> ActivoDepreciarT = ObjectSpace.GetObjects<Activo>(DepreciarTOperator);
                        if (!ReferenceEquals(ActivoDepreciarT, null))
                        {
                            foreach (Activo a in ActivoDepreciarT)
                            {
                                a.ValorActual = o.ValorResidual;
                                a.DepreciacionAnual = 0;
                                a.DepreciacionDiaria = 0;
                                a.DepreciacionMensual = 0;
                            }
                        }

                    }
                    else if (CodigoGrupoInt == 614)
                    {
                        int NumMeses = o.VidaUtil * 12;
                        double DepMensual = o.ValorInicial / NumMeses;
                        o.DepreciacionAnual = DepMensual * 12;
                        o.ValorADepreciar = o.ValorInicial;

                        //calculo de meses transcurridos
                        bool seguir = true;
                        int MesesAEvaluar = 0;
                        int i = 1;
                        while (seguir)
                        {
                            DateTime FechaInterna = o.FechaDeAdquisicion.AddMonths(i);

                            if (FechaInterna > FechaEvaluar)
                            {
                                seguir = false;
                            }
                            i++;
                            MesesAEvaluar++;
                        }

                        double TotalDepreciacion = (MesesAEvaluar - 1) * DepMensual;
                        o.DepreciacionAcumulada = TotalDepreciacion;
                        o.ValorActual = o.ValorInicial - TotalDepreciacion;
                      
                    }
                }

            }
            if (this.View.ObjectSpace.IsModified)
            {
                this.View.ObjectSpace.CommitChanges();
            }
        }

        private void DActivo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _ObjectSpaceCore = this.Application.CreateObjectSpace();
            _VistaPopup = this.Application.CreateDetailView(_ObjectSpaceCore, _ObjectSpaceCore.CreateObject<ParametroFecha>());
            //para ver el parametro en web
            _VistaPopup.ViewEditMode = ViewEditMode.Edit;
            //end
            e.View = _VistaPopup;
        }


     
    }
}
