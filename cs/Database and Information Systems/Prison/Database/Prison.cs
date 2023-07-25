using System;

namespace PrisonORM.Database
{
    public class Prison
    {
        public int Prison_id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public PrisonType PrisonType { get; set; }

        public override string ToString()
        {
            return
                "#" + Prison_id + ", " +
                "Adresa: " + Address + ", " +
                "Město: " + City + ", " +
                "Typ věznice #" + PrisonType.PrisonType_id;
        }
    }
}
