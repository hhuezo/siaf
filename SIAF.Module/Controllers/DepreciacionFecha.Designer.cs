namespace SIAF.Module.Controllers
{
    partial class DepreciacionFecha
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
            this.DepreActivo = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // DepreActivo
            // 
            this.DepreActivo.AcceptButtonCaption = null;
            this.DepreActivo.CancelButtonCaption = null;
            this.DepreActivo.Caption = "Depreciacion Activo";
            this.DepreActivo.ConfirmationMessage = null;
            this.DepreActivo.Id = "30e8880e-0094-498c-b5a7-320141e25a9a";
            this.DepreActivo.TargetObjectType = typeof(SIAF.Module.BusinessObjects.Activo);
            this.DepreActivo.ToolTip = null;
            this.DepreActivo.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.DActivo_CustomizePopupWindowParams);
            this.DepreActivo.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DActivo_Execute);
            // 
            // DepreciacionFecha
            // 
            this.Actions.Add(this.DepreActivo);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DepreActivo;

    }
}
