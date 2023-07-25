using Common.Class;
using DataLayerProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DomainLayer
{
    public class CellLogic
    {
        private CellProxy cellProxy = new CellProxy();

        private Dictionary<string, string> exceptions = new Dictionary<string, string>()
        {
            { "ERROR_CELL_TOO_LOW_CAPACITY",              "Kapacita cely musí být alespoň 1." },
            { "ERROR_CELL_TOO_LOW_OCCUPIED",              "Počet obsazených míst cely musí být alespoň 0." },
            { "ERROR_CELL_OCCUPIED_BIGGER_THAN_CAPACITY", "Počet obsazených míst cely musí být menší nebo rovno její kapacitě." }
        };

        public int Insert(Cell cell)
        {
            if (cell.Occupied >= 0)
            {
                if (cell.Occupied <= cell.Capacity)
                {
                    if (cell.Capacity >= 1)
                    {
                        return cellProxy.Insert(cell);
                    }
                    throw new Exception(exceptions["ERROR_CELL_TOO_LOW_CAPACITY"]);
                }
                throw new Exception(exceptions["ERROR_CELL_OCCUPIED_BIGGER_THAN_CAPACITY"]);
            }
            throw new Exception(exceptions["ERROR_CELL_TOO_LOW_OCCUPIED"]);
        }

        public int Update(Cell cell)
        {
            if (cell.Occupied >= 0)
            {
                if (cell.Occupied <= cell.Capacity)
                {
                    if (cell.Capacity >= 1)
                    {
                        return cellProxy.Update(cell);
                    }
                    throw new Exception(exceptions["ERROR_CELL_TOO_LOW_CAPACITY"]);
                }
                throw new Exception(exceptions["ERROR_CELL_OCCUPIED_BIGGER_THAN_CAPACITY"]);
            }
            throw new Exception(exceptions["ERROR_CELL_TOO_LOW_OCCUPIED"]);
        }

        public Collection<Cell> Select()
        {
            return cellProxy.Select();
        }

        public Cell Select(int cellId)
        {
            return cellProxy.Select(cellId);
        }

        public Cell SelectForPrisoner(int prisonerId)
        {
            return cellProxy.SelectForPrisoner(prisonerId);
        }
    }
}
