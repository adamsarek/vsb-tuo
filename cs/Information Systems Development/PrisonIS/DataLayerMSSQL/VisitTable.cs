using Common.Class;
using DataLayerInterface;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace DataLayerMSSQL
{
    public class VisitTable : IVisitData
    {
        public static string TABLE_NAME = "Visit";

        public static string SQL_SELECT = "SELECT Visit.visit_id, Visit.visitDate, Visit.allowed FROM Visit";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE Visit.visit_id = @visit_id";
        public static string SQL_INSERT = "INSERT INTO Visit VALUES (@visit_id, @visitDate, @allowed, @prisoner_id, @visitor_id)";
        public static string SQL_UPDATE = "UPDATE Visit SET visitDate = @visitDate, allowed = @allowed, prisoner_id = @prisoner_id, visitor_id = @visitor_id WHERE visit_id = @visit_id";
        public static string SQL_SELECT_FOR_PRISONER_ID = SQL_SELECT + " JOIN Prisoner ON Visit.prisoner_id = Prisoner.prisoner_id AND Prisoner.prisoner_id = @prisoner_id";
        public static string SQL_SELECT_FOR_VISITOR_ID = SQL_SELECT + " JOIN Visitor ON Visit.visitor_id = Visitor.visitor_id AND Visitor.visitor_id = @visitor_id";

        public int Insert(Visit visit)
        {
            Collection<Visit> visits = Select();
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@visit_id", visits.Count + 1);
            command.Parameters.AddWithValue("@visitDate", visit.VisitDate);
            command.Parameters.AddWithValue("@allowed", visit.Allowed);
            command.Parameters.AddWithValue("@prisoner_id", visit.Prisoner.PrisonerId);
            command.Parameters.AddWithValue("@visitor_id", visit.Visitor.VisitorId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public int Update(Visit visit)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@visit_id", visit.VisitId);
            command.Parameters.AddWithValue("@visitDate", visit.VisitDate);
            command.Parameters.AddWithValue("@allowed", visit.Allowed);
            command.Parameters.AddWithValue("@prisoner_id", visit.Prisoner.PrisonerId);
            command.Parameters.AddWithValue("@visitor_id", visit.Visitor.VisitorId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public Collection<Visit> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Visit> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        public Visit Select(int visitId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@visit_id", visitId);
            SqlDataReader reader = db.Select(command);
            Collection<Visit> obj = Read(reader);
            Visit visit = null;
            if (obj.Count == 1)
            {
                visit = obj[0];
            }
            reader.Close();
            db.Close();

            return visit;
        }

        public Collection<Visit> SelectForPrisoner(int prisonerId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_FOR_PRISONER_ID);
            command.Parameters.AddWithValue("@prisoner_id", prisonerId);
            SqlDataReader reader = db.Select(command);
            Collection<Visit> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        public Collection<Visit> SelectForVisitor(int visitorId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_FOR_VISITOR_ID);
            command.Parameters.AddWithValue("@visitor_id", visitorId);
            SqlDataReader reader = db.Select(command);
            Collection<Visit> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        private static Collection<Visit> Read(SqlDataReader reader)
        {
            Collection<Visit> obj = new Collection<Visit>();

            while (reader.Read())
            {
                int i = -1;
                Visit visit = new Visit();
                visit.VisitId = reader.GetInt32(++i);
                visit.VisitDate = reader.GetDateTime(++i);
                visit.Allowed = char.Parse(reader.GetString(++i));
                obj.Add(visit);
            }

            return obj;
        }
    }
}
