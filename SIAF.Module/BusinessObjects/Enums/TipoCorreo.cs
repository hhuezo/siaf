using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;

namespace SIAF.Module.BusinessObjects
{
    public enum TipoCorreo
    {
        [XafDisplayNameAttribute("Reseteo de clave")]
        ReseteoDeClave = 0,
        [XafDisplayNameAttribute("Informativo")]
        Informativo = 1,
        [XafDisplayNameAttribute("Otro")]
        Otro = 2
    }
}