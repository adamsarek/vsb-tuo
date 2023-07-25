using Common.Class;
using DataLayerProxy;
using System.Collections.ObjectModel;

namespace DomainLayer
{
    public class EmployeeLogic
    {
        private EmployeeProxy employeeProxy = new EmployeeProxy();

        public int Insert(Employee employee)
        {
            return employeeProxy.Insert(employee);
        }

        public int Update(Employee employee)
        {
            return employeeProxy.Update(employee);
        }

        public Collection<Employee> Select()
        {
            Collection<Employee> employees = employeeProxy.Select();
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Fired == '1')
                {
                    employees.Remove(employees[i]);
                    i--;
                }
            }

            return employees;
        }

        public Employee Select(int employeeId)
        {
            return employeeProxy.Select(employeeId);
        }

        public int Fire(Employee employee)
        {
            // Fire the employee
            employee.Fired = '1';

            return employeeProxy.Update(employee);
        }
    }
}
