using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;

namespace SIAF.Module.BusinessObjects
{
    public enum Trimestre
    {
        [XafDisplayNameAttribute("Trimestre I")]
        Trimestre1 = 0,
        [XafDisplayNameAttribute("Trimestre II")]
        Trimestre2 = 1,
        [XafDisplayNameAttribute("Trimestre III")]
        Trimestre3 = 2,
        [XafDisplayNameAttribute("Trimestre IV")]
        Trimestre4 = 3
    }
}