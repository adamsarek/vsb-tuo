using Common.Class;
using DataLayerInterface;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace DataLayerMSSQL
{
    public class VisitorTable : IVisitorData
    {
        public static string TABLE_NAME = "Visitor";

        public static string SQL_SELECT = "SELECT Visitor.visitor_id, Visitor.firstName, Visitor.lastName, Visitor.gender, Visitor.birthDate, Visitor.forbidden FROM Visitor";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE Visitor.visitor_id = @visitor_id";
        public static string SQL_INSERT = "INSERT INTO Visitor VALUES (@visitor_id, @firstName, @lastName, @gender, @birthDate, @forbidden)";
        public static string SQL_UPDATE = "UPDATE Visitor SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, forbidden = @forbidden WHERE visitor_id = @visitor_id";
        public static string SQL_SELECT_FOR_VISIT_ID = SQL_SELECT + " JOIN Visit ON Visitor.visitor_id = Visit.visitor_id AND Visit.visit_id = @visit_id";

        public int Insert(Visitor visitor)
        {
            Collection<Visitor> visitors = Select();
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@visitor_id", visitors.Count + 1);
            command.Parameters.AddWithValue("@firstName", visitor.FirstName);
            command.Parameters.AddWithValue("@lastName", visitor.LastName);
            command.Parameters.AddWithValue("@gender", visitor.Gender);
            command.Parameters.AddWithValue("@birthDate", visitor.BirthDate);
            command.Parameters.AddWithValue("@forbidden", visitor.Forbidden);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public int Update(Visitor visitor)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@visitor_id", visitor.VisitorId);
            command.Parameters.AddWithValue("@firstName", visitor.FirstName);
            command.Parameters.AddWithValue("@lastName", visitor.LastName);
            command.Parameters.AddWithValue("@gender", visitor.Gender);
            command.Parameters.AddWithValue("@birthDate", visitor.BirthDate);
            command.Parameters.AddWithValue("@forbidden", visitor.Forbidden);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public Collection<Visitor> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Visitor> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        public Visitor Select(int visitorId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@visitor_id", visitorId);
            SqlDataReader reader = db.Select(command);
            Collection<Visitor> obj = Read(reader);
            Visitor visitor = null;
            if (obj.Count == 1)
            {
                visitor = obj[0];
            }
            reader.Close();
            db.Close();

            return visitor;
        }

        public Visitor SelectForVisit(int visitId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_FOR_VISIT_ID);
            command.Parameters.AddWithValue("@visit_id", visitId);
            SqlDataReader reader = db.Select(command);
            Collection<Visitor> obj = Read(reader);
            Visitor visitor = null;
            if (obj.Count == 1)
            {
                visitor = obj[0];
            }
            reader.Close();
            db.Close();

            return visitor;
        }

        private static Collection<Visitor> Read(SqlDataReader reader)
        {
            Collection<Visitor> obj = new Collection<Visitor>();

            while (reader.Read())
            {
                int i = -1;
                Visitor visitor = new Visitor();
                visitor.VisitorId = reader.GetInt32(++i);
                visitor.FirstName = reader.GetString(++i);
                visitor.LastName = reader.GetString(++i);
                visitor.Gender = char.Parse(reader.GetString(++i));
                visitor.BirthDate = reader.GetDateTime(++i);
                visitor.Forbidden = char.Parse(reader.GetString(++i));
                obj.Add(visitor);
            }

            return obj;
        }
    }
}
