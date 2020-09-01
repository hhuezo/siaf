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
using SIAF.Module.BusinessObjects.Seguridad;

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Scheduler")]
    [ModelDefault("Caption", "Historial de reseteos")]
    public class HistorialDeReseteo : Entidad
    {
        public HistorialDeReseteo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        //private string _Equipo;
        //private string _IP;

        private DateTime _FechaTemporal;
        [NonPersistent]
        [ModelDefault("DisplayFormat", "{0:yyyy/MM/dd hh:mm:ss tt}")]
        [XafDisplayName("Fecha")]
        [Appearance("FechaTemporal", Enabled = false)]
        public DateTime FechaTemporal
        {
            get { return FechaDeIngreso; }
            set { SetPropertyValue("FechaTemporal", ref _FechaTemporal, value); }
        }


        private Usuario _Usuario;
        [Appearance("Usuario", Enabled = false)]
        public Usuario Usuario
        {
            get { return _Usuario; }
            set { SetPropertyValue("Usuario", ref _Usuario, value); }
        }

        protected override void OnLoaded()
        {
            BinaryOperator CriteriaUsuario = new BinaryOperator("Oid", UsuarioCreador);
            Usuario = this.Session.FindObject<Usuario>(CriteriaUsuario);
            base.OnLoaded();
        }

        //public string Equipo
        //{
        //    get { return _Equipo; }
        //    set { SetPropertyValue("Equipo", ref _Equipo, value); }
        //}

        //public string IP
        //{
        //    get { return _IP; }
        //    set { SetPropertyValue("IP", ref _IP, value); }
        //}
    }
}
