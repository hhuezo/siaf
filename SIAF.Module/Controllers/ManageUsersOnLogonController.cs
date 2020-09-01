using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Templates;

namespace Security.Extensions {
    public class ManageUsersOnLogonController : ViewController<DetailView> {
        protected const string LogonActionParametersActiveKey = "Active for ILogonActionParameters only";
        public SimpleAction RegisterUserAction { get; private set; }
        public SimpleAction RestorePasswordAction { get; private set; }
        public SimpleAction AcceptLogonParametersAction { get; private set; }
        public SimpleAction CancelLogonParametersAction { get; private set; }
        public ManageUsersOnLogonController() {
            //RegisterUserAction = CreateLogonParametersAction("RegisterUser", "RegisterUserCategory", "Registrar usuario", "BO_User", "Registrar un nuevo usuario en el sistema", typeof(RegisterUserParameters));
            RestorePasswordAction = CreateLogonParametersAction("RestorePassword", "RestorePasswordCategory", "Restaurar clave", "Action_ResetPassword", "Restaurar clave olvidada", typeof(RestorePasswordParameters));
            AcceptLogonParametersAction = new SimpleAction(this, "AcceptLogonParameters", "PopupActions", (s, e) => {
                AcceptParameters(e.CurrentObject as LogonActionParametersBase, e);
            }) { Caption = "Enviar nueva clave al correo" };
            CancelLogonParametersAction = new SimpleAction(this, "CancelLogonParameters", "PopupActions", (s, e) => {
                CancelParameters(e.CurrentObject as LogonActionParametersBase);
            }) { Caption = "Cancelar" };
        }
        //Controller activo solo cuando el usuario no está logueado
        protected override void OnViewChanging(View view) {
            base.OnViewChanging(view);
            Active[ControllerActiveKey] = !SecuritySystem.IsAuthenticated;
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            bool isLogonParametersActionView = (View != null) && (View.ObjectTypeInfo != null) && View.ObjectTypeInfo.Implements<LogonActionParametersBase>();
            LogonController lc = Frame.GetController<LogonController>();
            if (lc != null)
            {
                lc.AcceptAction.Active[LogonActionParametersActiveKey] = !isLogonParametersActionView;
                lc.CancelAction.Active[LogonActionParametersActiveKey] = !isLogonParametersActionView;
            }
            AcceptLogonParametersAction.Active[LogonActionParametersActiveKey] = isLogonParametersActionView;
            CancelLogonParametersAction.Active[LogonActionParametersActiveKey] = isLogonParametersActionView;
        }
        private SimpleAction CreateLogonParametersAction(string id, string category, string caption, string imageName, string toolTip, Type parametersType) {
            SimpleAction action = new SimpleAction(this, id, category);
            action.Caption = caption;
            action.ImageName = imageName;
            action.PaintStyle = ActionItemPaintStyle.Image;
            action.ToolTip = toolTip;
            action.Execute += (s, e) => CreateParametersViewCore(e);
            action.Tag = parametersType;
            return action;
        }
        protected virtual void CreateParametersViewCore(SimpleActionExecuteEventArgs e) {
            Type parametersType = e.Action.Tag as Type;
            Guard.ArgumentNotNull(parametersType, "parametersType");
            object logonActionParameters = Activator.CreateInstance(parametersType);
            DetailView dv = Application.CreateDetailView(ObjectSpaceInMemory.CreateNew(), logonActionParameters);
            dv.ViewEditMode = ViewEditMode.Edit;
            e.ShowViewParameters.CreatedView = dv;
            e.ShowViewParameters.Context = TemplateContext.PopupWindow;
            //WinForms:
            //e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            //ASP.NET:
            e.ShowViewParameters.TargetWindow = TargetWindow.Current;
        }
        protected virtual void AcceptParameters(LogonActionParametersBase parameters, SimpleActionExecuteEventArgs e)
        {
         /*   Guard.ArgumentNotNull(parameters, "parameters");
            parameters.ExecuteBusinessLogic(Application.CreateObjectSpace());
            RegisterUserParameters registerUserParameters = parameters as RegisterUserParameters;
            if (registerUserParameters != null)
            {
                LogonController lc = Frame.GetController<LogonController>();
                if (lc != null)
                {
                    lc.AcceptAction.Active.RemoveItem(LogonActionParametersActiveKey);
                    AuthenticationStandardLogonParameters securitySystemLogonParameters = (AuthenticationStandardLogonParameters)SecuritySystem.LogonParameters;
                    securitySystemLogonParameters.UserName = registerUserParameters.Usuario;
                    securitySystemLogonParameters.Password = registerUserParameters.Password;
                    lc.AcceptAction.DoExecute();
                }
            }
            else
                CloseParametersView();*/
        }
        protected virtual void CancelParameters(LogonActionParametersBase parameters) { CloseParametersView(); }

        protected virtual void CloseParametersView() {
            View.Close(false);
            Application.LogOff();            
        }
    }
}