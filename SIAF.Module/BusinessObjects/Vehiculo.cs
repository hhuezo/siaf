using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Vendor")]
    public class Vehiculo : Activo
    {
        public Vehiculo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private string _Equipo;
        private Traccion _Traccion;
        private Combustible _Combustible;
        private int _AnioFabricacion;
        private string _NumeroChasis;
        private string _Motor;
        private Color _Color;
        private string _Placa;
        private string _Modelo;
        private Marca _Marca;

        public Marca Marca
        {
            get { return _Marca; }
            set { SetPropertyValue("Marca", ref _Marca, value); }
        }

        public string Modelo
        {
            get { return _Modelo; }
            set { SetPropertyValue("Modelo", ref _Modelo, value); }
        }

        public string Placa
        {
            get { return _Placa; }
            set { SetPropertyValue("Placa", ref _Placa, value); }
        }


        public string Equipo
        {
            get
            {
                return _Equipo;
            }
            set
            {
                SetPropertyValue("Equipo", ref _Equipo, value);
            }
        }
        
        public Color Color
        {
            get { return _Color; }
            set { SetPropertyValue("Color", ref _Color, value); }
        }

        public string Motor
        {
            get { return _Motor; }
            set { SetPropertyValue("Motor", ref _Motor, value); }
        }

        public string NumeroChasis
        {
            get { return _NumeroChasis; }
            set { SetPropertyValue("NumeroChasis", ref _NumeroChasis, value); }
        }

        [XafDisplayName("Año de fabricación")]
        public int AnioFabricacion
        {
            get { return _AnioFabricacion; }
            set { SetPropertyValue("AnioFabricacion", ref _AnioFabricacion, value); }
        }

        public Combustible Combustible
        {
            get { return _Combustible; }
            set { SetPropertyValue("Combustible", ref _Combustible, value); }
        }

        [XafDisplayName("Tracción")]
        public Traccion Traccion
        {
            get { return _Traccion; }
            set { SetPropertyValue("Traccion", ref _Traccion, value); }
        }



        public void CalcularDepreciacion()
        {
            if (!ReferenceEquals(Establecimiento, null))
            {

                //calculo para determinar si ya ha pasado la vida util del activo
                DateTime FechaFinal = FechaDeAdquisicion.AddYears(VidaUtil);
                TimeSpan ValidarFecha = (FechaFinal - Establecimiento.FechaDepresiacion);
                int differenceInDays = ValidarFecha.Days;
                if (differenceInDays < 0)
                {
                    if (ValorInicial >= 600)
                    {
                        ValorResidual = ValorInicial * 0.1;
                        ValorADepreciar = ValorInicial * 0.9;
                        DepreciacionAnual = 0;
                        DepreciacionMensual = 0;
                        DepreciacionDiaria = 0;
                        DepreciacionAcumulada = ValorInicial * 0.9;
                        ValorActual = ValorInicial * 0.1;
                        DepreciadoTotalmente = true;
                    }
                    else
                    {
                        ValorResidual = 0;
                        ValorADepreciar = 0;
                        DepreciacionAnual = 0;
                        DepreciacionMensual = 0;
                        DepreciacionDiaria = 0;
                        DepreciacionAcumulada = 0;
                        ValorActual = ValorInicial;
                    }
                }
                else
                //esta dentro de su vida util
                {
                    DateTime fec = FechaDeAdquisicion;
                    int years = (Establecimiento.FechaDepresiacion.Year - FechaDeAdquisicion.Year);


                    if (years == 0)
                    {
                        if (ValorInicial >= 600 && Depresiable == siaf.Module.BusinessObjects.Enums.Depresiable.Depresiables)
                        {

                            ValorResidual = ValorInicial * 0.1;
                            double V_ValorADepreciar = ValorInicial * 0.9;
                            ValorADepreciar = V_ValorADepreciar;

                            //Depresiacion Anual
                            double DepreAnual = V_ValorADepreciar / VidaUtil;
                            DepreciacionAnual = DepreAnual;

                            //ultimo dia del año
                            DateTime firstDayAd = new DateTime(Establecimiento.FechaDepresiacion.Year, 01, 01);
                            DateTime lastDayAd = new DateTime(Establecimiento.FechaDepresiacion.Year, 12, 31);

                            //calculo de la depreciacion diaria
                            TimeSpan DiasYear = (lastDayAd - firstDayAd);
                            int DaysYearAd = (DiasYear.Days) + 1;
                            double DepreDiaria = DepreAnual / DaysYearAd;
                            int DiasMes = System.DateTime.DaysInMonth(Establecimiento.FechaDepresiacion.Year, Establecimiento.FechaDepresiacion.Month);

                            DepreciacionMensual = DiasMes * DepreDiaria;
                            DepreciacionDiaria = DepreDiaria;

                            //calcular dias del año
                            firstDayAd = new DateTime(FechaDeAdquisicion.Year, FechaDeAdquisicion.Month, FechaDeAdquisicion.Day);
                            lastDayAd = new DateTime(Establecimiento.FechaDepresiacion.Year, Establecimiento.FechaDepresiacion.Month, Establecimiento.FechaDepresiacion.Day);
                            DiasYear = (lastDayAd - firstDayAd);
                            DaysYearAd = (DiasYear.Days) + 1;
                            DepreciacionAcumulada = DaysYearAd * DepreDiaria;
                            ValorActual = ValorInicial - DepreciacionAcumulada;
                        }
                        else
                        {
                            ValorResidual = 0;
                            ValorADepreciar = 0;
                            DepreciacionAnual = 0;
                            DepreciacionMensual = 0;
                            DepreciacionDiaria = 0;
                            DepreciacionAcumulada = 0;
                            ValorActual = ValorInicial;
                        }

                    }
                    else if (years > 0)
                    {
                        if (ValorInicial >= 600 && Depresiable == siaf.Module.BusinessObjects.Enums.Depresiable.Depresiables)
                        {
                            ValorResidual = ValorInicial * 0.1;
                            double V_ValorADepreciar = ValorInicial * 0.9;
                            ValorADepreciar = V_ValorADepreciar;

                            //Depresiacion Anual
                            double DepreAnual = V_ValorADepreciar / VidaUtil;
                            DepreciacionAnual = DepreAnual;

                            //calculo depresicion del año de adquisicion
                            DateTime firstDayAd = new DateTime(FechaDeAdquisicion.Year, 01, 01);
                            DateTime lastDayAd = new DateTime(FechaDeAdquisicion.Year, 12, 31);

                            //calculo de la depreciacion diaria del año de adquisicion
                            TimeSpan DiasYear = (lastDayAd - firstDayAd);
                            int DaysYearAd = (DiasYear.Days) + 1;
                            double DepreDiaria = DepreAnual / DaysYearAd;

                            //calculo de dias a depreciar del año de adquisicion
                            TimeSpan DiasDepreciarAd = (lastDayAd - FechaDeAdquisicion);
                            int DiaDepreciarAd = (DiasDepreciarAd.Days) + 1;
                            double DepreciacionAdd = DepreDiaria * DiaDepreciarAd;


                            //depreciacion de años completos
                            double DeprecYearCompleto = DepreAnual * (years - 1);


                            //DepreciacionAdd del año actual

                            //calculo de la depreciacion diaria
                            DateTime firstDayYearActual = new DateTime(Establecimiento.FechaDepresiacion.Year, 01, 01);
                            DateTime lastDayYearActual = new DateTime(Establecimiento.FechaDepresiacion.Year, 12, 31);
                            TimeSpan DiaYearActual = (lastDayYearActual - firstDayYearActual);
                            int DiasYearActual = (DiaYearActual.Days) + 1;
                            double DepreciacionDiariaLastYear = DepreAnual / DiasYearActual;

                            //calculo depresiacion mensual
                            DateTime PrimerDiaMes = new DateTime(Establecimiento.FechaDepresiacion.Year, Establecimiento.FechaDepresiacion.Month, 01);
                            DateTime UltimoDiaMes = PrimerDiaMes.AddMonths(1);
                            TimeSpan DiaMes = (UltimoDiaMes - PrimerDiaMes);
                            int DiasMes = (DiaMes.Days);
                            DepreciacionMensual = DiasMes * DepreciacionDiariaLastYear;

                            //calculo de la depreciacion del año actual
                            TimeSpan TotalDiasYearActual = (Establecimiento.FechaDepresiacion - firstDayYearActual);
                            int TotalDiaYearActual = (TotalDiasYearActual.Days) + 1;
                            double DepreciaciononLastYear = DepreciacionDiariaLastYear * TotalDiaYearActual;

                            ValorActual = ValorInicial - (DepreciacionAdd + DeprecYearCompleto + DepreciaciononLastYear);
                            DepreciacionAcumulada = DepreciacionAdd + DeprecYearCompleto + DepreciaciononLastYear;
                            DepreciacionDiaria = DepreciacionDiariaLastYear;
                        }
                        else
                        {
                            ValorResidual = 0;
                            ValorADepreciar = 0;
                            DepreciacionAnual = 0;
                            DepreciacionMensual = 0;
                            DepreciacionDiaria = 0;
                            DepreciacionAcumulada = 0;
                            ValorActual = ValorInicial;
                        }
                    }

                }

            }
        }

        protected override void OnSaving()
        {
            CalcularDepreciacion();
        }
    }
}
