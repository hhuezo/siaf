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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Action_Export_Chart")]


    [Appearance("Visible", TargetItems = "CodigoEmpleado,Empleado,Ambiente,Unidad,ValorADepreciar,DepreciacionAnual,DepreciacionDiaria,Depresiable,HistorialMovimientos,ValorResidual,DepreciacionAcumulada,ValorActual,Guardado,ValorTotal", Visibility = ViewItemVisibility.Hide, Criteria = "!IsCurrentUserInRole('Administrators')")]
    public class Software : Activo
    {
        public Software(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private decimal _ValorTotal;
        private bool _Guardado;
        private bool _Licencia;
        private string _RegistroCNR;
        private int _Anio;

        private string _NumeroLicencia;
        private string _Version;
        private LineaSoftware _LineaSoftware;
        private AutorSoftware _AutorSoftware;

        [XafDisplayName("Autor")]
        public AutorSoftware AutorSoftware
        {
            get { return _AutorSoftware; }
            set { SetPropertyValue("AutorSoftware", ref _AutorSoftware, value); }
        }

        [XafDisplayName("Linea de software")]
        public LineaSoftware LineaSoftware
        {
            get { return _LineaSoftware; }
            set { SetPropertyValue("LineaSoftware", ref _LineaSoftware, value); }
        }

        [XafDisplayName("Versión")]
        public string Version
        {
            get { return _Version; }
            set { SetPropertyValue("Version", ref _Version, value); }
        }

        public bool Licencia
        {
            get { return _Licencia; }
            set { SetPropertyValue("Licencia", ref _Licencia, value); }
        }

        [XafDisplayName("Número de licencia")]
        public string NumeroLicencia
        {
            get { return _NumeroLicencia; }
            set { SetPropertyValue("NumeroLicencia", ref _NumeroLicencia, value); }
        }

       

        [XafDisplayName("Año")]
        public int Anio
        {
            get { return _Anio; }
            set { SetPropertyValue("Anio", ref _Anio, value); }
        }
        
        public string RegistroCNR
        {
            get { return _RegistroCNR; }
            set { SetPropertyValue("RegistroCNR", ref _RegistroCNR, value); }
        }

        [Appearance("ValorTotal", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public decimal ValorTotal
        {
            get { return _ValorTotal;}
            set { SetPropertyValue("ValorTotal", ref _ValorTotal, value);}
        }

        public bool Guardado
        {
            get { return _Guardado; }
            set { SetPropertyValue("Guardado", ref _Guardado, value);  }
        }


        [Appearance("Costeo", Visibility = ViewItemVisibility.Hide, Criteria = "Licencia = true")]
        [Association("Software-Costeo")]
        public XPCollection<Costeo> Costeo
        {
            get
            {
                return GetCollection<Costeo>("Costeo");
            }
        }

        [Appearance("Amortizacion", Visibility = ViewItemVisibility.Hide, Criteria = "Licencia = true")]
        [Association("Software-Amortizacion")]
        public XPCollection<Amortizacion> Amortizacion
        {
            get
            {
                return GetCollection<Amortizacion>("Amortizacion");
            }
        }






        protected override void OnSaving()
        {

            CalcularAmortizacion();

        }


        public void CalcularAmortizacion()
        {
            if (!ReferenceEquals(Establecimiento, null))
            {
                int meses = VidaUtil * 12;

                DepreciacionMensual = Math.Round(ValorInicial / meses, 2);

                var ListCosteo = Session.Query<Costeo>().Where(f => f.Software.CodigoDeActivo == this.CodigoDeActivo).ToList();

                decimal valor = 0;

                if (!ReferenceEquals(ListCosteo, null))
                {
                    foreach (Costeo ObjCosteo in ListCosteo)
                    {
                        valor += ObjCosteo.Monto;
                    }

                }

                this.ValorTotal = (decimal)ValorInicial + valor;
            }
        }












    }



}
