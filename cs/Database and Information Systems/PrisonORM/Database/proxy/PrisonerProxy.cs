using System.Collections.ObjectModel;
using System.Configuration;

namespace PrisonORM.Database.proxy
{
    abstract class PrisonerProxy
    {
        private static PrisonerProxy instance
        {
            get
            {
                return new mssql.PrisonerTable();
            }
        }

        protected abstract DatabaseProxy insert(Prisoner prisoner, DatabaseProxy pDb = null);
        protected abstract DatabaseProxy update(Prisoner prisoner, DatabaseProxy pDb = null);
        protected abstract Collection<Prisoner> select(DatabaseProxy pDb = null);
        protected abstract Prisoner select(int prisoner_id, DatabaseProxy pDb = null);
        protected abstract DatabaseProxy release(int prisoner_id, DatabaseProxy pDb = null);

        public static DatabaseProxy Insert(Prisoner prisoner, DatabaseProxy pDb = null)
        {
            return instance.insert(prisoner, pDb);
        }

        public static DatabaseProxy Update(Prisoner prisoner, DatabaseProxy pDb = null)
        {
            return instance.update(prisoner, pDb);
        }

        public static Collection<Prisoner> Select(DatabaseProxy pDb = null)
        {
            return instance.select(pDb);
        }

        public static Prisoner Select(int prisoner_id, DatabaseProxy pDb = null)
        {
            return instance.select(prisoner_id, pDb);
        }

        public static DatabaseProxy Release(int prisoner_id, DatabaseProxy pDb = null)
        {
            return instance.release(prisoner_id, pDb);
        }
    }
}
