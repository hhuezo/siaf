using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.DC;

namespace SIAF.Module.BusinessObjects
{
    public enum EstadoAnterior
    {
        Nuevo = 0,
        [XafDisplayNameAttribute("Nuevo Enviado")]
        NuevoEnviado = 1,
        Modificado = 2,
        [XafDisplayNameAttribute("Modificado Enviado")]
        ModificadoEnviado = 3,
        Traslado = 4,
        [XafDisplayNameAttribute("Traslado Enviado")]
        TrasladoEnviado = 5,
        Descargo = 6,
        [XafDisplayNameAttribute("Descargo Enviado")]
        DescargoEnviado = 7,
        Recibido = 8,
        Pendiente = 9,
        Asignado = 10
    }
}
