using System;

namespace PrisonORM.Database
{
    class PrisonType
    {
        public int PrisonType_id { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return
                "PrisonType ID: " + PrisonType_id + ", " +
                "Type: " + Type;
        }
    }
}
