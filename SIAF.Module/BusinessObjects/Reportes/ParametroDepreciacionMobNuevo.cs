using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Validation;
using siaf.Module.BusinessObjects.Enums;
using System.Collections.Generic;

namespace SIAF.Module.BusinessObjects.Reportes
{
    [DomainComponent]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/#Xaf/CustomDocument3594.
    public class ParametroDepreciacionMobNuevo : ReportParametersObjectBase
    {
        public ParametroDepreciacionMobNuevo(IObjectSpaceCreator provider) : base(provider) { }
        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(object));
        }
        public override CriteriaOperator GetCriteria()
        {
           /* CriteriaOperator criteriaCuenta = new BinaryOperator("CuentaContable", CuentaContable);
            CriteriaOperator criteriaFuente = new BinaryOperator("Fuente", Fuente);
            CriteriaOperator criteriaEstado = new BinaryOperator("Estado", Estado.Descargado, BinaryOperatorType.NotEqual);*/

            DeleteData();
            GenerateData();

            CriteriaOperator criteriaFinal = null;

           // criteriaFinal = CriteriaOperator.And(criteriaCuenta, criteriaFuente, criteriaEstado);
            return criteriaFinal;
        }
        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("Codigo", SortingDirection.Descending) };
            return sorting;
        }

        [RuleRequiredField]
        public DateTime FechaDepreciacion { get; set; }
        [RuleRequiredField]
        public CuentaContable CuentaContable { get; set; }
        [RuleRequiredField]
        public Fuente Fuente { get; set; }




           private void GenerateData()
           {
               CriteriaOperator criteriaFinal = null;
               CriteriaOperator criteriaFecha = new BinaryOperator("FechaDeAdquisicion", FechaDepreciacion, BinaryOperatorType.LessOrEqual);
               //CriteriaOperator criteriaEstado = new BinaryOperator("Estado", Estado.Descargado, BinaryOperatorType.NotEqual);
               CriteriaOperator criteriaCuenta = new BinaryOperator("CuentaContable", CuentaContable);
               CriteriaOperator criteriaFuente = new BinaryOperator("Fuente", Fuente);
               BinaryOperator Depreciar = new BinaryOperator("DepreciadoTotalmente", false);
               BinaryOperator NoDepreciable = new BinaryOperator("Depresiable", Depresiable.Depresiables);
               BinaryOperator Valor = new BinaryOperator("ValorInicial", 600, BinaryOperatorType.GreaterOrEqual);

               criteriaFinal = CriteriaOperator.And(criteriaCuenta, criteriaFuente,  criteriaFecha);
               ICollection<Equipo> ListadoEquipos = ObjectSpace.GetObjects<Equipo>(criteriaFinal);

               if (!ReferenceEquals(ListadoEquipos, null))
               {
                    foreach (Equipo ObjEquipos in ListadoEquipos)
                   {
                        bool validar = true; DateTime fechaDescargo = Convert.ToDateTime("1970/01/01");
                        if(ObjEquipos.Estado == Estado.Descargado || ObjEquipos.Estado == Estado.Proceso)
                        {
                           ICollection<Descargo> ListadoDescargo = ObjectSpace.GetObjects<Descargo>();
                           foreach (Descargo ObjDescargo in ListadoDescargo)
                           {
                               foreach (Activo activo in ObjDescargo.Activo)
                               {
                                   if (ListadoEquipos == activo && ObjDescargo.FechaDeIngreso > FechaDepreciacion)
                                   {
                                       fechaDescargo = ObjDescargo.FechaDeIngreso;
                                       break;
                                   }
                               }
                           }
                        }

                        if(fechaDescargo != Convert.ToDateTime("1970/01/01"))
                        {
                            validar = false;
                        }
                        else if(fechaDescargo == Convert.ToDateTime("1970/01/01") && (ObjEquipos.Estado == Estado.Proceso || ObjEquipos.Estado == Estado.Descargado))
                        {
                            validar = false;
                        }


                        DateTime FechaEvaluar = ObjEquipos.FechaDeAdquisicion.AddYears(ObjEquipos.VidaUtil);
                        if(validar == true)
                   {
                    

                       ReporteDepreciacion Objreporte = this.ObjectSpace.CreateObject<ReporteDepreciacion>();
                       Objreporte.Eliminar = true;                
                       Objreporte.Codigo = ObjEquipos.CodigoDeActivo;
                       Objreporte.Descripcion = ObjEquipos.SubClase.Descripcion;

                       if(!ReferenceEquals(ObjEquipos.Ambiente,null))
                       {Objreporte.Ubicacion = ObjEquipos.Ambiente.Descripcion;}
                    
                       Objreporte.Marca = ObjEquipos.Marca;
                       Objreporte.Modelo = ObjEquipos.Modelo;
                       Objreporte.Serie = ObjEquipos.Serie;
                       Objreporte.Fuente = Fuente.Descripcion;
                       Objreporte.Cuenta = CuentaContable.Codigo;
                       Objreporte.Fecha = FechaDepreciacion;
                       Objreporte.Adquisicion = ObjEquipos.FechaDeAdquisicion;
                       Objreporte.ValorAdquisicion = Convert.ToDecimal(ObjEquipos.ValorInicial);
                       Objreporte.ValorDepreciacion = Convert.ToDecimal(ObjEquipos.ValorADepreciar);
                       Objreporte.Estado = ObjEquipos.EstadoFisico.Descripcion;
                       Objreporte.DepresiacionAcumulada =  Convert.ToDecimal(ObjEquipos.DepreciacionAcumulada);

                       if (FechaEvaluar < FechaDepreciacion || ObjEquipos.Depresiable == Depresiable.NoDepresiable)
                       {
                           Objreporte.DepreciacionDias = 0;
                           Objreporte.DepreciacionMes = 0;
                           Objreporte.ValorLibro = Convert.ToDecimal(ObjEquipos.ValorInicial - ObjEquipos.ValorADepreciar);

                       }
                       else
                       {
                           int years = (FechaEvaluar.Year - ObjEquipos.FechaDeAdquisicion.Year);
                           FechaEvaluar = FechaDepreciacion;
                           double DepreciacionAnual = ObjEquipos.ValorADepreciar / ObjEquipos.VidaUtil;
                        
                           if (years == 0)
                           {
                               //ultimo dia del año
                               DateTime firstDayAd = new DateTime(ObjEquipos.FechaDeAdquisicion.Year, 01, 01);
                               DateTime lastDayAd = new DateTime(ObjEquipos.FechaDeAdquisicion.Year, 12, 31);

                               //calculo de la depreciacion diaria
                               TimeSpan DiasYear = (lastDayAd - firstDayAd);
                               int DaysYearAd = (DiasYear.Days) + 1;
                               double DepreciacionDiaria = DepreciacionAnual / DaysYearAd;
                               Objreporte.DepreciacionDias = Convert.ToDecimal(DepreciacionDiaria);


                               DateTime PrimerDiaMes = new DateTime(FechaEvaluar.Year, FechaEvaluar.Month, 01);
                               DateTime UltimoDiaMes = PrimerDiaMes.AddMonths(1);
                               TimeSpan DiaMes = (UltimoDiaMes - PrimerDiaMes);
                               int DiasMes = (DiaMes.Days);
                               Objreporte.DepreciacionMes = Convert.ToDecimal(DiasMes * DepreciacionDiaria);
                            

                               //calculo de dias a depreciar
                               TimeSpan DiasDepreciarAd = (FechaEvaluar - ObjEquipos.FechaDeAdquisicion);
                               int DiaDepreciarAd = (DiasDepreciarAd.Days) + 1;
                               Objreporte.ValorLibro = Convert.ToDecimal(ObjEquipos.ValorInicial - (DepreciacionDiaria * DiaDepreciarAd));
                             
                           }
                           else if (years > 0)
                           {
                               //calculo depresicion del año de adquisicion
                               DateTime firstDayAd = new DateTime(ObjEquipos.FechaDeAdquisicion.Year, 01, 01);
                               DateTime lastDayAd = new DateTime(ObjEquipos.FechaDeAdquisicion.Year, 12, 31);
                           
                               //calculo de la depreciacion diaria del año de adquisicion
                               TimeSpan DiasYear = (lastDayAd - firstDayAd);
                               int DaysYearAd = (DiasYear.Days) + 1;
                               double DepreciacionDiaria = DepreciacionAnual / DaysYearAd;

                               //calculo de dias a depreciar del año de adquisicion
                               TimeSpan DiasDepreciarAd = (lastDayAd - ObjEquipos.FechaDeAdquisicion);
                               int DiaDepreciarAd = (DiasDepreciarAd.Days) + 1;
                               double DepreciacionAdd = DepreciacionDiaria * DiaDepreciarAd;


                               int YearCompleto = (FechaEvaluar.Year - ObjEquipos.FechaDeAdquisicion.Year)-1;
                               //depreciacion de años completos
                               double DeprecYearCompleto = DepreciacionAnual * YearCompleto;


                               //DepreciacionAdd del año actual

                               //calculo de la depreciacion diaria
                               DateTime firstDayYearActual = new DateTime(FechaEvaluar.Year, 01, 01);
                               DateTime lastDayYearActual = new DateTime(FechaEvaluar.Year, 12, 31);
                               TimeSpan DiaYearActual = (lastDayYearActual - firstDayYearActual);
                               int DiasYearActual = (DiaYearActual.Days) + 1;
                               double DepreciacionDiariaLastYear = ObjEquipos.DepreciacionAnual / DiasYearActual;

                            


                               //calculo de la depreciacion del año actual
                               TimeSpan TotalDiasYearActual = (FechaEvaluar - firstDayYearActual);
                               int TotalDiaYearActual = (TotalDiasYearActual.Days) + 1;
                               double DepreciaciononLastYear = DepreciacionDiariaLastYear * TotalDiaYearActual;


                               //calculo de dias del ultimo mes evaluado
                               //DateTime FechaInterna = o.FechaDeAdquisicion.AddMonths(i);

                               DateTime PrimerDiaMes = new DateTime(FechaEvaluar.Year, FechaEvaluar.Month, 01);
                               DateTime UltimoDiaMes = PrimerDiaMes.AddMonths(1);
                               TimeSpan DiaMes = (UltimoDiaMes - PrimerDiaMes);
                               int DiasMes = (DiaMes.Days);
                               Objreporte.DepreciacionMes = Convert.ToDecimal(DiasMes * (DepreciacionAnual / DiasYearActual));


                               Objreporte.ValorLibro = Convert.ToDecimal(ObjEquipos.ValorInicial - (DepreciacionAdd + DeprecYearCompleto + DepreciaciononLastYear));
                               Objreporte.DepreciacionDias = Convert.ToDecimal(DepreciacionDiariaLastYear);

                           }
                       }

                   }
                
                   }
                
               }

               this.ObjectSpace.CommitChanges(); 
           }

           private void DeleteData()
           {
               BinaryOperator Eliminar = new BinaryOperator("Eliminar", true);
               bool Seguir = true;
               while (Seguir)
               {
                   if (!ReferenceEquals(this.ObjectSpace.FindObject<ReporteDepreciacion>(Eliminar), null))
                   {
                       this.ObjectSpace.Delete(this.ObjectSpace.FindObject<ReporteDepreciacion>(Eliminar));
                       this.ObjectSpace.CommitChanges();
                   }
                   else
                   {
                       Seguir = false;
                   }
               }

           }

    }
}
