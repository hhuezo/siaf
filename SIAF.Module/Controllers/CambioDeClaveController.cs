using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIAF.Module.Controllers
{
    public partial class CambioDeClaveController : ViewController<DetailView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            Active["ShouldUseNarrowForm"] = View.ObjectTypeInfo.Type == typeof(ChangePasswordOnLogonParameters);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (WebWindow.CurrentRequestPage != null)
            {
                var htmlForm = WebWindow.CurrentRequestPage.FindControl("Form2") as HtmlForm;
                if (htmlForm != null)
                {
                    htmlForm.Attributes.CssStyle.Add("width", Unit.Percentage(50).ToString());
                    htmlForm.Attributes.CssStyle.Add("margin-left", "auto");
                    htmlForm.Attributes.CssStyle.Add("margin-right", "auto");
                }
            }
        }
    }
}
