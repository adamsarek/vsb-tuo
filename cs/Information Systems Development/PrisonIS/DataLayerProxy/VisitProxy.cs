using Common;
using Common.Class;
using DataLayerInterface;
using DataLayerMSSQL;
using DataLayerXML;
using System.Collections.ObjectModel;

namespace DataLayerProxy
{
    public class VisitProxy : IVisitData
    {
        private IVisitData instance
        {
            get
            {
                if (Session.DataStorage == Session.Storage.SQL)
                {
                    return new VisitTable();
                }
                else
                {
                    return new VisitXml();
                }
            }
        }

        public int Insert(Visit visit)
        {
            return instance.Insert(visit);
        }

        public int Update(Visit visit)
        {
            return instance.Update(visit);
        }

        public Collection<Visit> Select()
        {
            return instance.Select();
        }

        public Visit Select(int visitId)
        {
            return instance.Select(visitId);
        }

        public Collection<Visit> SelectForPrisoner(int prisonerId)
        {
            return instance.SelectForPrisoner(prisonerId);
        }

        public Collection<Visit> SelectForVisitor(int visitorId)
        {
            return instance.SelectForVisitor(visitorId);
        }
    }
}
