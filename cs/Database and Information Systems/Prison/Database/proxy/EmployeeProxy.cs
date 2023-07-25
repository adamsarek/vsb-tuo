using System.Collections.ObjectModel;
using System.Configuration;

namespace PrisonORM.Database.proxy
{
    abstract class EmployeeProxy
    {
        private static EmployeeProxy instance
        {
            get
            {
                return new mssql.EmployeeTable();
            }
        }

        protected abstract int insert(Employee employee, DatabaseProxy pDb = null);
        protected abstract int update(Employee employee, DatabaseProxy pDb = null);
        protected abstract Collection<Employee> select(DatabaseProxy pDb = null);
        protected abstract Employee select(int employee_id, DatabaseProxy pDb = null);
        protected abstract int fire(int employee_id, DatabaseProxy pDb = null);

        public static int Insert(Employee employee, DatabaseProxy pDb = null)
        {
            return instance.insert(employee, pDb);
        }

        public static int Update(Employee employee, DatabaseProxy pDb = null)
        {
            return instance.update(employee, pDb);
        }

        public static Collection<Employee> Select(DatabaseProxy pDb = null)
        {
            return instance.select(pDb);
        }

        public static Employee Select(int employee_id, DatabaseProxy pDb = null)
        {
            return instance.select(employee_id, pDb);
        }

        public static int Fire(int employee_id, DatabaseProxy pDb = null)
        {
            return instance.fire(employee_id, pDb);
        }
    }
}
