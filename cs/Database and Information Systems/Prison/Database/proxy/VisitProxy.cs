using System.Collections.ObjectModel;
using System.Configuration;

namespace PrisonORM.Database.proxy
{
    abstract class VisitProxy
    {
        private static VisitProxy instance
        {
            get
            {
                return new mssql.VisitTable();
            }
        }

        protected abstract DatabaseProxy insert(Visit visit, DatabaseProxy pDb = null);
        protected abstract int update(Visit visit, DatabaseProxy pDb = null);
        protected abstract Collection<Visit> select(DatabaseProxy pDb = null);
        protected abstract Visit select(int visit_id, DatabaseProxy pDb = null);
        
        public static DatabaseProxy Insert(Visit visit, DatabaseProxy pDb = null)
        {
            return instance.insert(visit, pDb);
        }

        public static int Update(Visit visit, DatabaseProxy pDb = null)
        {
            return instance.update(visit, pDb);
        }

        public static Collection<Visit> Select(DatabaseProxy pDb = null)
        {
            return instance.select(pDb);
        }

        public static Visit Select(int visit_id, DatabaseProxy pDb = null)
        {
            return instance.select(visit_id, pDb);
        }
    }
}
