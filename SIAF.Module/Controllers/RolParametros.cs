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

namespace SIAF.Module.Controllers
{
    [NonPersistent]
    [ModelDefault("Caption", "Generar rol")]
    public class RolParametros : XPLiteObject
    {
        public RolParametros(Session session) : base(session) { }
        public override void AfterConstruction() 
        {
            base.AfterConstruction();
            this.Leer = true;
        }

        // Fields...
        private bool _RolVacio;
        private bool _Navegar;
        private bool _Borrar;
        private bool _Crear;
        private bool _Escribir;
        private bool _Leer;

        [Appearance("Leer", Enabled = false, Criteria = "RolVacio = True")]
        public bool Leer
        {
            get { return _Leer; }
            set { SetPropertyValue("Leer", ref _Leer, value); }
        }

        [Appearance("Escribir", Enabled = false, Criteria = "RolVacio = True")]
        public bool Escribir
        {
            get { return _Escribir; }
            set { SetPropertyValue("Escribir", ref _Escribir, value); }
        }

        [Appearance("Crear", Enabled = false, Criteria = "RolVacio = True")]
        public bool Crear
        {
            get { return _Crear; }
            set { SetPropertyValue("Crear", ref _Crear, value); }
        }

        [Appearance("Borrar", Enabled = false, Criteria = "RolVacio = True")]
        public bool Borrar
        {
            get { return _Borrar; }
            set { SetPropertyValue("Borrar", ref _Borrar, value); }
        }

        [Appearance("Navegar", Enabled = false, Criteria = "RolVacio = True")]
        public bool Navegar
        {
            get { return _Navegar; }
            set { SetPropertyValue("Navegar", ref _Navegar, value); }
        }

        [XafDisplayName("Generar perfil vacío")]
        [ImmediatePostData]
        public bool RolVacio
        {
            get { return _RolVacio; }
            set { SetPropertyValue("RolVacio", ref _RolVacio, value); }
        }
    }
}
