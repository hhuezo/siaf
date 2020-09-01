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
using DevExpress.ExpressApp.Editors;

namespace SIAF.Module.BusinessObjects
{
    [NonPersistent]
    public class Entidad : BaseObject
    {
        public Entidad(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private string _CodAnterior;
        private DateTime _FechaDeIngreso;
        private Guid _UsuarioCreador;

        [Appearance("CodAnterior", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string CodAnterior
        {
            get { return _CodAnterior; }
            set { SetPropertyValue("CodAnterior", ref _CodAnterior, value); }
        }

        [Appearance("UsuarioCreador", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public Guid UsuarioCreador
        {
            get {return _UsuarioCreador;}
            set {SetPropertyValue("UsuarioCreador", ref _UsuarioCreador, value);}
        }

        [Appearance("FechaDeIngreso", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public DateTime FechaDeIngreso
        {
            get {return _FechaDeIngreso;}
            set {SetPropertyValue("FechaDeIngreso", ref _FechaDeIngreso, value);}
        }

        protected override void OnSaving()
        {
            if (UsuarioCreador == Guid.Empty)
            {
                UsuarioCreador = (Guid)SecuritySystem.CurrentUserId;
                FechaDeIngreso = Hora.ObtenerHora();
            }                    
            base.OnSaving();
        }
    }
}