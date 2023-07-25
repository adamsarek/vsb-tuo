using Common.Class;
using System.Collections.ObjectModel;

namespace DataLayerInterface
{
    public interface IVisitorData
    {
        int Insert(Visitor visitor);
        int Update(Visitor visitor);
        Collection<Visitor> Select();
        Visitor Select(int visitorId);
        Visitor SelectForVisit(int visitId);
    }
}
