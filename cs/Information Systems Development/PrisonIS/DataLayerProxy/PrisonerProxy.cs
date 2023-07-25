using Common;
using Common.Class;
using DataLayerInterface;
using DataLayerMSSQL;
using DataLayerXML;
using System.Collections.ObjectModel;

namespace DataLayerProxy
{
    public class PrisonerProxy : IPrisonerData
    {
        private IPrisonerData instance
        {
            get
            {
                if (Session.DataStorage == Session.Storage.SQL)
                {
                    return new PrisonerTable();
                }
                else
                {
                    return new PrisonerXml();
                }
            }
        }

        public int Insert(Prisoner prisoner)
        {
            return instance.Insert(prisoner);
        }

        public int Update(Prisoner prisoner)
        {
            return instance.Update(prisoner);
        }

        public Collection<Prisoner> Select()
        {
            return instance.Select();
        }

        public Prisoner Select(int prisonerId)
        {
            return instance.Select(prisonerId);
        }

        public Collection<Prisoner> SelectForCell(int cellId)
        {
            return instance.SelectForCell(cellId);
        }

        public Prisoner SelectForVisit(int visitId)
        {
            return instance.SelectForVisit(visitId);
        }
    }
}
