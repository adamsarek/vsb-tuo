using Common;
using Common.Class;
using DataLayerInterface;
using DataLayerMSSQL;
using DataLayerXML;
using System.Collections.ObjectModel;

namespace DataLayerProxy
{
    public class CellProxy : ICellData
    {
        private ICellData instance
        {
            get
            {
                if (Session.DataStorage == Session.Storage.SQL)
                {
                    return new CellTable();
                }
                else
                {
                    return new CellXml();
                }
            }
        }

        public int Insert(Cell cell)
        {
            return instance.Insert(cell);
        }

        public int Update(Cell cell)
        {
            return instance.Update(cell);
        }

        public Collection<Cell> Select()
        {
            return instance.Select();
        }

        public Cell Select(int cellId)
        {
            return instance.Select(cellId);
        }

        public Cell SelectForPrisoner(int prisonerId)
        {
            return instance.SelectForPrisoner(prisonerId);
        }
    }
}
