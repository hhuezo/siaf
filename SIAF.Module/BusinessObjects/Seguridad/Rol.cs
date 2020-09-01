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
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace SIAF.Module.BusinessObjects.Seguridad
{
    [DefaultClassOptions]
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class Rol : SecuritySystemRole
    {
        public Rol(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        private DateTime _FechaDeIngreso;
        private Guid _UsuarioCreador;

        [Association("Reporte-Rol")]
        public XPCollection<Reporte> Reporte
        {
            get { return GetCollection<Reporte>("Reporte"); }
        }

        [Appearance("UsuarioCreador", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public Guid UsuarioCreador
        {
            get { return _UsuarioCreador; }
            set { SetPropertyValue("UsuarioCreador", ref _UsuarioCreador, value); }
        }

        [Appearance("FechaDeIngreso", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public DateTime FechaDeIngreso
        {
            get { return _FechaDeIngreso; }
            set { SetPropertyValue("FechaDeIngreso", ref _FechaDeIngreso, value); }
        }

        protected override void OnSaving()
        {
            if (UsuarioCreador == Guid.Empty)
            {
                try
                {
                    UsuarioCreador = (Guid)SecuritySystem.CurrentUserId;
                    FechaDeIngreso = Hora.ObtenerHora();
                }
                catch
                { FechaDeIngreso = Hora.ObtenerHora(); }
            }
            base.OnSaving();
        }
    }    
}
