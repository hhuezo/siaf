using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;

namespace SIAF.Module.BusinessObjects.Enums
{
    public enum Mayor600
    {
        [XafDisplayNameAttribute("No Aplica")]
        NoAplica = 0,
        [XafDisplayNameAttribute("Mayor a 600")]
        Mayor = 1,
        [XafDisplayNameAttribute("Menor a 600")]
        Menor = 2,
    }
}
