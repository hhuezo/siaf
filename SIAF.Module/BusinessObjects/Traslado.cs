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
    [ImageName("Action_Refresh")]
    [Appearance("Traslado", Enabled = false, Criteria = "(Estado = 1) AND !IsCurrentUserInRole('Administrators')", TargetItems = "*")]
    public class Traslado : Entidad
    {
        public Traslado(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private EstadoMovimiento _Estado;
        private string _CargoRecibidoPor;
        private Empleado _RecibidoPor;
        //private string _CargoAprobadoPor;
        //private Empleado _AprobadoPor;
        private string _CargoSolicitadoPor;
        private Empleado _SolicitadoPor;
        private ClaseMovimiento _ClaseMovimiento;

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

        public ClaseMovimiento ClaseMovimiento
        {
            get { return _ClaseMovimiento; }
            set { SetPropertyValue("ClaseMovimiento", ref _ClaseMovimiento, value); }
        }

        [XafDisplayName("Entregado por")]
        public Empleado SolicitadoPor
        {
            get { return _SolicitadoPor; }
            set { SetPropertyValue("SolicitadoPor", ref _SolicitadoPor, value); }
        }

        //[XafDisplayName("Cargo")]
        //public string CargoSolicitadoPor
        //{
        //    get { return _CargoSolicitadoPor; }
        //    set { SetPropertyValue("CargoSolicitadoPor", ref _CargoSolicitadoPor, value); }
        //}

        //[XafDisplayName("Aprobado por")]
        //public Empleado AprobadoPor
        //{
        //    get { return _AprobadoPor; }
        //    set { SetPropertyValue("AprobadoPor", ref _AprobadoPor, value); }
        //}

        //[XafDisplayName("Cargo")]
        //public string CargoAprobadoPor
        //{
        //    get { return _CargoAprobadoPor; }
        //    set { SetPropertyValue("CargoAprobadoPor", ref _CargoAprobadoPor, value); }
        //}

        [XafDisplayName("Recibido por")]
        public Empleado RecibidoPor
        {
            get { return _RecibidoPor; }
            set { SetPropertyValue("RecibidoPor", ref _RecibidoPor, value); }
        }

        //[XafDisplayName("Cargo")]
        //public string CargoRecibidoPor
        //{
        //    get { return _CargoRecibidoPor; }
        //    set { SetPropertyValue("CargoRecibidoPor", ref _CargoRecibidoPor, value); }
        //}

        [Appearance("Estado", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public EstadoMovimiento Estado
        {
            get { return _Estado; }
            set { SetPropertyValue("Estado", ref _Estado, value); }
        }

        [Association("Traslado-Activo")]
        public XPCollection<Activo> Activo
        {
            get { return GetCollection<Activo>("Activo"); }
        }
    }
}
