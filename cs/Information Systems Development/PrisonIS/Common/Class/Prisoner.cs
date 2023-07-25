using System;

namespace Common.Class
{
    public class Prisoner
    {
        private Cell cell = null;

        public int PrisonerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime PunishmentStartDate { get; set; }
        public DateTime PunishmentEndDate { get; set; }
        public char Released { get; set; }
        public Cell Cell
        {
            get
            {
                if (cell == null)
                {
                    cell = new CellLogic().SelectForPrisoner(PrisonerId);
                }
                return cell;
            }
            set
            {
                cell = value;
            }
        }

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
        public string FullReleased { get { return (Released == '0' ? "NE" : "ANO"); } }

        public override string ToString()
        {
            return
                "#" + PrisonerId + ", " +
                "Jméno: " + FullName + ", " +
                "Pohlaví: " + FullGender + ", " +
                "Věk: " + Age + ", " +
                "Začátek trestu: " + PunishmentStartDate.ToShortDateString() + ", " +
                "Konec trestu: " + PunishmentEndDate.ToShortDateString() + ", " +
                "Propuštěn: " + FullReleased + ", " +
                "Cela #" + Cell.CellId;
        }
    }
}
