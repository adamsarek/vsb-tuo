using Common.Class;
using DataLayerInterface;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace DataLayerMSSQL
{
    public class CellTable : ICellData
    {
        public static string TABLE_NAME = "Cell";

        public static string SQL_SELECT = "SELECT Cell.cell_id, Cell.occupied, Cell.capacity FROM Cell";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE cell_id = @cell_id";
        public static string SQL_INSERT = "INSERT INTO Cell VALUES (@cell_id, @occupied, @capacity)";
        public static string SQL_UPDATE = "UPDATE Cell SET occupied = @occupied, capacity = @capacity WHERE cell_id = @cell_id";
        public static string SQL_SELECT_FOR_PRISONER_ID = SQL_SELECT + " JOIN Prisoner ON Cell.cell_id = Prisoner.cell_id AND Prisoner.prisoner_id = @prisoner_id";

        public int Insert(Cell cell)
        {
            Collection<Cell> cells = Select();
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@cell_id", cells.Count + 1);
            command.Parameters.AddWithValue("@occupied", cell.Occupied);
            command.Parameters.AddWithValue("@capacity", cell.Capacity);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public int Update(Cell cell)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@cell_id", cell.CellId);
            command.Parameters.AddWithValue("@occupied", cell.Occupied);
            command.Parameters.AddWithValue("@capacity", cell.Capacity);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public Collection<Cell> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Cell> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        public Cell Select(int cellId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@cell_id", cellId);
            SqlDataReader reader = db.Select(command);
            Collection<Cell> obj = Read(reader);
            Cell cell = null;
            if (obj.Count == 1)
            {
                cell = obj[0];
            }
            reader.Close();
            db.Close();

            return cell;
        }

        public Cell SelectForPrisoner(int prisonerId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_FOR_PRISONER_ID);
            command.Parameters.AddWithValue("@prisoner_id", prisonerId);
            SqlDataReader reader = db.Select(command);
            Collection<Cell> obj = Read(reader);
            Cell cell = null;
            if (obj.Count == 1)
            {
                cell = obj[0];
            }
            reader.Close();
            db.Close();

            return cell;
        }

        private static Collection<Cell> Read(SqlDataReader reader)
        {
            Collection<Cell> obj = new Collection<Cell>();

            while (reader.Read())
            {
                int i = -1;
                Cell cell = new Cell();
                cell.CellId = reader.GetInt32(++i);
                cell.Occupied = reader.GetInt32(++i);
                cell.Capacity = reader.GetInt32(++i);
                obj.Add(cell);
            }

            return obj;
        }
    }
}
