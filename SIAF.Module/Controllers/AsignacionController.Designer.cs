namespace SIAF.Module.Controllers
{
    partial class AsignacionController
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
            this.Asignar = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // Asignar
            // 
            this.Asignar.Caption = "Asignar";
            this.Asignar.ConfirmationMessage = null;
            this.Asignar.Id = "Asignar";
            this.Asignar.TargetObjectType = typeof(SIAF.Module.BusinessObjects.Asignacion);
            this.Asignar.ToolTip = null;
            this.Asignar.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Asignar_Execute);
            // 
            // AsignacionController
            // 
            this.Actions.Add(this.Asignar);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction Asignar;
    }
}
