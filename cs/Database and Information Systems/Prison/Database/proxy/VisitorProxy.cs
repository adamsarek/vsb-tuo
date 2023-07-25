using System.Collections.ObjectModel;
using System.Configuration;

namespace PrisonORM.Database.proxy
{
    abstract class VisitorProxy
    {
        private static VisitorProxy instance
        {
            get
            {
                return new mssql.VisitorTable();
            }
        }

        protected abstract int insert(Visitor visitor, DatabaseProxy pDb = null);
        protected abstract int update(Visitor visitor, DatabaseProxy pDb = null);
        protected abstract Collection<Visitor> select(DatabaseProxy pDb = null);
        protected abstract Visitor select(int visitor_id, DatabaseProxy pDb = null);
        protected abstract DatabaseProxy updateActivity(int visitor_id, DatabaseProxy pDb = null);
        protected abstract DatabaseProxy forbid(int visitor_id, DatabaseProxy pDb = null);

        public static int Insert(Visitor visitor, DatabaseProxy pDb = null)
        {
            return instance.insert(visitor, pDb);
        }

        public static int Update(Visitor visitor, DatabaseProxy pDb = null)
        {
            return instance.update(visitor, pDb);
        }

        public static Collection<Visitor> Select(DatabaseProxy pDb = null)
        {
            return instance.select(pDb);
        }

        public static Visitor Select(int visitor_id, DatabaseProxy pDb = null)
        {
            return instance.select(visitor_id, pDb);
        }

        public static DatabaseProxy UpdateActivity(int visitor_id, DatabaseProxy pDb = null)
        {
            return instance.updateActivity(visitor_id, pDb);
        }

        public static DatabaseProxy Forbid(int visitor_id, DatabaseProxy pDb = null)
        {
            return instance.forbid(visitor_id, pDb);
        }
    }
}
