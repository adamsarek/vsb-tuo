using System;

namespace PrisonORM.Database
{
    public class Cell
    {
        public int Cell_id { get; set; }
        public int Occupied { get; set; }
        public int Capacity { get; set; }
        public Prison Prison { get; set; }

        public override string ToString()
        {
            return
                "#" + Cell_id + ", " +
                "Obsazeno: " + Occupied + ", " +
                "Kapacita: " + Capacity + ", " +
                "Věznice #" + Prison.Prison_id;
        }
    }
}
