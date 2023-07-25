using Common.Class;
using System.Collections.ObjectModel;

namespace DataLayerInterface
{
    public interface IEmployeeData
    {
        int Insert(Employee employee);
        int Update(Employee employee);
        Collection<Employee> Select();
        Employee Select(int employeeId);
    }
}
