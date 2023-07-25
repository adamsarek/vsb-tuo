using Common.Class;
using System.Collections.ObjectModel;

namespace DataLayerInterface
{
    public interface IVisitData
    {
        int Insert(Visit visit);
        int Update(Visit visit);
        Collection<Visit> Select();
        Visit Select(int visitId);
        Collection<Visit> SelectForPrisoner(int prisonerId);
        Collection<Visit> SelectForVisitor(int visitorId);
    }
}
