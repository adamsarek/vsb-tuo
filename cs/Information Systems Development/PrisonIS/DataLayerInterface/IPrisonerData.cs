using Common.Class;
using System.Collections.ObjectModel;

namespace DataLayerInterface
{
    public interface IPrisonerData
    {
        int Insert(Prisoner prisoner);
        int Update(Prisoner prisoner);
        Collection<Prisoner> Select();
        Prisoner Select(int prisonerId);
        Collection<Prisoner> SelectForCell(int cellId);
        Prisoner SelectForVisit(int visitId);
    }
}
