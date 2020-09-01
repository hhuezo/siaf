using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using SIAF.Module.BusinessObjects;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace SIAF.Module.Controllers
{
    public partial class AsignacionController : ViewController
    {
        public AsignacionController() { InitializeComponent(); }
        protected override void OnActivated() { base.OnActivated(); }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }

        private void Asignar_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            int i = 0;
            IEnumerable<Asignacion> ObjetosSeleccionados = e.SelectedObjects.Cast<Asignacion>();
            foreach (Asignacion objetoSeleccionado in ObjetosSeleccionados)
            {
                if (objetoSeleccionado.Estado == EstadoMovimiento.Ingresado)
                {
                    foreach (Activo activo in objetoSeleccionado.Activo)
                    {
                        activo.Unidad = objetoSeleccionado.Empleado.Ambiente.Unidad;
                        activo.Ambiente = objetoSeleccionado.Empleado.Ambiente;
                        activo.Empleado = objetoSeleccionado.Empleado;
                        CrearHistorial(objetoSeleccionado, activo);
                    }
                    objetoSeleccionado.Estado = EstadoMovimiento.Finalizado;
                    i = i + 1;
                }
            }
            if (this.View.ObjectSpace.IsModified)
                this.View.ObjectSpace.CommitChanges();
            MostrarMensaje(e, i);
        }

        private void CrearHistorial(Asignacion objetoSeleccionado, Activo activo)
        {
            HistorialMovimientos historial = this.ObjectSpace.CreateObject<HistorialMovimientos>();
            historial.Activo = activo;
            historial.TipoDeMovimiento = TipoHistorialMovimientos.Asignacion;
            historial.Unidad = objetoSeleccionado.Empleado.Ambiente.Unidad;
            historial.Ambiente = objetoSeleccionado.Empleado.Ambiente;
            historial.Empleado = objetoSeleccionado.Empleado;
        }

        private void MostrarMensaje(SimpleActionExecuteEventArgs e, int i)
        {
            string mensaje = "";
            if (i == 0) mensaje = "No se realizaron asignaciones";
            if (i == 1) mensaje = "Asignación realizada satisfactoriamente";
            if (i > 1) mensaje = i + " asignaciones realizadas satisfactoriamente";
            new WMB.Mensaje(e.ShowViewParameters, Application, mensaje);
        }
    }
}
