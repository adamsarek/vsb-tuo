namespace Common.Class
{
    public class Cell
    {
        public int CellId { get; set; }
        public int Occupied { get; set; }
        public int Capacity { get; set; }

        public override string ToString()
        {
            return
                "#" + CellId + ", " +
                "Obsazeno: " + Occupied + ", " +
                "Kapacita: " + Capacity;
        }
    }
}
