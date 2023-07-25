using System;

namespace PrisonORM.Database
{
    public class PrisonType
    {
        public int PrisonType_id { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return
                "#" + PrisonType_id + ", " +
                "Typ: " + Type;
        }
    }
}
