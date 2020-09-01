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
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class ReporteDepreciacion : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public ReporteDepreciacion(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        // Fields...
        private decimal _DepresiacionAcumulada;
        private decimal _ValoTotal;
        private string _Axo;
        private string _NoChasis;
        private string _NoMotor;
        private string _Placa;
        private DateTime _Fecha;
        private string _Cuenta;
        private decimal _ValorLibro;
        private decimal _DepreciacionMes;
        private decimal _DepreciacionDias;
        private decimal _ValorDepreciacion;
        private decimal _ValorAdquisicion;
        private DateTime _Adquisicion;
        private string _Fuente;
        private string _Estado;
        private string _Serie;
        private string _Modelo;
        private string _Marca;
        private string _Ubicacion;
        private string _Descripcion;
        private string _Codigo;
        private bool _Eliminar;

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


        public string Codigo
        {
            get
            {
                return _Codigo;
            }
            set
            {
                SetPropertyValue("Codigo", ref _Codigo, value);
            }
        }


        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                SetPropertyValue("Descripcion", ref _Descripcion, value);
            }
        }


        public string Ubicacion
        {
            get
            {
                return _Ubicacion;
            }
            set
            {
                SetPropertyValue("Ubicacion", ref _Ubicacion, value);
            }
        }


        public string Marca
        {
            get
            {
                return _Marca;
            }
            set
            {
                SetPropertyValue("Marca", ref _Marca, value);
            }
        }


        public string Modelo
        {
            get
            {
                return _Modelo;
            }
            set
            {
                SetPropertyValue("Modelo", ref _Modelo, value);
            }
        }


        public string Serie
        {
            get
            {
                return _Serie;
            }
            set
            {
                SetPropertyValue("Serie", ref _Serie, value);
            }
        }


        public string Estado
        {
            get
            {
                return _Estado;
            }
            set
            {
                SetPropertyValue("Estado", ref _Estado, value);
            }
        }


        public string Fuente
        {
            get
            {
                return _Fuente;
            }
            set
            {
                SetPropertyValue("Fuente", ref _Fuente, value);
            }
        }



        public DateTime Adquisicion
        {
            get
            {
                return _Adquisicion;
            }
            set
            {
                SetPropertyValue("Adquisicion", ref _Adquisicion, value);
            }
        }


        public decimal ValorAdquisicion
        {
            get
            {
                return _ValorAdquisicion;
            }
            set
            {
                SetPropertyValue("ValorAdquisicion", ref _ValorAdquisicion, value);
            }
        }


        public decimal ValorDepreciacion
        {
            get
            {
                return _ValorDepreciacion;
            }
            set
            {
                SetPropertyValue("ValorDepreciacion", ref _ValorDepreciacion, value);
            }
        }



        public decimal DepreciacionDias
        {
            get
            {
                return _DepreciacionDias;
            }
            set
            {
                SetPropertyValue("DepreciacionDias", ref _DepreciacionDias, value);
            }
        }


        public decimal DepreciacionMes
        {
            get
            {
                return _DepreciacionMes;
            }
            set
            {
                SetPropertyValue("DepreciacionMes", ref _DepreciacionMes, value);
            }
        }



        public decimal ValorLibro
        {
            get
            {
                return _ValorLibro;
            }
            set
            {
                SetPropertyValue("ValorLibro", ref _ValorLibro, value);
            }
        }


        public decimal DepresiacionAcumulada
        {
            get
            {
                return _DepresiacionAcumulada;
            }
            set
            {
                SetPropertyValue("DepresiacionAcumulada", ref _DepresiacionAcumulada, value);
            }
        }

        public string Cuenta
        {
            get
            {
                return _Cuenta;
            }
            set
            {
                SetPropertyValue("Cuenta", ref _Cuenta, value);
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


        public string Placa
        {
            get
            {
                return _Placa;
            }
            set
            {
                SetPropertyValue("Placa", ref _Placa, value);
            }
        }


        public string NoMotor
        {
            get
            {
                return _NoMotor;
            }
            set
            {
                SetPropertyValue("NoMotor", ref _NoMotor, value);
            }
        }


        public string NoChasis
        {
            get
            {
                return _NoChasis;
            }
            set
            {
                SetPropertyValue("NoChasis", ref _NoChasis, value);
            }
        }


        public string Axo
        {
            get
            {
                return _Axo;
            }
            set
            {
                SetPropertyValue("Axo", ref _Axo, value);
            }
        }


        public decimal ValoTotal
        {
            get
            {
                return _ValoTotal;
            }
            set
            {
                SetPropertyValue("ValoTotal", ref _ValoTotal, value);
            }
        }

        

    }
}
