namespace SIAF.Module.Controllers
{
    partial class DescargoController
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
            this.Descargar = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // Descargar
            // 
            this.Descargar.Caption = "Descargar";
            this.Descargar.ConfirmationMessage = null;
            this.Descargar.Id = "Descargar";
            this.Descargar.TargetObjectType = typeof(SIAF.Module.BusinessObjects.Descargo);
            this.Descargar.ToolTip = null;
            this.Descargar.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Descargar_Execute);
            // 
            // DescargoController
            // 
            this.Actions.Add(this.Descargar);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction Descargar;
    }
}
