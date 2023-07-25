using System;

namespace Common.Class
{
    public class Visit
    {
        private Prisoner prisoner = null;
        private Visitor visitor = null;

        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public char Allowed { get; set; }
        public Prisoner Prisoner
        {
            get
            {
                if (prisoner == null)
                {
                    prisoner = new PrisonerLogic().SelectForVisit(VisitId);
                }
                return prisoner;
            }
            set
            {
                prisoner = value;
            }
        }
        public Visitor Visitor
        {
            get
            {
                if (visitor == null)
                {
                    visitor = new VisitorLogic().SelectForVisit(VisitId);
                }
                return visitor;
            }
            set
            {
                visitor = value;
            }
        }

        public string FullAllowed { get { return (Allowed == '0' ? "Nepovolená" : (Allowed == '1' ? "Povolená" : "Nerozhodnutá")); } }
        public string Visitor_FullName { get { return Visitor.FullName; } }
        public string Visitor_FullGender { get { return Visitor.FullGender; } }
        public int Visitor_Age { get { return Visitor.Age; } }

        public override string ToString()
        {
            return
                "#" + VisitId + ", " +
                "Datum návštěvy: " + VisitDate.ToShortDateString() + ", " +
                "Povolení: " + FullAllowed + ", " +
                "Vězeň #" + Prisoner.PrisonerId + ", " +
                "Návštěvník #" + Visitor.VisitorId;
        }
    }
}
