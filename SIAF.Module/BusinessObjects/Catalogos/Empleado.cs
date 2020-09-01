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

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Position")]
    [XafDefaultProperty("Nombre")]
    public class Empleado : Entidad
    {
        public Empleado(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private bool _Activo;
        private Ambiente _Ambiente;
        private Depto _Departamento;
        private Gerencia _Gerencia;
        private string _Nombre;
        private string _Codigo;

        [RuleUniqueValue]
        [XafDisplayName("Código")]
        public string Codigo
        {
            get { return _Codigo; }
            set { SetPropertyValue("Codigo", ref _Codigo, value); }
        }
        
        [Association("Ambiente-Empleado")]
        public Ambiente Ambiente
        {
            get { return _Ambiente; }
            set { SetPropertyValue("Ambiente", ref _Ambiente, value); }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { SetPropertyValue("Nombre", ref _Nombre, value); }
        }
        
        public Gerencia Gerencia
        {
            get { return _Gerencia; }
            set { SetPropertyValue("Gerencia", ref _Gerencia, value); }
        }

        public Depto Departamento
        {
            get { return _Departamento; }
            set { SetPropertyValue("Departamento", ref _Departamento, value); }
        }


        public bool Activo
        {
            get
            {
                return _Activo;
            }
            set
            {
                SetPropertyValue("Activo", ref _Activo, value);
            }
        }
    }
}
