using System;

namespace PrisonORM.Database
{
    public class Visit
    {
        public int Visit_id { get; set; }
        public DateTime VisitDate { get; set; }
        public char Allowed { get; set; }
        public Prisoner Prisoner { get; set; }
        public Visitor Visitor { get; set; }

        public string FullAllowed { get { return (Allowed == '0' ? "Nepovolená" : (Allowed == '1' ? "Povolená" : "Nerozhodnutá")); } }

        public override string ToString()
        {
            return
                "#" + Visit_id + ", " +
                "Datum návštěvy: " + VisitDate.ToShortDateString() + ", " +
                "Povolení: " + FullAllowed + ", " +
                "Vězeň #" + Prisoner.Prisoner_id + ", " +
                "Návštěvník #" + Visitor.Visitor_id;
        }
    }
}
