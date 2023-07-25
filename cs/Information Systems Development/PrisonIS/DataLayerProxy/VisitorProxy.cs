using Common;
using Common.Class;
using DataLayerInterface;
using DataLayerMSSQL;
using DataLayerXML;
using System.Collections.ObjectModel;

namespace DataLayerProxy
{
    public class VisitorProxy : IVisitorData
    {
        private IVisitorData instance
        {
            get
            {
                if (Session.DataStorage == Session.Storage.SQL)
                {
                    return new VisitorTable();
                }
                else
                {
                    return new VisitorXml();
                }
            }
        }

        public int Insert(Visitor visitor)
        {
            return instance.Insert(visitor);
        }

        public int Update(Visitor visitor)
        {
            return instance.Update(visitor);
        }

        public Collection<Visitor> Select()
        {
            return instance.Select();
        }

        public Visitor Select(int visitorId)
        {
            return instance.Select(visitorId);
        }

        public Visitor SelectForVisit(int visitId)
        {
            return instance.SelectForVisit(visitId);
        }
    }
}
