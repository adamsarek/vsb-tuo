using System;

namespace PrisonORM.Database
{
    class Employee
    {
        public int Employee_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public char Warden { get; set; }
        public char Fired { get; set; }
        public Prison Prison { get; set; }

        public override string ToString()
        {
            return
                "Employee ID: " + Employee_id + ", " +
                "First name: " + FirstName + ", " +
                "Last name: " + LastName + ", " +
                "Gender: " + Gender + ", " +
                "Birth date: " + BirthDate.ToShortDateString() + ", " +
                "Warden: " + Warden + ", " +
                "Fired: " + Fired + ", " +
                "Prison ID: " + Prison.Prison_id;
        }
    }
}
