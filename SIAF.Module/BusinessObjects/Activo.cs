using System;
using System.Linq;
using System.Linq.Expressions;
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
using siaf.Module.BusinessObjects.Enums;

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty("CodigoDeActivo")]
    [RuleCombinationOfPropertiesIsUnique("KeyUniqueActivo", DefaultContexts.Save, "Establecimiento, Grupo, Clase, SubClase, Correlativo")]
    [Appearance("Grupo", TargetItems = "Grupo", Enabled = false, Criteria = "Tipo<>'Activo'")]
    [Appearance("Vehiculo", TargetItems = "Clase", Enabled = false, Criteria = "Tipo='Vehiculo'")]

    public class Activo : Entidad
    {
        public Activo(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            BinaryOperator Criteria = new BinaryOperator("Descripcion", "ISTA");
            this.Establecimiento = this.Session.FindObject<Establecimiento>(Criteria);

            Criteria = null;
            Tipo = base.ClassInfo.TableName;
            switch (Tipo)
            {
                case "Equipo":
                    Criteria = new BinaryOperator("Codigo", "611");
                    break;
                case "Vehiculo":
                    Criteria = new BinaryOperator("Codigo", "611");
                    break;
                case "Software":
                    Criteria = new BinaryOperator("Codigo", "614");
                    break;
                default:
                    break;
            }
            this.Grupo = this.Session.FindObject<Grupo>(Criteria);
            if (Tipo == "Vehiculo")
            {
                Criteria = new BinaryOperator("Codigo", "05");
                this.Clase = this.Session.FindObject<Clase>(Criteria);
            }
            
            base.AfterConstruction();
        }
                
        // Fields...
        private bool _ValorFijo;
        private Depresiable _Depresiable;
        private double _DepreciacionMensual;
        private string _PropertyName;
        private bool _NoDepreciable;
        private string _Detalle;
        private int _CorrelativoInt;
        private string _CodigoEmpleado;
        private int _DiasADepreciar;
        private double _DepreciacionAnual;
        private double _ValorADepreciar;
        private double _ValorResidual;
        private bool _DepreciadoTotalmente;
        private Estado _Estado;
        private EstadoActivoGuardado _EstadoActivoGuardado;
        private double _DepreciacionDiaria;
        private string _Tipo;
        private EstadoAnterior _EstadoAnterior;
        private Ambiente _Ambiente;
        private Unidad _Unidad;
        private Empleado _Empleado;
        private string _CodigoDeActivo;
        private double _DepreciacionAcumulada;
        private int _VidaUtil;
        private double _ValorActual;
        private double _ValorInicial;
        private string _Observacion;
        private string _OtraCaracteristica;
        private string _NumeroDeFactura;
        private Fuente _Fuente;
        private Procedencia _Procedencia;
        private DateTime _FechaTemporal;
        private DateTime _FechaDeAdquisicion;
        private EstadoFisico _EstadoFisico;
        private CuentaContable _CuentaContable;
        private string _Correlativo;
        private SubClase _SubClase;
        private Clase _Clase;
        private Grupo _Grupo;
        private Establecimiento _Establecimiento;

        [Appearance("Establecimiento", Enabled = false)]
        public Establecimiento Establecimiento
        {
            get { return _Establecimiento; }
            set { SetPropertyValue("Establecimiento", ref _Establecimiento, value); }
        }

        [ImmediatePostData]
        public Grupo Grupo
        {
            get { return _Grupo; }
            set { SetPropertyValue("Grupo", ref _Grupo, value); }
        }

        [ImmediatePostData]
        //[DataSourceProperty("Grupo.Clase")]
        //[DataSourceCriteria("Grupo.Codigo='614'")]
        [DataSourceProperty("ClasesDisponibles")]
        public Clase Clase
        {
            get { return _Clase; }
            set { SetPropertyValue("Clase", ref _Clase, value); }
        }


        private XPCollection<Clase> clasesDisponibles;
        [Browsable(false)]
        public XPCollection<Clase> ClasesDisponibles
        {
            get
            {
                clasesDisponibles = new XPCollection<Clase>(Session);
                List<CriteriaOperator> Operadores = new List<CriteriaOperator>();
                CriteriaOperator Criteria = null;
                Operadores.Add(new BinaryOperator("Grupo", Grupo));
                switch (Tipo)
                {
                    case "Equipo":
                        Operadores.Add(new BinaryOperator("Codigo", "05", BinaryOperatorType.NotEqual));
                        break;
                    case "Vehiculo":
                        Operadores.Add(new BinaryOperator("Codigo", "05"));
                        break;
                    case "Software":
                        break;
                    default:
                        break;
                }
                Criteria = CriteriaOperator.And(Operadores.ToArray());
                clasesDisponibles.Criteria = Criteria;
                return clasesDisponibles;
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("Clase.SubClase")]
        public SubClase SubClase
        {
            get { return _SubClase; }
            set { SetPropertyValue("SubClase", ref _SubClase, value); }
        }

        [XafDisplayName("Correlativo")]
        [Appearance("Correlativo", Enabled = false)]
        public string Correlativo
        {
            get { return _Correlativo; }
            set { SetPropertyValue("Correlativo", ref _Correlativo, value); }
        }


        [Appearance("CorrelativoInt", Visibility = ViewItemVisibility.Hide, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public int CorrelativoInt
        {
            get
            {
                if (!ReferenceEquals(Correlativo, null))
                {
                    _CorrelativoInt = Int32.Parse(Correlativo);
                }
                return _CorrelativoInt;
            }
            set
            {
                SetPropertyValue("CorrelativoInt", ref _CorrelativoInt, value);
            }
        }

        private string ObtenerCorrelativo()
        {
            CriteriaOperator Criteria = null;
            int secuencia = 1;
            int VarActivo = 0;
            Criteria = CriteriaOperator.Parse("Correlativo = [<Activo>][SubClase=?].Max(CorrelativoInt)", SubClase);

            Activo activo = this.Session.FindObject<Activo>(Criteria);


            if (activo != null)
            {
                //VarActivo = Int32.Parse(activo.Correlativo);
                //Criteria = CriteriaOperator.Parse("Correlativo = [<Activo>][SubClase=?].Max(CorrelativoInt)", SubClase);
                activo = this.Session.FindObject<Activo>(Criteria);
                secuencia = Int32.Parse(activo.Correlativo) + 1;
            }
            else
            {
                secuencia = 1;
            }
            return secuencia.ToString("D3");

        }

        [RuleUniqueValue]
        [RuleRequiredField]
        [XafDisplayName("Código de Activo")]
        [Appearance("CodigoDeActivo", Enabled = false)]
        public string CodigoDeActivo
        {
            get { return _CodigoDeActivo; }
            set { SetPropertyValue("CodigoDeActivo", ref _CodigoDeActivo, value); }
        }

        private string ObtenerCodigoDeActivo()
        {
            return Establecimiento.Codigo + '-' + Grupo.Codigo + '-' + Clase.Codigo + '-' + SubClase.Codigo + '-' + Correlativo;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "Grupo":
                    Clase = null;
                    VidaUtil = 0;
                    break;
                case "Clase":
                    SubClase = null;
                    VidaUtil = Clase.VidaUtil;
                    break;
                case "SubClase":
                    if ((Establecimiento != null) && (Grupo != null) && (Clase != null) && (SubClase != null))
                    {
                        Correlativo = ObtenerCorrelativo();
                        CodigoDeActivo = ObtenerCodigoDeActivo();
                    }
                    else
                    {
                        Correlativo = "";
                        CodigoDeActivo = "";
                    }
                    break;
                case "Unidad":
                    Ambiente = null;
                    break;
                case "Ambiente":
                    Empleado = null;
                    break;
                case "VidaUtil":
                    CalcularDatosDepreciacion();
                    break;
                case "ValorInicial":
                    CalcularDatosDepreciacion();
                    break;
                default:
                    break;
            }
        }

        private void CalcularDatosDepreciacion()
        {

        }

        public CuentaContable CuentaContable
        {
            get { return _CuentaContable; }
            set { SetPropertyValue("CuentaContable", ref _CuentaContable, value); }
        }

        [XafDisplayName("Estado físico")]
        public EstadoFisico EstadoFisico
        {
            get { return _EstadoFisico; }
            set { SetPropertyValue("Estado", ref _EstadoFisico, value); }
        }

        [XafDisplayName("Fecha de adquisicion")]
        public DateTime FechaDeAdquisicion
        {
            get { return _FechaDeAdquisicion; }
            set { SetPropertyValue("FechaDeAdquisicion", ref _FechaDeAdquisicion, value); }
        }


        [Appearance("UsuarioCreador", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [NonPersistent]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [XafDisplayName("Fecha de ingreso")]
       // [Appearance("FechaTemporal", Enabled = false)]
        public DateTime FechaTemporal
        {
            get { return FechaDeIngreso; }
            set { SetPropertyValue("FechaTemporal", ref _FechaTemporal, value); }
        }

        [ImmediatePostData]
        public Procedencia Procedencia
        {
            get { return _Procedencia; }
            set { SetPropertyValue("Procedencia", ref _Procedencia, value); }
        }


        [DataSourceProperty("Procedencia.Fuente")]
        public Fuente Fuente
        {
            get { return _Fuente; }
            set { SetPropertyValue("Fuente", ref _Fuente, value); }
        }


        public Depresiable Depresiable
        {
            get
            {
                return _Depresiable;
            }
            set
            {
                SetPropertyValue("Depresiable", ref _Depresiable, value);
            }
        }



   

        [XafDisplayName("Número de factura")]
        public string NumeroDeFactura
        {
            get { return _NumeroDeFactura; }
            set { SetPropertyValue("NumeroDeFactura", ref _NumeroDeFactura, value); }
        }

        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("Otra característica")]
        public string OtraCaracteristica
        {
            get { return _OtraCaracteristica; }
            set { SetPropertyValue("OtraCaracteristica", ref _OtraCaracteristica, value); }
        }

        [Size(200)]
        public string Observacion
        {
            get { return _Observacion; }
            set { SetPropertyValue("Observacion", ref _Observacion, value); }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        [XafDisplayName("Valor inicial")]
        public double ValorInicial
        {
            get { return _ValorInicial; }
            set { SetPropertyValue("ValorInicial", ref _ValorInicial, value); }
        }

        [XafDisplayName("Vida útil")]
        [Appearance("VidaUtil", Enabled = false, Criteria = "Grupo.Codigo <> 614 ")] //validar si es bien intagible
        public int VidaUtil
        {
            get { return _VidaUtil; }
            set { SetPropertyValue("VidaUtil", ref _VidaUtil, value); }
        }




        [Appearance("ValorResidual", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        [XafDisplayName("Valor residual")]
        public double ValorResidual
        {
            get {
               
                return _ValorResidual; }
            set { SetPropertyValue("ValorResidual", ref _ValorResidual, value); }
        }

        [Appearance("ValorADepreciar", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        [XafDisplayName("Valor a depreciar")]
        public double ValorADepreciar
        {
            get {
                return _ValorADepreciar; }
            set { SetPropertyValue("ValorADepreciar", ref _ValorADepreciar, value); }
        }
        
        [Appearance("DiasADepreciar", Visibility = ViewItemVisibility.Hide)]
        [XafDisplayName("Dias a depreciar")]
        public int DiasADepreciar
        {
            get { return _DiasADepreciar; }
            set { SetPropertyValue("DiasADepreciar", ref _DiasADepreciar, value); }
        }

        [Appearance("DepreciacionAnual", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        [XafDisplayName("Depreciación anual")]
        public double DepreciacionAnual
        {
            get { return _DepreciacionAnual; }
            set { SetPropertyValue("DepreciacionAnual", ref _DepreciacionAnual, value); }
        }


        [Appearance("DepreciacionMensual", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        public double DepreciacionMensual
        {
            get
            {return _DepreciacionMensual;}
            set
            {
                SetPropertyValue("DepreciacionMensual", ref _DepreciacionMensual, value);
            }
        }

        [Appearance("DepreciacionDiaria", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        [XafDisplayName("Depreciación diaria")]
        public double DepreciacionDiaria
        {
            get {  return _DepreciacionDiaria; }
            set { SetPropertyValue("DepreciacionDiaria", ref _DepreciacionDiaria, value); }
        }


        [Appearance("DepreciacionAcumulada", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        [XafDisplayName("Depreciación acumulada")]
        public double DepreciacionAcumulada
        {
            get {return _DepreciacionAcumulada; }
            set { SetPropertyValue("DepreciacionAcumulada", ref _DepreciacionAcumulada, value); }
        }

        [Appearance("ValorActual", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [ModelDefault("EditMask", "#,###,##0.00")]
        [ModelDefault("DisplayFormat", "#,###,##0.00")]
        [XafDisplayName("Valor actual")]
        public double ValorActual
        {
            get { return _ValorActual; }
            set { SetPropertyValue("ValorActual", ref _ValorActual, value); }
        }

        [ImmediatePostData]
       // [Appearance("Unidad", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public Unidad Unidad
        {
            get { return _Unidad; }
            set { SetPropertyValue("Unidad", ref _Unidad, value); }
        }

        [ImmediatePostData]
       // [Appearance("Ambiente", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [DataSourceProperty("Unidad.Ambiente")]
        public Ambiente Ambiente
        {
            get { return _Ambiente; }
            set { SetPropertyValue("Ambiente", ref _Ambiente, value); }
        }

        [ImmediatePostData]
       // [Appearance("Empleado", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [DataSourceProperty("Ambiente.Empleado")]
        public Empleado Empleado
        {
            get { return _Empleado; }
            set { SetPropertyValue("Empleado", ref _Empleado, value); }
        }

        //[Appearance("Estado", Enabled = false)]
        [NonPersistent]
         public string CodigoEmpleado
        {
            get
            {
                String Cod ="";
                if (this.Empleado != null)
                {
                    Cod = Empleado.Codigo;
                }
                _CodigoEmpleado = Cod; 
                 return _CodigoEmpleado;
            }           
        }

        [Appearance("Detalle", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string Detalle
        {
            get
            {
                return _Detalle;
            }
            set
            {
                SetPropertyValue("Detalle", ref _Detalle, value);
            }
        }


        [Appearance("Estado", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public Estado Estado
        {
            get { return _Estado; }
            set { SetPropertyValue("Estado", ref _Estado, value); }
        }
        
        [Appearance("EstadoAnterior", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public EstadoAnterior EstadoAnterior
        {
            get { return _EstadoAnterior; }
            set { SetPropertyValue("EstadoAnterior", ref _EstadoAnterior, value); }
        }

        [NonPersistent]
        [Appearance("Tipo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        [Appearance("EstadoActivoGuardado", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public EstadoActivoGuardado EstadoActivoGuardado
        {
            get { return _EstadoActivoGuardado; }
            set { SetPropertyValue("EstadoActivoGuardado", ref _EstadoActivoGuardado, value); }
        }

        [Appearance("Asignacion-Activo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Association("Asignacion-Activo")]
        public XPCollection<Asignacion> Asignacion
        {
            get { return GetCollection<Asignacion>("Asignacion"); }
        }

        [Appearance("Traslado-Activo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Association("Traslado-Activo")]
        public XPCollection<Traslado> Traslado
        {
            get { return GetCollection<Traslado>("Traslado"); }
        }

        [Appearance("Descargo-Activo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Association("Descargo-Activo")]
        public XPCollection<Descargo> Descargo
        {
            get { return GetCollection<Descargo>("Descargo"); }
        }

  

        [Appearance("DepreciadoTotalmente", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public bool DepreciadoTotalmente
        {
            get { return _DepreciadoTotalmente; }
            set { SetPropertyValue("DepreciadoTotalmente", ref _DepreciadoTotalmente, value); }
        }

        [Appearance("Activo-HistorialMovimientos", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators')")]
        [Association("Activo-HistorialMovimientos")]
        public XPCollection<HistorialMovimientos> HistorialMovimientos
        {
            get { return GetCollection<HistorialMovimientos>("HistorialMovimientos"); }
        }

     

        protected override void  OnSaving()
        {
        
            //if (EstadoActivoGuardado == 0)
            //{
            //    EstadoActivoGuardado = EstadoActivoGuardado.Guardado;
            //    Correlativo = ObtenerCorrelativo();
            //    if (ValorInicial < 600)
            //        ValorActual = ValorInicial;
            //}



            if (VidaUtil > 0 && ValorInicial > 0)
            {
                switch (Tipo)
                {
                    case "Equipo":
                        break;
                    case "Vehiculo":

                        break;
                    case "Software":
                        break;
                    default:
                        break;
                }
            }
           
        
            if (ReferenceEquals(FechaDeIngreso, null)) { this.FechaDeIngreso = DateTime.Now; }
 	        base.OnSaving();
        }

    }
}
