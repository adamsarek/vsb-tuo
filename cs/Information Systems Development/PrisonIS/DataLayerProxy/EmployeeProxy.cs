using Common;
using Common.Class;
using DataLayerInterface;
using DataLayerMSSQL;
using DataLayerXML;
using System.Collections.ObjectModel;

namespace DataLayerProxy
{
    public class EmployeeProxy : IEmployeeData
    {
        private IEmployeeData instance
        {
            get
            {
                if (Session.DataStorage == Session.Storage.SQL)
                {
                    return new EmployeeTable();
                }
                else
                {
                    return new EmployeeXml();
                }
            }
        }

        public int Insert(Employee employee)
        {
            return instance.Insert(employee);
        }

        public int Update(Employee employee)
        {
            return instance.Update(employee);
        }

        public Collection<Employee> Select()
        {
            return instance.Select();
        }

        public Employee Select(int employeeId)
        {
            return instance.Select(employeeId);
        }
    }
}
