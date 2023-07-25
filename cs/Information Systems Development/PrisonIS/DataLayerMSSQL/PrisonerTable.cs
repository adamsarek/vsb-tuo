using Common.Class;
using DataLayerInterface;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace DataLayerMSSQL
{
    public class PrisonerTable : IPrisonerData
    {
        public static string TABLE_NAME = "Prisoner";

        public static string SQL_SELECT = "SELECT Prisoner.prisoner_id, Prisoner.firstName, Prisoner.lastName, Prisoner.gender, Prisoner.birthDate, Prisoner.punishmentStartDate, Prisoner.punishmentEndDate, Prisoner.released FROM Prisoner";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE prisoner_id = @prisoner_id";
        public static string SQL_INSERT = "INSERT INTO Prisoner VALUES (@prisoner_id, @firstName, @lastName, @gender, @birthDate, @punishmentStartDate, @punishmentEndDate, @released, @cell_id)";
        public static string SQL_UPDATE = "UPDATE Prisoner SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, punishmentStartDate = @punishmentStartDate, punishmentEndDate = @punishmentEndDate, released = @released, cell_id = @cell_id WHERE prisoner_id = @prisoner_id";
        public static string SQL_SELECT_FOR_CELL_ID = SQL_SELECT + " JOIN Cell ON Prisoner.cell_id = Cell.cell_id AND Cell.cell_id = @cell_id";
        public static string SQL_SELECT_FOR_VISIT_ID = SQL_SELECT + " JOIN Visit ON Prisoner.prisoner_id = Visit.prisoner_id AND Visit.visit_id = @visit_id";

        public int Insert(Prisoner prisoner)
        {
            Collection<Prisoner> prisoners = Select();
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@prisoner_id", prisoners.Count + 1);
            command.Parameters.AddWithValue("@firstName", prisoner.FirstName);
            command.Parameters.AddWithValue("@lastName", prisoner.LastName);
            command.Parameters.AddWithValue("@gender", prisoner.Gender);
            command.Parameters.AddWithValue("@birthDate", prisoner.BirthDate);
            command.Parameters.AddWithValue("@punishmentStartDate", prisoner.PunishmentStartDate);
            command.Parameters.AddWithValue("@punishmentEndDate", prisoner.PunishmentEndDate);
            command.Parameters.AddWithValue("@released", prisoner.Released);
            command.Parameters.AddWithValue("@cell_id", prisoner.Cell.CellId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public int Update(Prisoner prisoner)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@prisoner_id", prisoner.PrisonerId);
            command.Parameters.AddWithValue("@firstName", prisoner.FirstName);
            command.Parameters.AddWithValue("@lastName", prisoner.LastName);
            command.Parameters.AddWithValue("@gender", prisoner.Gender);
            command.Parameters.AddWithValue("@birthDate", prisoner.BirthDate);
            command.Parameters.AddWithValue("@punishmentStartDate", prisoner.PunishmentStartDate);
            command.Parameters.AddWithValue("@punishmentEndDate", prisoner.PunishmentEndDate);
            command.Parameters.AddWithValue("@released", prisoner.Released);
            command.Parameters.AddWithValue("@cell_id", prisoner.Cell.CellId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public Collection<Prisoner> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Prisoner> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        public Prisoner Select(int prisonerId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@prisoner_id", prisonerId);
            SqlDataReader reader = db.Select(command);
            Collection<Prisoner> obj = Read(reader);
            Prisoner prisoner = null;
            if (obj.Count == 1)
            {
                prisoner = obj[0];
            }
            reader.Close();
            db.Close();

            return prisoner;
        }

        public Collection<Prisoner> SelectForCell(int cellId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_FOR_CELL_ID);
            command.Parameters.AddWithValue("@cell_id", cellId);
            SqlDataReader reader = db.Select(command);
            Collection<Prisoner> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        public Prisoner SelectForVisit(int visitId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_FOR_VISIT_ID);
            command.Parameters.AddWithValue("@visit_id", visitId);
            SqlDataReader reader = db.Select(command);
            Collection<Prisoner> obj = Read(reader);
            Prisoner prisoner = null;
            if (obj.Count == 1)
            {
                prisoner = obj[0];
            }
            reader.Close();
            db.Close();

            return prisoner;
        }

        private static Collection<Prisoner> Read(SqlDataReader reader)
        {
            Collection<Prisoner> obj = new Collection<Prisoner>();

            while (reader.Read())
            {
                int i = -1;
                Prisoner prisoner = new Prisoner();
                prisoner.PrisonerId = reader.GetInt32(++i);
                prisoner.FirstName = reader.GetString(++i);
                prisoner.LastName = reader.GetString(++i);
                prisoner.Gender = char.Parse(reader.GetString(++i));
                prisoner.BirthDate = reader.GetDateTime(++i);
                prisoner.PunishmentStartDate = reader.GetDateTime(++i);
                prisoner.PunishmentEndDate = reader.GetDateTime(++i);
                prisoner.Released = char.Parse(reader.GetString(++i));
                obj.Add(prisoner);
            }

            return obj;
        }
    }
}
