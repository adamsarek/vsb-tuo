using PrisonORM.Database;
using PrisonORM.Database.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.Classes
{
    public static class Session
    {
        public static Employee LoggedEmployee
        {
            get
            {
                return EmployeeTable.Select(1);
            }
        }
    }
}
