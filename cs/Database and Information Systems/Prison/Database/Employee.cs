using System;

namespace PrisonORM.Database
{
    public class Employee
    {
        public int Employee_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public char Warden { get; set; }
        public char Fired { get; set; }
        public Prison Prison { get; set; }

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
        public string FullWarden { get { return (Warden == '0' ? "NE" : "ANO"); } }
        public string FullFired { get { return (Fired == '0' ? "NE" : "ANO"); } }

        public override string ToString()
        {
            return
                "#" + Employee_id + ", " +
                "Jméno: " + FullName + ", " +
                "Pohlaví: " + FullGender + ", " +
                "Věk: " + Age + ", " +
                "Ředitel: " + FullWarden + ", " +
                "Propuštěn: " + FullFired + ", " +
                "Věznice #" + Prison.Prison_id;
        }
    }
}
