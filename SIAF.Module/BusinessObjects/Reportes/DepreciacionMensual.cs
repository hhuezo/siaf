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

namespace SIAF.Module.BusinessObjects.Reportes
{
    [DefaultClassOptions]
    [NavigationItem("Catalogos")]
    [FriendlyKeyProperty("Equipo")]
    public class DepreciacionMensual : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public DepreciacionMensual(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }




        // Fields...
        private string _CodigoDeActivo;
        private SubClase _SubClase;
        private Equipo _Equipo;
        private Vehiculo _Vehiculo;
        private DateTime _FechaFinal;
        private DateTime _FechaInicio;
        private decimal _ValorActual;
        private DateTime _Fecha;
        private int _Numero;
        private bool _Eliminar;
        private decimal _Depreciacion;



        public Vehiculo Vehiculo
        {
            get
            {
                return _Vehiculo;
            }
            set
            {
                SetPropertyValue("Vehiculo", ref _Vehiculo, value);
            }
        }



        public Equipo Equipo
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

        public int Numero
        {
            get
            {
                return _Numero;
            }
            set
            {
                SetPropertyValue("Numero", ref _Numero, value);
            }
        }



        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }
            set
            {
                SetPropertyValue("Fecha", ref _Fecha, value);
            }
        }



        public decimal ValorActual
        {
            get
            {
                return _ValorActual;
            }
            set
            {
                SetPropertyValue("ValorActual", ref _ValorActual, value);
            }
        }

        public decimal Depreciacion
        {
            get
            {
                return _Depreciacion;
            }
            set
            {
                SetPropertyValue("Depreciacion", ref _Depreciacion, value);
            }
        }



        public DateTime FechaInicio
        {
            get
            {
                return _FechaInicio;
            }
            set
            {
                SetPropertyValue("FechaInicio", ref _FechaInicio, value);
            }
        }


        public SubClase SubClase
        {
            get
            {
                return _SubClase;
            }
            set
            {
                SetPropertyValue("SubClase", ref _SubClase, value);
            }
        }



        public string CodigoDeActivo
        {
            get
            {
                return _CodigoDeActivo;
            }
            set
            {
                SetPropertyValue("CodigoDeActivo", ref _CodigoDeActivo, value);
            }
        }

        public DateTime FechaFinal
        {
            get
            {
                return _FechaFinal;
            }
            set
            {
                SetPropertyValue("FechaFinal", ref _FechaFinal, value);
            }
        }


        public bool Eliminar
        {
            get
            {
                return _Eliminar;
            }
            set
            {
                SetPropertyValue("Eliminar", ref _Eliminar, value);
            }
        }


    }
}
