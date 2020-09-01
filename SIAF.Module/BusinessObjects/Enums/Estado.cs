using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;

namespace SIAF.Module.BusinessObjects
{
    public enum Estado
    {
        Disponible = 0,
        Asignado = 1,
        Descargado = 2,
        [XafDisplayNameAttribute("Proceso de Descargo")]
        Proceso = 3
    }
}
