using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace SIAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class Costeo : Entidad
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Costeo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }


        // Fields...
        private int _VidaUtil;
        private bool _Finalizado;
        private FileData _Archivo;
        private string _Detalle;
        private decimal _Monto;
        private DateTime _Fecha;
        private Software _Software;

        [Association("Software-Costeo")]
        public Software Software
        {
            get
            {
                return _Software;
            }
            set
            {
                SetPropertyValue("Software", ref _Software, value);
            }
        }

        [Appearance("Fecha", Enabled = false, Criteria = "!IsCurrentUserInRole('Administrators') and Finalizado <> true")]
        [RuleRequiredField]
        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }
            set
            {
                SetPropertyValue("Fecha", ref _Fecha, value);
            }
        }


         [RuleRequiredField]
        public decimal Monto
        {
            get
            {
                return _Monto;
            }
            set
            {
                SetPropertyValue("Monto", ref _Monto, value);
            }
        }

         [RuleRequiredField]
        [Size(500)]
        public string Detalle
        {
            get
            {
                return _Detalle;
            }
            set
            {
                SetPropertyValue("Detalle", ref _Detalle, value);
            }
        }



        public FileData Archivo
        {
            get
            {
                return _Archivo;
            }
            set
            {
                SetPropertyValue("Archivo", ref _Archivo, value);
            }
        }


        public int VidaUtil
        {
            get
            {
                return _VidaUtil;
            }
            set
            {
                SetPropertyValue("VidaUtil", ref _VidaUtil, value);
            }
        }


        [Appearance("CorrelativoInt", Visibility = ViewItemVisibility.Hide, Criteria = "!IsCurrentUserInRole('Administrators')")]
        public bool Finalizado
        {
            get
            {
                return _Finalizado;
            }
            set
            {
                SetPropertyValue("Finalizado", ref _Finalizado, value);
            }
        }


        private Boolean ValidarFecha(DateTime FechaDeAdquisicion, DateTime FechaCosteo)
        {
            bool retorno = true;
            if (FechaDeAdquisicion > FechaCosteo)
            {
                retorno = false;
            }

            return retorno;

        }


        protected override void OnSaving()
        {
            if (!ReferenceEquals(Software, null))
            {
                if (!ReferenceEquals(Fecha, null))
                {
                    bool BoolFecha = ValidarFecha(Software.FechaDeAdquisicion, Fecha);


                 

                    BinaryOperator BinarySoftware = new BinaryOperator("Software", Software);
                    BinaryOperator BinaryFinalizado = new BinaryOperator("Finalizado", true, BinaryOperatorType.NotEqual);
                    BinaryOperator BinaryOid = new BinaryOperator("Oid", this.Oid, BinaryOperatorType.NotEqual);
                    CriteriaOperator criteriaFinal = CriteriaOperator.And(BinaryFinalizado, BinarySoftware, BinaryOid);

                    Costeo ObjCosteo = this.Session.FindObject<Costeo>(criteriaFinal);


                    var ListCosteo = Session.Query<Costeo>().Where(f => f.Software.CodigoDeActivo == this.Software.CodigoDeActivo && f.Fecha > this.Fecha).ToList();


                    if (BoolFecha == false)
                    {
                        throw new UserFriendlyException("Error, la fecha del costeo es menos que la fecha de adquisicion");
                    }
                    else if (!ReferenceEquals(ObjCosteo, null))
                    {
                        throw new UserFriendlyException("Error, existe un costeo sin finalizar");
                    }
                    else if (ListCosteo.Count > 0)
                    {
                        throw new UserFriendlyException("Error, Ya existe un costeo con fecha superior a la ingresada");
                    }

                    base.OnSaving();
                }

            }
        }



    }
}
