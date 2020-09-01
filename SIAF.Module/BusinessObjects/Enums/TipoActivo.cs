using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;

namespace SIAF.Module.BusinessObjects.Enums
{
    public enum TipoActivo
    {
        [XafDisplayNameAttribute("EQUIPOS")]
        equipos = 0,
        [XafDisplayNameAttribute("VEHICULOS")]
        vehiculos = 1,
        [XafDisplayNameAttribute("SOFTWARE")]
        software = 1,
    }
}
