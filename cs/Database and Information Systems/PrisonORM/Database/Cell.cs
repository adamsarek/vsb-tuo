using System;

namespace PrisonORM.Database
{
    class Cell
    {
        public int Cell_id { get; set; }
        public int Occupied { get; set; }
        public int Capacity { get; set; }
        public Prison Prison { get; set; }

        public override string ToString()
        {
            return
                "Cell ID: " + Cell_id + ", " +
                "Occupied: " + Occupied + ", " +
                "Capacity: " + Capacity + ", " +
                "Prison ID: " + Prison.Prison_id;
        }
    }
}
