using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using PrisonORM.Database.proxy;

namespace PrisonORM.Database.mssql
{
    class EmployeeTable : EmployeeProxy
    {
        public static string TABLE_NAME = "Employee";

        public static string SQL_SELECT =
            "SELECT Employee.employee_id, Employee.firstName, Employee.lastName, Employee.gender, Employee.birthDate, Employee.warden, Employee.fired, " +
            "Prison.prison_id, Prison.address, Prison.city " +
            "FROM Employee " +
            "JOIN Prison ON Employee.Prison_prison_id = Prison.prison_id";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE employee_id = @employee_id";
        public static string SQL_INSERT = "INSERT INTO Employee VALUES (@employee_id, @firstName, @lastName, @gender, @birthDate, @warden, @fired, @prison_id)";
        public static string SQL_UPDATE = "UPDATE Employee SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, warden = @warden WHERE employee_id = @employee_id";
        public static string SQL_FIRE = "UPDATE Employee SET fired = '1' WHERE employee_id = @employee_id";

        protected override int insert(Employee employee, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@employee_id", employee.Employee_id);
            command.Parameters.AddWithValue("@firstName", employee.FirstName);
            command.Parameters.AddWithValue("@lastName", employee.LastName);
            command.Parameters.AddWithValue("@gender", employee.Gender);
            command.Parameters.AddWithValue("@birthDate", employee.BirthDate);
            command.Parameters.AddWithValue("@warden", employee.Warden);
            command.Parameters.AddWithValue("@fired", employee.Fired);
            command.Parameters.AddWithValue("@prison_id", employee.Prison.Prison_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        protected override int update(Employee employee, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@employee_id", employee.Employee_id);
            command.Parameters.AddWithValue("@firstName", employee.FirstName);
            command.Parameters.AddWithValue("@lastName", employee.LastName);
            command.Parameters.AddWithValue("@gender", employee.Gender);
            command.Parameters.AddWithValue("@birthDate", employee.BirthDate);
            command.Parameters.AddWithValue("@warden", employee.Warden);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        protected override Collection<Employee> select(DatabaseProxy pDb = null)
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
            Collection<Employee> obj = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return obj;

        }

        protected override Employee select(int employee_id, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue("@employee_id", employee_id);
            SqlDataReader reader = db.Select(command);
            Collection<Employee> obj = Read(reader);
            Employee employee = null;
            if (obj.Count == 1)
            {
                employee = obj[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return employee;
        }

        protected override int fire(int employee_id, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_FIRE);
            command.Parameters.AddWithValue("@employee_id", employee_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static Collection<Employee> Read(SqlDataReader reader)
        {
            Collection<Employee> obj = new Collection<Employee>();

            while (reader.Read())
            {
                int i = -1;
                Employee employee = new Employee();
                employee.Employee_id = reader.GetInt32(++i);
                employee.FirstName = reader.GetString(++i);
                employee.LastName = reader.GetString(++i);
                employee.Gender = char.Parse(reader.GetString(++i));
                employee.BirthDate = reader.GetDateTime(++i);
                employee.Warden = char.Parse(reader.GetString(++i));
                employee.Fired = char.Parse(reader.GetString(++i));
                employee.Prison = new Prison();
                employee.Prison.Prison_id = reader.GetInt32(++i);
                employee.Prison.Address = reader.GetString(++i);
                employee.Prison.City = reader.GetString(++i);
                obj.Add(employee);
            }

            return obj;
        }
    }
}
