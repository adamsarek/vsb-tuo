using Common.Class;
using DataLayerProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DomainLayer
{
    public class VisitLogic
    {
        private VisitProxy visitProxy = new VisitProxy();

        private Dictionary<string, string> exceptions = new Dictionary<string, string>()
        {
            { "ERROR_PRISONER_RELEASED", "Daný vězeň již byl propuštěn." },
            { "ERROR_VISITOR_FORBIDDEN", "Daný návštěvník je již zakázán." }
        };

        public int Insert(Visit visit)
        {
            if (visit.Prisoner.Released == '0')
            {
                if (visit.Visitor.Forbidden == '0')
                {
                    return visitProxy.Insert(visit);
                }
                throw new Exception(exceptions["ERROR_VISITOR_FORBIDDEN"]);
            }
            throw new Exception(exceptions["ERROR_PRISONER_RELEASED"]);
        }

        public int Update(Visit visit)
        {
            return visitProxy.Update(visit);
        }

        public Collection<Visit> Select()
        {
            return visitProxy.Select();
        }

        public Visit Select(int visitId)
        {
            return visitProxy.Select(visitId);
        }

        public Collection<Visit> SelectForPrisoner(int prisonerId)
        {
            return visitProxy.SelectForPrisoner(prisonerId);
        }

        public Collection<Visit> SelectForVisitor(int visitorId)
        {
            return visitProxy.SelectForVisitor(visitorId);
        }
    }
}
