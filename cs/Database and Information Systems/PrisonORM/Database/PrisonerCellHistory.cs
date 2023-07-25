using System;

namespace PrisonORM.Database
{
    class PrisonerCellHistory
    {
        public int PrisonerCellHistory_id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Cell Cell { get; set; }
        public Prisoner Prisoner { get; set; }

        public override string ToString()
        {
            return
                "PrisonerCellHistory ID: " + PrisonerCellHistory_id + ", " +
                "Start date: " + StartDate.ToShortDateString() + ", " +
                "End date: " + EndDate.ToShortDateString() + ", " +
                "Cell ID: " + Cell.Cell_id + ", " +
                "Prisoner ID: " + Prisoner.Prisoner_id;
        }
    }
}
