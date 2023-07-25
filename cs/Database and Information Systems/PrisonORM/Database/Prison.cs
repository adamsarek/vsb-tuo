using System;

namespace PrisonORM.Database
{
    class Prison
    {
        public int Prison_id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public PrisonType PrisonType { get; set; }

        public override string ToString()
        {
            return
                "Prison ID: " + Prison_id + ", " +
                "Address: " + Address + ", " +
                "City: " + City + ", " +
                "PrisonType ID: " + PrisonType.PrisonType_id;
        }
    }
}
