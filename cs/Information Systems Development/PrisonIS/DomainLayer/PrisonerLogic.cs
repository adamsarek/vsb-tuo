using Common.Class;
using DataLayerProxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DomainLayer
{
    public class PrisonerLogic
    {
        private CellLogic cellLogic = new CellLogic();
        private VisitLogic visitLogic = new VisitLogic();

        private PrisonerProxy prisonerProxy = new PrisonerProxy();

        private Dictionary<string, string> exceptions = new Dictionary<string, string>()
        {
            { "ERROR_CELL_CANNOT_ADD_MORE_PRISONERS",    "Do této cely není možné přidat další vězně." },
            { "ERROR_PRISONER_INVALID_PUNISHMENT_DATES", "Datum začátku trestu musí být nastaveno na dřívější datum než je nastaveno datum konce trestu." }
        };

        public int Insert(Prisoner prisoner)
        {
            if (prisoner.PunishmentStartDate < prisoner.PunishmentEndDate)
            {
                if (prisoner.Cell.Occupied + 1 <= prisoner.Cell.Capacity)
                {
                    // Increment occupation of cell into which is the prisoner added
                    prisoner.Cell.Occupied++;
                    cellLogic.Update(prisoner.Cell);

                    return prisonerProxy.Insert(prisoner);
                }
                throw new Exception(exceptions["ERROR_CELL_CANNOT_ADD_MORE_PRISONERS"]);
            }
            throw new Exception(exceptions["ERROR_PRISONER_INVALID_PUNISHMENT_DATES"]);
        }

        public int Update(Prisoner prisoner)
        {
            if (prisoner.PunishmentStartDate < prisoner.PunishmentEndDate)
            {
                Prisoner oldPrisoner = prisonerProxy.Select(prisoner.PrisonerId);
                if (oldPrisoner.Released == '0' && prisoner.Released == '1')
                {
                    // Decrement occupation of cell from which is the prisoner removed
                    prisoner.Cell.Occupied--;
                    cellLogic.Update(prisoner.Cell);

                    // Disallow future visits of the prisoner
                    Collection<Visit> prisonerVisits = visitLogic.SelectForPrisoner(prisoner.PrisonerId);
                    foreach (Visit prisonerVisit in prisonerVisits)
                    {
                        if (prisonerVisit.VisitDate > DateTime.Today)
                        {
                            prisonerVisit.Allowed = '0';
                            visitLogic.Update(prisonerVisit);
                        }
                    }

                    return prisonerProxy.Update(prisoner);
                }
                else
                {
                    if (oldPrisoner.Cell.CellId == prisoner.Cell.CellId)
                    {
                        return prisonerProxy.Update(prisoner);
                    }
                    else if (prisoner.Cell.Occupied + 1 <= prisoner.Cell.Capacity)
                    {
                        // Decrement occupation of cell from which is the prisoner removed
                        oldPrisoner.Cell.Occupied--;
                        cellLogic.Update(oldPrisoner.Cell);

                        // Increment occupation of cell into which is the prisoner added
                        prisoner.Cell.Occupied++;
                        cellLogic.Update(prisoner.Cell);

                        return prisonerProxy.Update(prisoner);
                    }
                    throw new Exception(exceptions["ERROR_CELL_CANNOT_ADD_MORE_PRISONERS"]);
                }
            }
            throw new Exception(exceptions["ERROR_PRISONER_INVALID_PUNISHMENT_DATES"]);
        }

        public Collection<Prisoner> Select()
        {
            Collection<Prisoner> prisoners = prisonerProxy.Select();
            for (int i = 0; i < prisoners.Count; i++)
            {
                if (prisoners[i].Released == '1')
                {
                    prisoners.Remove(prisoners[i]);
                    i--;
                }
            }

            return prisoners;
        }

        public Prisoner Select(int prisonerId)
        {
            return prisonerProxy.Select(prisonerId);
        }

        public Collection<Prisoner> SelectForCell(int cellId)
        {
            Collection<Prisoner> prisoners = prisonerProxy.SelectForCell(cellId);
            for (int i = 0; i < prisoners.Count; i++)
            {
                if (prisoners[i].Released == '1')
                {
                    prisoners.Remove(prisoners[i]);
                    i--;
                }
            }

            return prisoners;
        }

        public Prisoner SelectForVisit(int visitId)
        {
            return prisonerProxy.SelectForVisit(visitId);
        }

        public int Release(Prisoner prisoner)
        {
            // Release the prisoner
            prisoner.Released = '1';

            return Update(prisoner);
        }
    }
}
