using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;

namespace SIAF.Module.BusinessObjects
{
    public enum TipoHistorialMovimientos
    {
        [XafDisplayNameAttribute("Asignación")]
        Asignacion = 0,
        Traslado = 1,
        Descargo = 2,
        [XafDisplayNameAttribute("Préstamo")]
        Prestamo = 3,
        Mantenimiento = 4
    }
}
