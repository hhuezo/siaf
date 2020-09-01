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

namespace SIAF.Module.Controllers
{
    public partial class TrasladoController : ViewController
    {
        public TrasladoController() { InitializeComponent(); }
        protected override void OnActivated() { base.OnActivated(); }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }

        private void Trasladar_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            int i = 0;
            IEnumerable<Traslado> ObjetosSeleccionados = e.SelectedObjects.Cast<Traslado>();
            foreach (Traslado objetoSeleccionado in ObjetosSeleccionados)
            {
                if (objetoSeleccionado.Estado == EstadoMovimiento.Ingresado)
                {
                    foreach (Activo activo in objetoSeleccionado.Activo)
                    {
                        activo.Unidad = objetoSeleccionado.RecibidoPor.Ambiente.Unidad;
                        activo.Ambiente = objetoSeleccionado.RecibidoPor.Ambiente;
                        activo.Empleado = objetoSeleccionado.RecibidoPor;
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

        private void CrearHistorial(Traslado objetoSeleccionado, Activo activo)
        {
            HistorialMovimientos historial = this.ObjectSpace.CreateObject<HistorialMovimientos>();
            historial.Activo = activo;
            historial.TipoDeMovimiento = TipoHistorialMovimientos.Traslado;
            historial.Unidad = objetoSeleccionado.RecibidoPor.Ambiente.Unidad;
            historial.Ambiente = objetoSeleccionado.RecibidoPor.Ambiente;
            historial.Empleado = objetoSeleccionado.RecibidoPor;
        }

        private void MostrarMensaje(SimpleActionExecuteEventArgs e, int i)
        {
            string mensaje="";
            if (i == 0) mensaje = "No se realizaron traslados";
            if (i == 1) mensaje = "Traslado realizado satisfactoriamente";
            if (i > 1) mensaje = i + " traslados realizados satisfactoriamente";
            new WMB.Mensaje(e.ShowViewParameters, Application, mensaje);
        }
    }
}
