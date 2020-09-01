namespace SIAF.Module.Controllers
{
    partial class RolController
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
            this.GenerarPerfil = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // GenerarPerfil
            // 
            this.GenerarPerfil.AcceptButtonCaption = null;
            this.GenerarPerfil.CancelButtonCaption = null;
            this.GenerarPerfil.Caption = "Generar Perfil";
            this.GenerarPerfil.ConfirmationMessage = null;
            this.GenerarPerfil.Id = "GenerarPerfil";
            this.GenerarPerfil.ToolTip = null;
            this.GenerarPerfil.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.GenerarPerfil_CustomizePopupWindowParams);
            this.GenerarPerfil.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.GenerarPerfil_Execute);
            // 
            // RolController
            // 
            this.Actions.Add(this.GenerarPerfil);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction GenerarPerfil;
    }
}
