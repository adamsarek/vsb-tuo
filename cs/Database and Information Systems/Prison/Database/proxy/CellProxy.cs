using System.Collections.ObjectModel;
using System.Configuration;

namespace PrisonORM.Database.proxy
{
    abstract class CellProxy
    {
        private static CellProxy instance
        {
            get
            {
                return new mssql.CellTable();
            }
        }

        protected abstract int insert(Cell cell, DatabaseProxy pDb = null);
        protected abstract int update(Cell cell, DatabaseProxy pDb = null);
        protected abstract Collection<Cell> select(DatabaseProxy pDb = null);
        protected abstract Cell select(int cell_id, DatabaseProxy pDb = null);
        
        public static int Insert(Cell cell, DatabaseProxy pDb = null)
        {
            return instance.insert(cell, pDb);
        }

        public static int Update(Cell cell, DatabaseProxy pDb = null)
        {
            return instance.update(cell, pDb);
        }

        public static Collection<Cell> Select(DatabaseProxy pDb = null)
        {
            return instance.select(pDb);
        }

        public static Cell Select(int cell_id, DatabaseProxy pDb = null)
        {
            return instance.select(cell_id, pDb);
        }
    }
}
