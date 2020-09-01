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
    [NavigationItem("Extras")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class EquipoDescargado : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public EquipoDescargado(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        // Fields...


       
        // Fields...
        private string _SubClase;
        private string _Observacion;
        private double _ValorInicial;
        private string _Fuente;
        private string _CuentaContable;
        private string _Procedencia;
        private string _FechaDeAdquisicion;
        private string _Material;
        private string _Color;
        private string _Serie;
        private string _Modelo;
        private string _Marca;
        private string _CodigoDeActivo;
        private int _Oid;

        [Key]
        public int Oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                SetPropertyValue("Oid", ref _Oid, value);
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


        public string SubClase
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


        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                SetPropertyValue("Color", ref _Color, value);
            }
        }


        public string Material
        {
            get
            {
                return _Material;
            }
            set
            {
                SetPropertyValue("Material", ref _Material, value);
            }
        }


        public string FechaDeAdquisicion
        {
            get
            {
                return _FechaDeAdquisicion;
            }
            set
            {
                SetPropertyValue("FechaDeAdquisicion", ref _FechaDeAdquisicion, value);
            }
        }


        public string Procedencia
        {
            get
            {
                return _Procedencia;
            }
            set
            {
                SetPropertyValue("Procedencia", ref _Procedencia, value);
            }
        }


        public string CuentaContable
        {
            get
            {
                return _CuentaContable;
            }
            set
            {
                SetPropertyValue("CuentaContable", ref _CuentaContable, value);
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


        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        public double ValorInicial
        {
            get
            {
                return _ValorInicial;
            }
            set
            {
                SetPropertyValue("ValorInicial", ref _ValorInicial, value);
            }
        }


        public string Observacion
        {
            get
            {
                return _Observacion;
            }
            set
            {
                SetPropertyValue("Observacion", ref _Observacion, value);
            }
        }

    }
}
