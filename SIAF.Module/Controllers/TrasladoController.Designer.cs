namespace SIAF.Module.Controllers
{
    partial class TrasladoController
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
            this.Trasladar = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // Trasladar
            // 
            this.Trasladar.Caption = "Trasladar";
            this.Trasladar.Id = "Trasladar";
            this.Trasladar.TargetObjectType = typeof(SIAF.Module.BusinessObjects.Traslado);
            this.Trasladar.ToolTip = null;
            this.Trasladar.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Trasladar_Execute);
            // 
            // TrasladoController
            // 
            this.Actions.Add(this.Trasladar);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction Trasladar;
    }
}
