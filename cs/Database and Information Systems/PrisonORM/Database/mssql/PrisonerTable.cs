using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using PrisonORM.Database.proxy;

namespace PrisonORM.Database.mssql
{
    class PrisonerTable : PrisonerProxy
    {
        public static string TABLE_NAME = "Prisoner";

        public static string SQL_SELECT = "SELECT prisoner_id, firstName, lastName, gender, birthDate, punishmentStartDate, punishmentEndDate, released, Cell_cell_id FROM Prisoner";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE prisoner_id = @prisoner_id";

        protected override DatabaseProxy insert(Prisoner prisoner, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand("insertPrisoner");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@firstName", prisoner.FirstName);
            command.Parameters.AddWithValue("@lastName", prisoner.LastName);
            command.Parameters.AddWithValue("@gender", prisoner.Gender);
            command.Parameters.AddWithValue("@birthDate", prisoner.BirthDate);
            command.Parameters.AddWithValue("@punishmentStartDate", prisoner.PunishmentStartDate);
            command.Parameters.AddWithValue("@punishmentEndDate", prisoner.PunishmentEndDate);
            command.Parameters.AddWithValue("@Cell_cell_id", prisoner.Cell.Cell_id);
            db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return pDb;
        }

        protected override DatabaseProxy update(Prisoner prisoner, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand("updatePrisoner");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@prisoner_id", prisoner.Prisoner_id);
            command.Parameters.AddWithValue("@firstName", prisoner.FirstName);
            command.Parameters.AddWithValue("@lastName", prisoner.LastName);
            command.Parameters.AddWithValue("@gender", prisoner.Gender);
            command.Parameters.AddWithValue("@birthDate", prisoner.BirthDate);
            command.Parameters.AddWithValue("@punishmentStartDate", prisoner.PunishmentStartDate);
            command.Parameters.AddWithValue("@punishmentEndDate", prisoner.PunishmentEndDate);
            command.Parameters.AddWithValue("@NewCell_cell_id", prisoner.Cell.Cell_id);
            db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return pDb;
        }

        protected override Collection<Prisoner> select(DatabaseProxy pDb = null)
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
            Collection<Prisoner> obj = Read(reader);
            reader.Close();
            if (pDb == null)
            {
                db.Close();
            }

            return obj;
        }

        protected override Prisoner select(int prisoner_id, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@prisoner_id", prisoner_id);
            SqlDataReader reader = db.Select(command);
            Collection<Prisoner> obj = Read(reader);
            Prisoner prisoner = null;
            if (obj.Count == 1)
            {
                prisoner = obj[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return prisoner;
        }

        protected override DatabaseProxy release(int prisoner_id, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand("releasePrisoner");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@prisoner_id", prisoner_id);
            db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return pDb;
        }

        private static Collection<Prisoner> Read(SqlDataReader reader)
        {
            Collection<Prisoner> obj = new Collection<Prisoner>();

            while (reader.Read())
            {
                int i = -1;
                Prisoner prisoner = new Prisoner();
                prisoner.Prisoner_id = reader.GetInt32(++i);
                prisoner.FirstName = reader.GetString(++i);
                prisoner.LastName = reader.GetString(++i);
                prisoner.Gender = char.Parse(reader.GetString(++i));
                prisoner.BirthDate = reader.GetDateTime(++i);
                prisoner.PunishmentStartDate = reader.GetDateTime(++i);
                prisoner.PunishmentEndDate = reader.GetDateTime(++i);
                prisoner.Released = char.Parse(reader.GetString(++i));
                prisoner.Cell = new Cell();
                prisoner.Cell.Cell_id = reader.GetInt32(++i);
                obj.Add(prisoner);
            }

            return obj;
        }
    }
}
