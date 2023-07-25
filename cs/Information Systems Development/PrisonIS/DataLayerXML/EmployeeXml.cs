using Common.Class;
using DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace DataLayerXML
{
    public class EmployeeXml : IEmployeeData
    {
        private Xml xmlEmployee = new Xml("employees.xml", "employees", "employee");

        public int Insert(Employee employee)
        {
            Collection<Employee> employees = Select();
            xmlEmployee.Insert(
                new List<(string, string)>
                {
                    ("employee_id", (employees.Count + 1).ToString()),
                    ("firstName", employee.FirstName.ToString()),
                    ("lastName", employee.LastName.ToString()),
                    ("gender", employee.Gender.ToString()),
                    ("birthDate", employee.BirthDate.ToString()),
                    ("warden", employee.Warden.ToString()),
                    ("fired", employee.Fired.ToString())
                }
            );

            return 0;
        }

        public int Update(Employee employee)
        {
            xmlEmployee.Update(
                new List<(string, string)>
                {
                    ("employee_id", employee.EmployeeId.ToString())
                },
                new List<(string, string)>
                {
                    ("employee_id", employee.EmployeeId.ToString()),
                    ("firstName", employee.FirstName.ToString()),
                    ("lastName", employee.LastName.ToString()),
                    ("gender", employee.Gender.ToString()),
                    ("birthDate", employee.BirthDate.ToString()),
                    ("warden", employee.Warden.ToString()),
                    ("fired", employee.Fired.ToString())
                }
            );

            return 0;
        }

        public Collection<Employee> Select()
        {
            Collection<Employee> items = new Collection<Employee>();
            foreach (XmlNode element in xmlEmployee.SelectAll())
            {
                Employee item = new Employee();
                item.EmployeeId = int.Parse(element.Attributes["employee_id"].Value);
                item.FirstName = element.Attributes["firstName"].Value;
                item.LastName = element.Attributes["lastName"].Value;
                item.Gender = char.Parse(element.Attributes["gender"].Value);
                item.BirthDate = DateTime.Parse(element.Attributes["birthDate"].Value);
                item.Warden = char.Parse(element.Attributes["warden"].Value);
                item.Fired = char.Parse(element.Attributes["fired"].Value);
                items.Add(item);
            }

            return items;
        }

        public Employee Select(int employeeId)
        {
            XmlNode element = xmlEmployee.SelectOne(
                new List<(string, string)>
                {
                    ("employee_id", employeeId.ToString())
                }
            );
            Employee item = new Employee();
            item.EmployeeId = int.Parse(element.Attributes["employee_id"].Value);
            item.FirstName = element.Attributes["firstName"].Value;
            item.LastName = element.Attributes["lastName"].Value;
            item.Gender = char.Parse(element.Attributes["gender"].Value);
            item.BirthDate = DateTime.Parse(element.Attributes["birthDate"].Value);
            item.Warden = char.Parse(element.Attributes["warden"].Value);
            item.Fired = char.Parse(element.Attributes["fired"].Value);

            return item;
        }
    }
}
