using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using PrisonORM.Database.proxy;

namespace PrisonORM.Database.mssql
{
    class CellTable : CellProxy
    {
        public static string TABLE_NAME = "Cell";

        public static string SQL_SELECT =
            "SELECT Cell.cell_id, Cell.occupied, Cell.capacity, " +
            "Prison.prison_id, Prison.address, Prison.city " +
            "FROM Cell " +
            "JOIN Prison ON Cell.Prison_prison_id = Prison.prison_id";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE cell_id = @cell_id";
        public static string SQL_INSERT = "INSERT INTO Cell VALUES (@cell_id, @occupied, @capacity, @prison_id)";
        public static string SQL_UPDATE = "UPDATE Cell SET occupied = @occupied, capacity = @capacity WHERE cell_id = @cell_id";

        protected override int insert(Cell cell, DatabaseProxy pDb = null)
        {
            Database db;

            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@cell_id", cell.Cell_id);
            command.Parameters.AddWithValue("@occupied", cell.Occupied);
            command.Parameters.AddWithValue("@capacity", cell.Capacity);
            command.Parameters.AddWithValue("@prison_id", cell.Prison.Prison_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        protected override int update(Cell cell, DatabaseProxy pDb = null)
        {
            Database db;

            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@cell_id", cell.Cell_id);
            command.Parameters.AddWithValue("@occupied", cell.Occupied);
            command.Parameters.AddWithValue("@capacity", cell.Capacity);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        protected override Collection<Cell> select(DatabaseProxy pDb = null)
        {
            Database db;

            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Cell> obj = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return obj;
        }

        protected override Cell select(int cell_id, DatabaseProxy pDb = null)
        {
            Database db;

            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@cell_id", cell_id);
            SqlDataReader reader = db.Select(command);
            Collection<Cell> obj = Read(reader);
            Cell cell = null;
            if (obj.Count == 1)
            {
                cell = obj[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return cell;
        }

        private static Collection<Cell> Read(SqlDataReader reader)
        {
            Collection<Cell> obj = new Collection<Cell>();

            while (reader.Read())
            {
                int i = -1;
                Cell cell = new Cell();
                cell.Cell_id = reader.GetInt32(++i);
                cell.Occupied = reader.GetInt32(++i);
                cell.Capacity = reader.GetInt32(++i);
                cell.Prison = new Prison();
                cell.Prison.Prison_id = reader.GetInt32(++i);
                cell.Prison.Address = reader.GetString(++i);
                cell.Prison.City = reader.GetString(++i);
                obj.Add(cell);
            }

            return obj;
        }
    }
}
