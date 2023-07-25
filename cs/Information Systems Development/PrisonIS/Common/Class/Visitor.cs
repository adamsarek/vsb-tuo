using System;

namespace Common.Class
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public char Forbidden { get; set; }

        public string FullName { get { return FirstName + ' ' + LastName; } }
        public string FullGender { get { return (Gender == 'M' ? "Muž" : "Žena"); } }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age)) { age--; }
                return age;
            }
        }
        public string FullForbidden { get { return (Forbidden == '0' ? "NE" : "ANO"); } }

        public override string ToString()
        {
            return
                "#" + VisitorId + ", " +
                "Jméno: " + FullName + ", " +
                "Pohlaví: " + FullGender + ", " +
                "Věk: " + Age + ", " +
                "Zakázán: " + FullForbidden;
        }
    }
}
