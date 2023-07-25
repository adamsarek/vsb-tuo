using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using PrisonORM.Database.proxy;

namespace PrisonORM.Database.mssql
{
    class PrisonerCellHistoryTable : PrisonerCellHistoryProxy
    {
        public static string TABLE_NAME = "PrisonerCellHistory";

        public static string SQL_SELECT =
            "SELECT PrisonerCellHistory.prisonerCellHistory_id, PrisonerCellHistory.startDate, PrisonerCellHistory.endDate, PrisonerCellHistory.Cell_cell_id, " +
            "Prisoner.prisoner_id, Prisoner.firstName, Prisoner.lastName, Prisoner.gender, Prisoner.birthDate " +
            "FROM PrisonerCellHistory " +
            "JOIN Prisoner ON PrisonerCellHistory.Prisoner_prisoner_id = Prisoner.prisoner_id";
        
        protected override DatabaseProxy insert(PrisonerCellHistory prisonerCellHistory, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand("insertPrisonerCellHistory");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Prisoner_prisoner_id", prisonerCellHistory.Prisoner.Prisoner_id);
            command.Parameters.AddWithValue("@Cell_cell_id", prisonerCellHistory.Cell.Cell_id);
            db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return pDb;
        }

        protected override Collection<PrisonerCellHistory> select(DatabaseProxy pDb = null)
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
            Collection<PrisonerCellHistory> obj = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return obj;

        }

        private static Collection<PrisonerCellHistory> Read(SqlDataReader reader)
        {
            Collection<PrisonerCellHistory> obj = new Collection<PrisonerCellHistory>();

            while (reader.Read())
            {
                int i = -1;
                PrisonerCellHistory prisonerCellHistory = new PrisonerCellHistory();
                prisonerCellHistory.PrisonerCellHistory_id = reader.GetInt32(++i);
                prisonerCellHistory.StartDate = reader.GetDateTime(++i);
                prisonerCellHistory.EndDate = reader.GetDateTime(++i);
                prisonerCellHistory.Cell = new Cell();
                prisonerCellHistory.Cell.Cell_id = reader.GetInt32(++i);
                prisonerCellHistory.Prisoner = new Prisoner();
                prisonerCellHistory.Prisoner.Prisoner_id = reader.GetInt32(++i);
                prisonerCellHistory.Prisoner.FirstName = reader.GetString(++i);
                prisonerCellHistory.Prisoner.LastName = reader.GetString(++i);
                prisonerCellHistory.Prisoner.Gender = char.Parse(reader.GetString(++i));
                prisonerCellHistory.Prisoner.BirthDate = reader.GetDateTime(++i);
                obj.Add(prisonerCellHistory);
            }

            return obj;
        }
    }
}
