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
    [ImageName("Action_Delete")]
    public class Descargo : Entidad
    {
        public Descargo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private string _Marca;
        private Destino _Destino;
        private MotivoDescargo _MotivoDescargo;
        private EstadoMovimiento _Estado;
        private string _CargoRecibidoPor;
        private Empleado _RecibidoPor;
        private string _CargoAprobadoPor;
        private Empleado _AprobadoPor;
        private string _CargoSolicitadoPor;
        private Empleado _SolicitadoPor;

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

        [XafDisplayName("Motivo descargo")]
        public MotivoDescargo MotivoDescargo
        {
            get { return _MotivoDescargo; }
            set { SetPropertyValue("MotivoDescargo", ref _MotivoDescargo, value); }
        }
        
        public Destino Destino
        {
            get { return _Destino; }
            set { SetPropertyValue("Destino", ref _Destino, value); }
        }

        [XafDisplayName("Solicitado por")]
        public Empleado SolicitadoPor
        {
            get { return _SolicitadoPor; }
            set { SetPropertyValue("SolicitadoPor", ref _SolicitadoPor, value); }
        }

        //[XafDisplayName("Cargo")]
        [XafDisplayName("Cargo Solicitado")]
        public string CargoSolicitadoPor
        {
            get { return _CargoSolicitadoPor; }
            set { SetPropertyValue("CargoSolicitadoPor", ref _CargoSolicitadoPor, value); }
        }

        [XafDisplayName("Realizado por")]
        public Empleado AprobadoPor
        {
            get { return _AprobadoPor; }
            set { SetPropertyValue("AprobadoPor", ref _AprobadoPor, value); }
        }

        //[XafDisplayName("Cargo")]
        [XafDisplayName("Cargo")]
        public string CargoAprobadoPor
        {
            get { return _CargoAprobadoPor; }
            set { SetPropertyValue("CargoAprobadoPor", ref _CargoAprobadoPor, value); }
        }

        [XafDisplayName("Autorizado por")]
        public Empleado RecibidoPor
        {
            get { return _RecibidoPor; }
            set { SetPropertyValue("RecibidoPor", ref _RecibidoPor, value); }
        }

        //[XafDisplayName("Cargo")]
        [XafDisplayName("Cargo Autorizado")]
        public string CargoRecibidoPor
        {
            get { return _CargoRecibidoPor; }
            set { SetPropertyValue("CargoRecibidoPor", ref _CargoRecibidoPor, value); }
        }

        [Appearance("Estado", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public EstadoMovimiento Estado
        {
            get { return _Estado; }
            set { SetPropertyValue("Estado", ref _Estado, value); }
        }

        [Association("Descargo-Activo")]
        public XPCollection<Activo> Activo
        {
            get { return GetCollection<Activo>("Activo"); }
        }



        //metodo para cambiar estado a los activos que se quieren descargar
        public void ProcesoDescargo()
        {
           if(!ReferenceEquals(FechaTemporal,null))
            {
                foreach (Activo DetalleActivo in Activo)
                {
                    if (DetalleActivo.Estado == BusinessObjects.Estado.Asignado || DetalleActivo.Estado == BusinessObjects.Estado.Disponible)
                    {
                        DetalleActivo.Estado = BusinessObjects.Estado.Proceso;
                    }
                    
                }
            }
        }

        protected override void OnSaving()
        {
            if (!ReferenceEquals(FechaTemporal, null))
            {
                ProcesoDescargo();
            
           
                if (ReferenceEquals(FechaDeIngreso, null)) { this.FechaDeIngreso = DateTime.Now; }
                base.OnSaving();
            }
        }
    }
}
