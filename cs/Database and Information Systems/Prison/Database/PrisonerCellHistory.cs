using System;

namespace PrisonORM.Database
{
    public class PrisonerCellHistory
    {
        public int PrisonerCellHistory_id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Cell Cell { get; set; }
        public Prisoner Prisoner { get; set; }

        public override string ToString()
        {
            return
                "#" + PrisonerCellHistory_id + ", " +
                "V cele od: " + StartDate.ToShortDateString() + ", " +
                "V cele do: " + EndDate.ToShortDateString() + ", " +
                "Cela #" + Cell.Cell_id + ", " +
                "Vězeň #" + Prisoner.Prisoner_id;
        }
    }
}
