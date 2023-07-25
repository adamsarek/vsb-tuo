using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using PrisonORM.Database.proxy;

namespace PrisonORM.Database.mssql
{
    class VisitorTable : VisitorProxy
    {
        public static string TABLE_NAME = "Visitor";

        public static string SQL_SELECT = "SELECT visitor_id, firstName, lastName, gender, birthDate, active, forbidden FROM Visitor";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE visitor_id = @visitor_id";
        public static string SQL_INSERT = "INSERT INTO Visitor VALUES (@visitor_id, @firstName, @lastName, @gender, @birthDate, @active, @forbidden)";
        public static string SQL_UPDATE = "UPDATE Visitor SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate WHERE visitor_id = @visitor_id";

        protected override int insert(Visitor visitor, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@visitor_id", visitor.Visitor_id);
            command.Parameters.AddWithValue("@firstName", visitor.FirstName);
            command.Parameters.AddWithValue("@lastName", visitor.LastName);
            command.Parameters.AddWithValue("@gender", visitor.Gender);
            command.Parameters.AddWithValue("@birthDate", visitor.BirthDate);
            command.Parameters.AddWithValue("@active", visitor.Active);
            command.Parameters.AddWithValue("@forbidden", visitor.Forbidden);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        protected override int update(Visitor visitor, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@visitor_id", visitor.Visitor_id);
            command.Parameters.AddWithValue("@firstName", visitor.FirstName);
            command.Parameters.AddWithValue("@lastName", visitor.LastName);
            command.Parameters.AddWithValue("@gender", visitor.Gender);
            command.Parameters.AddWithValue("@birthDate", visitor.BirthDate);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        protected override Collection<Visitor> select(DatabaseProxy pDb = null)
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
            Collection<Visitor> obj = Read(reader);
            reader.Close();
            if (pDb == null)
            {
                db.Close();
            }

            return obj;
        }

        protected override Visitor select(int visitor_id, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@visitor_id", visitor_id);
            SqlDataReader reader = db.Select(command);
            Collection<Visitor> obj = Read(reader);
            Visitor visitor = null;
            if (obj.Count == 1)
            {
                visitor = obj[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return visitor;
        }

        protected override DatabaseProxy updateActivity(int visitor_id, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand("updateVisitorActivity");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@visitor_id", visitor_id);
            db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return pDb;
        }

        protected override DatabaseProxy forbid(int visitor_id, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand("forbidVisitor");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@visitor_id", visitor_id);
            db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return pDb;
        }

        private static Collection<Visitor> Read(SqlDataReader reader)
        {
            Collection<Visitor> obj = new Collection<Visitor>();

            while (reader.Read())
            {
                int i = -1;
                Visitor visitor = new Visitor();
                visitor.Visitor_id = reader.GetInt32(++i);
                visitor.FirstName = reader.GetString(++i);
                visitor.LastName = reader.GetString(++i);
                visitor.Gender = char.Parse(reader.GetString(++i));
                visitor.BirthDate = reader.GetDateTime(++i);
                visitor.Active = char.Parse(reader.GetString(++i));
                visitor.Forbidden = char.Parse(reader.GetString(++i));
                obj.Add(visitor);
            }

            return obj;
        }
    }
}
