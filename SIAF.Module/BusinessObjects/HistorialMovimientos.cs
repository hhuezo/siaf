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

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [ModelDefault("Caption", "Historial de movimientos")]
    public class HistorialMovimientos : Entidad
    {
        public HistorialMovimientos(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Activo _Activo;
        private Empleado _Empleado;
        private Ambiente _Ambiente;
        private Unidad _Unidad;
        private TipoHistorialMovimientos _TipoDeMovimiento;

        private DateTime _FechaTemporal;
        [NonPersistent]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [XafDisplayName("Fecha")]
        [Appearance("FechaTemporal", Enabled = false)]
        public DateTime FechaTemporal
        {
            get { return FechaDeIngreso; }
            set { SetPropertyValue("FechaTemporal", ref _FechaTemporal, value); }
        }

        [XafDisplayName("Tipo de movimiento")]
        public TipoHistorialMovimientos TipoDeMovimiento
        {
            get { return _TipoDeMovimiento; }
            set { SetPropertyValue("TipoDeMovimiento", ref _TipoDeMovimiento, value); }
        }

        public Unidad Unidad
        {
            get { return _Unidad; }
            set { SetPropertyValue("Unidad", ref _Unidad, value); }
        }

        public Ambiente Ambiente
        {
            get { return _Ambiente; }
            set { SetPropertyValue("Ambiente", ref _Ambiente, value); }
        }

        public Empleado Empleado
        {
            get { return _Empleado; }
            set { SetPropertyValue("Empleado", ref _Empleado, value); }
        }

        [Appearance("Activo-HistorialMovimientos", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [Association("Activo-HistorialMovimientos")]
        public Activo Activo
        {
            get { return _Activo; }
            set { SetPropertyValue("Activo", ref _Activo, value);
            }
        }
    }
}
