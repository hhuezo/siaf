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
    public class Usuario : SecuritySystemUser
    {
        public Usuario(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private bool _UsuarioAdministrador;
        private string _Email;
        private string _NombreCompleto;
        private DateTime _FechaDeIngreso;
        private Guid _UsuarioCreador;

        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { SetPropertyValue("NombreCompleto", ref _NombreCompleto, value); }
        }

        public string Email
        {
            get { return _Email; }
            set { SetPropertyValue("Email", ref _Email, value); }
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


        public bool UsuarioAdministrador
        {
            get
            {
                return _UsuarioAdministrador;
            }
            set
            {
                SetPropertyValue("UsuarioAdministrador", ref _UsuarioAdministrador, value);
            }
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

        [Association("Usuario-Ambiente")]
        public XPCollection<Ambiente> Ambiente
        {
            get { return GetCollection<Ambiente>("Ambiente"); }
        }

        public CriteriaOperator ObtenerFiltroUnidades()
        {
            CriteriaOperator FiltroDeUnidades = null;
            //List<CriteriaOperator> Operadores = new List<CriteriaOperator>();
            //foreach (Cooperativa unidadAdministrativa in this.Cooperativa)
            //    Operadores.Add(new BinaryOperator("ResponsableDirecto.Oid", unidadAdministrativa.Oid));
            //foreach (Cooperativa unidadAdministrativa in this.Cooperativa)
            //    Operadores.Add(new BinaryOperator("ResponsableIndirecto.Oid", unidadAdministrativa.Oid));
            //if (Operadores.Count == 0)
            //    Operadores.Add(new BinaryOperator("ResponsableDirecto.Oid", Guid.Empty));
            //FiltroDeUnidades = CriteriaOperator.Or(Operadores.ToArray());
            return FiltroDeUnidades;
        }

        public CriteriaOperator ObtenerFiltroReportes()
        {
            CriteriaOperator FiltroDeUnidades = null;
            List<CriteriaOperator> Operadores = new List<CriteriaOperator>();
            foreach (Rol rol in this.Roles)
                foreach (Reporte reporte in rol.Reporte)
                    Operadores.Add(new BinaryOperator("Oid", reporte.Oid));
            //if (Operadores.Count == 0)
            //    Operadores.Add(new BinaryOperator("Oid", Guid.Empty));
            FiltroDeUnidades = CriteriaOperator.Or(Operadores.ToArray());
            return FiltroDeUnidades;
        }
    }
}
