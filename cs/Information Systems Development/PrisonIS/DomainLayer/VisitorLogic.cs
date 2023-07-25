using Common.Class;
using DataLayerProxy;
using System;
using System.Collections.ObjectModel;

namespace DomainLayer
{
    public class VisitorLogic
    {
        private VisitLogic visitLogic = new VisitLogic();

        private VisitorProxy visitorProxy = new VisitorProxy();

        public int Insert(Visitor visitor)
        {
            return visitorProxy.Insert(visitor);
        }

        public int Update(Visitor visitor)
        {
            return visitorProxy.Update(visitor);
        }

        public Collection<Visitor> Select()
        {
            return visitorProxy.Select();
        }

        public Collection<Visitor> SelectAllowed()
        {
            Collection<Visitor> visitors = visitorProxy.Select();
            for (int i = 0; i < visitors.Count; i++)
            {
                if (visitors[i].Forbidden == '1')
                {
                    visitors.Remove(visitors[i]);
                    i--;
                }
            }

            return visitors;
        }

        public Visitor Select(int visitorId)
        {
            return visitorProxy.Select(visitorId);
        }

        public Visitor SelectForVisit(int visitId)
        {
            return visitorProxy.SelectForVisit(visitId);
        }

        public int Forbid(Visitor visitor)
        {
            // Disallow future visits by the visitor
            Collection<Visit> visitorVisits = visitLogic.SelectForVisitor(visitor.VisitorId);
            foreach (Visit visitorVisit in visitorVisits)
            {
                if (visitorVisit.VisitDate > DateTime.Today)
                {
                    visitorVisit.Allowed = '0';
                    visitLogic.Update(visitorVisit);
                }
            }

            // Forbid the visitor
            visitor.Forbidden = '1';

            return visitorProxy.Update(visitor);
        }
    }
}
