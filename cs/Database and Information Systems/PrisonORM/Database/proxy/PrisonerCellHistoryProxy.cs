using System.Collections.ObjectModel;
using System.Configuration;

namespace PrisonORM.Database.proxy
{
    abstract class PrisonerCellHistoryProxy
    {
        private static PrisonerCellHistoryProxy instance
        {
            get
            {
                return new mssql.PrisonerCellHistoryTable();
            }
        }

        protected abstract DatabaseProxy insert(PrisonerCellHistory prisonerCellHistory, DatabaseProxy pDb = null);
        protected abstract Collection<PrisonerCellHistory> select(DatabaseProxy pDb = null);
        
        public static DatabaseProxy Insert(PrisonerCellHistory prisonerCellHistory, DatabaseProxy pDb = null)
        {
            return instance.insert(prisonerCellHistory, pDb);
        }

        public static Collection<PrisonerCellHistory> Select(DatabaseProxy pDb = null)
        {
            return instance.select(pDb);
        }
    }
}
