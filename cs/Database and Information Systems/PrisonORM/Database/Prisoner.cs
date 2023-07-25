using System;

namespace PrisonORM.Database
{
    class Prisoner
    {
        public int Prisoner_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime PunishmentStartDate { get; set; }
        public DateTime PunishmentEndDate { get; set; }
        public char Released { get; set; }
        public Cell Cell { get; set; }

        public override string ToString()
        {
            return
                "Prisoner ID: " + Prisoner_id + ", " +
                "First name: " + FirstName + ", " +
                "Last name: " + LastName + ", " +
                "Gender: " + Gender + ", " +
                "Birth date: " + BirthDate.ToShortDateString() + ", " +
                "Punishment start date: " + PunishmentStartDate.ToShortDateString() + ", " +
                "Punishment end date: " + PunishmentEndDate.ToShortDateString() + ", " +
                "Released: " + Released + ", " +
                "Cell ID: " + Cell.Cell_id;
        }
    }
}
