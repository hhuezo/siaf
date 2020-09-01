using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Xpo;

namespace SIAF.Module.BusinessObjects.Seguridad
{
    public static class AuditTrailConfig
    {
        public static void Initialize()
        {
            AuditTrailService.Instance.SaveAuditTrailData += Instance_SaveAuditTrailData;
            IAuditTimestampStrategy timeStampStrategy = new TimestampStrategy();
            AuditTrailService.Instance.TimestampStrategy = timeStampStrategy;
        }

        static void Instance_SaveAuditTrailData(object sender, SaveAuditTrailDataEventArgs e)
        {
            List<AuditDataItem> toDelete = new List<AuditDataItem>();
            foreach (var item in e.AuditTrailDataItems)
            {
                if (item.OperationType == AuditOperationType.ObjectChanged || item.OperationType == AuditOperationType.ObjectDeleted) continue;
                toDelete.Add(item);
            }
            foreach (var item in toDelete)
            {
                e.AuditTrailDataItems.Remove(item);
            }
        }
    }

    public class TimestampStrategy : IAuditTimestampStrategy
    {
        DateTime cachedTimestamp;
        #region IAuditTimestampStrategy Members
        public DateTime GetTimestamp(AuditDataItem auditDataItem)
        {
            return cachedTimestamp;
        }
        public void OnBeginSaveTransaction(Session session)
        {
            cachedTimestamp = Hora.ObtenerHora();
        }
        #endregion
    }
}
