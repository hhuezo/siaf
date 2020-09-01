using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using SIAF.Module.BusinessObjects;


namespace SIAF.Module.Controllers
{
    public partial class DescargoController : ViewController
    {
        public DescargoController() { InitializeComponent(); }
        protected override void OnActivated() { base.OnActivated(); }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }

        private void Descargar_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            int i = 0;
            IEnumerable<Descargo> ObjetosSeleccionados = e.SelectedObjects.Cast<Descargo>();
            foreach (Descargo objetoSeleccionado in ObjetosSeleccionados)
            {
                if (objetoSeleccionado.Estado == EstadoMovimiento.Ingresado)
                {
                    foreach (Activo activo in objetoSeleccionado.Activo)
                    {
                        activo.Unidad = null;
                        activo.Ambiente = null;
                        activo.Empleado = null;
                        activo.Estado = Estado.Descargado;
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

        private void CrearHistorial(Descargo objetoSeleccionado, Activo activo)
        {
            HistorialMovimientos historial = this.ObjectSpace.CreateObject<HistorialMovimientos>();
            historial.Activo = activo;
            historial.TipoDeMovimiento = TipoHistorialMovimientos.Descargo;
            historial.Unidad = activo.Unidad;
            historial.Ambiente = activo.Ambiente;
            historial.Empleado = null;
        }

        private void MostrarMensaje(SimpleActionExecuteEventArgs e, int i)
        {
            string mensaje = "";
            if (i == 0) mensaje = "No se realizaron descargos";
            if (i == 1) mensaje = "Descargo realizado satisfactoriamente";
            if (i > 1) mensaje = i + " descargos realizados satisfactoriamente";
            new WMB.Mensaje(e.ShowViewParameters, Application, mensaje);
        }
        
    }
}
