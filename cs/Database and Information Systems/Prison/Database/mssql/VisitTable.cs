using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using PrisonORM.Database.proxy;

namespace PrisonORM.Database.mssql
{
    class VisitTable : VisitProxy
    {
        public static string TABLE_NAME = "Visit";

        public static string SQL_SELECT =
            "SELECT Visit.visit_id, Visit.visitDate, Visit.allowed, " +
            "Prisoner.prisoner_id, Prisoner.firstName, Prisoner.lastName, Prisoner.gender, Prisoner.birthDate, " +
            "Visitor.visitor_id, Visitor.firstName, Visitor.lastName, Visitor.gender, Visitor.birthDate " +
            "FROM Visit " +
            "JOIN Prisoner ON Visit.Prisoner_prisoner_id = Prisoner.prisoner_id " +
            "JOIN Visitor ON Visit.Visitor_visitor_id = Visitor.visitor_id";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE Visit.visit_id = @visit_id";
        public static string SQL_UPDATE = "UPDATE Visit SET visitDate = @visitDate, allowed = @allowed WHERE visit_id = @visit_id";

        protected override DatabaseProxy insert(Visit visit, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand("insertVisit");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@visitDate", visit.VisitDate);
            command.Parameters.AddWithValue("@allowed", visit.Allowed);
            command.Parameters.AddWithValue("@Prisoner_prisoner_id", visit.Prisoner.Prisoner_id);
            command.Parameters.AddWithValue("@Visitor_visitor_id", visit.Visitor.Visitor_id);
            db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return pDb;
        }

        protected override int update(Visit visit, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@visit_id", visit.Visit_id);
            command.Parameters.AddWithValue("@visitDate", visit.VisitDate);
            command.Parameters.AddWithValue("@allowed", visit.Allowed);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        protected override Collection<Visit> select(DatabaseProxy pDb = null)
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
            Collection<Visit> obj = Read(reader);
            reader.Close();
            if (pDb == null)
            {
                db.Close();
            }

            return obj;
        }

        protected override Visit select(int visit_id, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@visit_id", visit_id);
            SqlDataReader reader = db.Select(command);
            Collection<Visit> obj = Read(reader);
            Visit visit = null;
            if (obj.Count == 1)
            {
                visit = obj[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return visit;
        }

        private static Collection<Visit> Read(SqlDataReader reader)
        {
            Collection<Visit> obj = new Collection<Visit>();

            while (reader.Read())
            {
                int i = -1;
                Visit visit = new Visit();
                visit.Visit_id = reader.GetInt32(++i);
                visit.VisitDate = reader.GetDateTime(++i);
                visit.Allowed = char.Parse(reader.GetString(++i));
                visit.Prisoner = new Prisoner();
                visit.Prisoner.Prisoner_id = reader.GetInt32(++i);
                visit.Prisoner.FirstName = reader.GetString(++i);
                visit.Prisoner.LastName = reader.GetString(++i);
                visit.Prisoner.Gender = char.Parse(reader.GetString(++i));
                visit.Prisoner.BirthDate = reader.GetDateTime(++i);
                visit.Visitor = new Visitor();
                visit.Visitor.Visitor_id = reader.GetInt32(++i);
                visit.Visitor.FirstName = reader.GetString(++i);
                visit.Visitor.LastName = reader.GetString(++i);
                visit.Visitor.Gender = char.Parse(reader.GetString(++i));
                visit.Visitor.BirthDate = reader.GetDateTime(++i);
                obj.Add(visit);
            }

            return obj;
        }
    }
}
