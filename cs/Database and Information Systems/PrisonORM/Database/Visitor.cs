using System;

namespace PrisonORM.Database
{
    class Visitor
    {
        public int Visitor_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public char Active { get; set; }
        public char Forbidden { get; set; }

        public override string ToString()
        {
            return
                "Visitor ID: " + Visitor_id + ", " +
                "First name: " + FirstName + ", " +
                "Last name: " + LastName + ", " +
                "Gender: " + Gender + ", " +
                "Birth date: " + BirthDate.ToShortDateString() + ", " +
                "Active: " + Active + ", " +
                "Forbidden: " + Forbidden;
        }
    }
}
