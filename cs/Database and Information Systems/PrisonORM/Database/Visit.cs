using System;

namespace PrisonORM.Database
{
    class Visit
    {
        public int Visit_id { get; set; }
        public DateTime VisitDate { get; set; }
        public char Allowed { get; set; }
        public Prisoner Prisoner { get; set; }
        public Visitor Visitor { get; set; }

        public override string ToString()
        {
            return
                "Visit ID: " + Visit_id + ", " +
                "Visit date: " + VisitDate.ToShortDateString() + ", " +
                "Allowed: " + Allowed + ", " +
                "Prisoner ID: " + Prisoner.Prisoner_id + ", " +
                "Visitor ID: " + Visitor.Visitor_id;
        }
    }
}
