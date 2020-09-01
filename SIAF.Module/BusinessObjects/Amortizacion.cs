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
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class Amortizacion : Entidad
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Amortizacion(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }


        // Fields...
        private int _Mes;
        private int _Axo;
        private int _Numero;
        private decimal _SaldoPendiente;
        private decimal _Saldo;
        private decimal _AmortizacionMensual;
        private DateTime _Fecha;
        private Software _Software;

        [Association("Software-Amortizacion")]
        public Software Software
        {
            get
            {
                return _Software;
            }
            set
            {
                SetPropertyValue("Software", ref _Software, value);
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




        public int Axo
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



        public int Mes
        {
            get
            {
                return _Mes;
            }
            set
            {
                SetPropertyValue("Mes", ref _Mes, value);
            }
        }

        public decimal AmortizacionMensual
        {
            get
            {
                return _AmortizacionMensual;
            }
            set
            {
                SetPropertyValue("AmortizacionMensual", ref _AmortizacionMensual, value);
            }
        }



        public decimal Saldo
        {
            get
            {
                return _Saldo;
            }
            set
            {
                SetPropertyValue("Saldo", ref _Saldo, value);
            }
        }



        public decimal SaldoPendiente
        {
            get
            {
                return _SaldoPendiente;
            }
            set
            {
                SetPropertyValue("SaldoPendiente", ref _SaldoPendiente, value);
            }
        }

    }
}
