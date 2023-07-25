using Common.Class;
using System.Collections.ObjectModel;

namespace DataLayerInterface
{
    public interface ICellData
    {
        int Insert(Cell cell);
        int Update(Cell cell);
        Collection<Cell> Select();
        Cell Select(int cellId);
        Cell SelectForPrisoner(int prisonerId);
    }
}
