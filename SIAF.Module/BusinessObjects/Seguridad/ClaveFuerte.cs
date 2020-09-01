using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security;
using System.Text.RegularExpressions;

namespace SIAF.Module.BusinessObjects
{
    [CodeRule]
    public class ClaveFuerte : RuleBase<ChangePasswordOnLogonParameters>
    {
        public ClaveFuerte() : base("", "ChangePassword")
        { this.Properties.SkipNullOrEmptyValues = false; }

        public ClaveFuerte(IRuleBaseProperties properties) : base(properties) { }
        protected override bool IsValidInternal(ChangePasswordOnLogonParameters target, out string errorMessageTemplate)
        {
            if (CalcularClaveFuerte(target.NewPassword) < 5)
            {
                errorMessageTemplate = 
                    "La clave no es lo suficientemente fuerte.\r\n" +
                    "Debe ser de más de 6 caracteres y utilizar una combinación de mayúsculas, minúsculas, números y símbolos.";
                return false;
            }
            errorMessageTemplate = string.Empty;
            return true;
        }
        private int CalcularClaveFuerte(string pwd)
        {
            int Fuerza = 0;
            if (pwd == null) return Fuerza;
                
            Regex Mayusculas = new Regex("[A-Z]");
            Regex Minusculas = new Regex("[a-z]");
            Regex Numeros = new Regex("[0-9]");
            Regex Especiales = new Regex("[^a-zA-Z0-9]");

            if (pwd.Length > 6)
                ++Fuerza;
            Match match = Mayusculas.Match(pwd);
            if (match.Success)
                ++Fuerza;
            match = Minusculas.Match(pwd);
            if (match.Success)
                ++Fuerza;
            match = Numeros.Match(pwd);
            if (match.Success)
                ++Fuerza;
            match = Especiales.Match(pwd);
            if (match.Success)
                ++Fuerza;

            return Fuerza;
        }
    }
}
