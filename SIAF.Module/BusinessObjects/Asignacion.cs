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
    [ImageName("BO_Lead")]
    [Appearance("Asignacion", Enabled = false, Criteria = "(Estado = 1) AND !IsCurrentUserInRole('Administrators')", TargetItems = "*")]
    public class Asignacion : Entidad
    {
        public Asignacion(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private EstadoMovimiento _Estado;
        private Empleado _Empleado;
        private Ambiente _Ambiente;
        private Unidad _Unidad;

        [NonPersistent]
        private DateTime _FechaTemporal;
        [ModelDefault("DisplayFormat", "{0:g}")]
        [XafDisplayName("Fecha")]
        [Appearance("FechaTemporal", Enabled = false)]
        public DateTime FechaTemporal
        {
            get { return FechaDeIngreso; }
            set { SetPropertyValue("FechaTemporal", ref _FechaTemporal, value); }
        }
        
        [ImmediatePostData]
        public Unidad Unidad
        {
            get { return _Unidad; }
            set { SetPropertyValue("Unidad", ref _Unidad, value); }
        }

        [ImmediatePostData]
        [DataSourceProperty("Unidad.Ambiente")]
        public Ambiente Ambiente
        {
            get { return _Ambiente; }
            set { SetPropertyValue("Ambiente", ref _Ambiente, value); }
        }

        [DataSourceProperty("Ambiente.Empleado")]
        public Empleado Empleado
        {
            get { return _Empleado; }
            set { SetPropertyValue("Empleado", ref _Empleado, value); }
        }

        [Appearance("Estado", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public EstadoMovimiento Estado
        {
            get { return _Estado; }
            set { SetPropertyValue("Estado", ref _Estado, value); }
        }

        [Association("Asignacion-Activo")]
        public XPCollection<Activo> Activo
        {
            get { return GetCollection<Activo>("Activo"); }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {                
                case "Unidad":
                    Ambiente = null;
                    break;
                case "Ambiente":
                    Empleado = null;
                    break;
                default:
                    break;
            }
        }
    }
}
