using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System.Configuration;
using SIAF.Module.BusinessObjects.Seguridad;

namespace SIAF.Module.BusinessObjects
{
    public static class XpoHelper
    {
        public static Session GetNewSession() { return new Session(DataLayer); }

        public static UnitOfWork GetNewUnitOfWork() { return new UnitOfWork(DataLayer); }

        private readonly static object lockObject = new object();
        static volatile IDataLayer fDataLayer;

        static IDataLayer DataLayer
        {
            get
            {
                if (fDataLayer == null)
                    lock (lockObject)
                        if (fDataLayer == null)
                            fDataLayer = GetDataLayer();
                return fDataLayer;
            }
        }

        private static IDataLayer GetDataLayer()
        {
            XpoDefault.Session = null;
            string conn = Conexion.ObternerConexion();
            XPDictionary dict = new ReflectionDictionary();
            IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);
            dict.GetDataStoreSchema(typeof(HistorialDeLogueo).Assembly);
            IDataLayer dl = new ThreadSafeDataLayer(dict, store);
            return dl;
        }
    }
}