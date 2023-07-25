using Common.Class;
using DataLayerInterface;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace DataLayerMSSQL
{
    public class EmployeeTable : IEmployeeData
    {
        public static string TABLE_NAME = "Employee";

        public static string SQL_SELECT = "SELECT Employee.employee_id, Employee.firstName, Employee.lastName, Employee.gender, Employee.birthDate, Employee.warden, Employee.fired FROM Employee";
        public static string SQL_SELECT_ID = SQL_SELECT + " WHERE employee_id = @employee_id";
        public static string SQL_INSERT = "INSERT INTO Employee VALUES (@employee_id, @firstName, @lastName, @gender, @birthDate, @warden, @fired)";
        public static string SQL_UPDATE = "UPDATE Employee SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, warden = @warden, fired = @fired WHERE employee_id = @employee_id";

        public int Insert(Employee employee)
        {
            Collection<Employee> employees = Select();
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@employee_id", employees.Count + 1);
            command.Parameters.AddWithValue("@firstName", employee.FirstName);
            command.Parameters.AddWithValue("@lastName", employee.LastName);
            command.Parameters.AddWithValue("@gender", employee.Gender);
            command.Parameters.AddWithValue("@birthDate", employee.BirthDate);
            command.Parameters.AddWithValue("@warden", employee.Warden);
            command.Parameters.AddWithValue("@fired", employee.Fired);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public int Update(Employee employee)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@employee_id", employee.EmployeeId);
            command.Parameters.AddWithValue("@firstName", employee.FirstName);
            command.Parameters.AddWithValue("@lastName", employee.LastName);
            command.Parameters.AddWithValue("@gender", employee.Gender);
            command.Parameters.AddWithValue("@birthDate", employee.BirthDate);
            command.Parameters.AddWithValue("@warden", employee.Warden);
            command.Parameters.AddWithValue("@fired", employee.Fired);
            int ret = db.ExecuteNonQuery(command);
            db.Close();

            return ret;
        }

        public Collection<Employee> Select()
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Employee> obj = Read(reader);
            reader.Close();
            db.Close();

            return obj;
        }

        public Employee Select(int employeeId)
        {
            Database db = new Database();
            db.Connect();
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@employee_id", employeeId);
            SqlDataReader reader = db.Select(command);
            Collection<Employee> obj = Read(reader);
            Employee employee = null;
            if (obj.Count == 1)
            {
                employee = obj[0];
            }
            reader.Close();
            db.Close();

            return employee;
        }

        private static Collection<Employee> Read(SqlDataReader reader)
        {
            Collection<Employee> obj = new Collection<Employee>();

            while (reader.Read())
            {
                int i = -1;
                Employee employee = new Employee();
                employee.EmployeeId = reader.GetInt32(++i);
                employee.FirstName = reader.GetString(++i);
                employee.LastName = reader.GetString(++i);
                employee.Gender = char.Parse(reader.GetString(++i));
                employee.BirthDate = reader.GetDateTime(++i);
                employee.Warden = char.Parse(reader.GetString(++i));
                employee.Fired = char.Parse(reader.GetString(++i));
                obj.Add(employee);
            }

            return obj;
        }
    }
}
