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

namespace SIAF.Module.BusinessObjects.Reportes.temporal
{
    [DefaultClassOptions]

    public class Amorizacion : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Amorizacion(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        // Fields...
        private decimal _SaldoPendiente;
        private decimal _Saldo;
        private Software _Software;
        private DateTime _Fecha;

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
