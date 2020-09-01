namespace SIAF.Module.Controllers
{
    partial class AmortizacionController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Amortizacion = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.Costeo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // Amortizacion
            // 
            this.Amortizacion.Caption = "Amortizacion";
            this.Amortizacion.ConfirmationMessage = null;
            this.Amortizacion.Id = "413a76cd-f762-435e-bfce-b9fbed645723";
            this.Amortizacion.TargetObjectsCriteria = "(Licencia<>true and Guardado <> true) ";
            this.Amortizacion.TargetObjectType = typeof(SIAF.Module.BusinessObjects.Software);
            this.Amortizacion.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.Amortizacion.ToolTip = null;
            this.Amortizacion.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Amortizacion.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Amortizacion_Execute);
            // 
            // Costeo
            // 
            this.Costeo.Caption = "Guardar costeo";
            this.Costeo.ConfirmationMessage = null;
            this.Costeo.Id = "1a0ad266-20b4-4097-a2e9-435e0a788455";
            this.Costeo.TargetObjectsCriteria = "Finalizado <> true";
            this.Costeo.TargetObjectType = typeof(SIAF.Module.BusinessObjects.Costeo);
            this.Costeo.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.Costeo.ToolTip = null;
            this.Costeo.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.Costeo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Costeo_Execute);
            // 
            // AmortizacionController
            // 
            this.Actions.Add(this.Amortizacion);
            this.Actions.Add(this.Costeo);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction Amortizacion;
        private DevExpress.ExpressApp.Actions.SimpleAction Costeo;
    }
}
