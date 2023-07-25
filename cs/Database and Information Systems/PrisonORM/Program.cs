using System;
using PrisonORM.Database.mssql;
using PrisonORM.Database.proxy;
using PrisonORM.Database;
using System.Collections.ObjectModel;

namespace PrisonORM
{
    class Program
    {
        static void Main(string[] args)
        {
            //===== Connection =====//
            PrisonORM.Database.mssql.Database db = new PrisonORM.Database.mssql.Database();
            db.Connect();

            //===== Settings =====//
            int logCountLimit = 5;

            //===== Table Cell =====//
            printTable("Cell");
            printInfo("Pocet zaznamu: " + CellTable.Select(db).Count);

            printFunction("1.1 Pridani cely");
            Cell cell = new Cell()
            {
                Cell_id = 24,
                Occupied = 0,
                Capacity = 4,
                Prison = new Prison() { Prison_id = 5 }
            };
            CellProxy.Insert(cell, db);
            printInfo("Pocet zaznamu: " + CellTable.Select(db).Count);

            printFunction("1.2 Seznam cel");
            Collection<Cell> cells = CellProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(cells[i]);
            }

            printFunction("1.3 Aktualizace cely");
            cells[0].Capacity = 4;
            CellProxy.Update(cells[0], db);
            cells = CellProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(cells[i]);
            }

            printFunction("1.4 Detail cely");
            cell = CellProxy.Select(24, db);
            printInfo("Vypis detailu cely s ID(" + 24 + "):");
            Console.WriteLine(cell);
            printTableEnd();

            //===== Tabulka Employee =====//
            printTable("Employee");
            printInfo("Pocet zaznamu: " + EmployeeTable.Select(db).Count);

            printFunction("2.1 Pridani zamestnance");
            Employee employee = new Employee()
            {
                Employee_id = 37,
                FirstName = "Zdenek",
                LastName = "Novak",
                Gender = 'M',
                BirthDate = DateTime.Parse("1989-02-28"),
                Warden = '0',
                Fired = '0',
                Prison = new Prison() { Prison_id = 5 }
            };
            EmployeeProxy.Insert(employee, db);
            printInfo("Pocet zaznamu: " + EmployeeTable.Select(db).Count);

            printFunction("2.2 Seznam zamestnancu");
            Collection<Employee> employees = EmployeeProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(employees[i]);
            }

            printFunction("2.3 Aktualizace zamestnance");
            employees[0].LastName = "Mala";
            EmployeeProxy.Update(employees[0], db);
            employees = EmployeeProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(employees[i]);
            }

            printFunction("2.4 Propustit zamestnance");
            EmployeeProxy.Fire(2, db);
            employees = EmployeeProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(employees[i]);
            }

            printFunction("2.5 Detail zamestnance");
            employee = EmployeeProxy.Select(37, db);
            printInfo("Vypis detailu zamestnance s ID(" + 37 + "):");
            Console.WriteLine(employee);
            printTableEnd();

            //===== Tabulka Prisoner =====//
            printTable("Prisoner");
            printInfo("Pocet zaznamu: " + PrisonerTable.Select(db).Count);

            printFunction("3.1 Pridani vezne");
            Prisoner prisoner = new Prisoner()
            {
                Prisoner_id = 43,
                FirstName = "Helena",
                LastName = "Nejedla",
                Gender = 'F',
                BirthDate = DateTime.Parse("1966-06-06"),
                PunishmentStartDate = DateTime.Parse("2020-04-01"),
                PunishmentEndDate = DateTime.Parse("2030-04-01"),
                Released = '0',
                Cell = new Cell() { Cell_id = 22}
            };
            PrisonerProxy.Insert(prisoner, db);
            printInfo("Pocet zaznamu: " + PrisonerTable.Select(db).Count);

            printFunction("3.2 Seznam veznu");
            Collection<Prisoner> prisoners = PrisonerProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(prisoners[i]);
            }

            printFunction("3.3 Aktualizace vezne");
            prisoners[1].LastName = "Slunecna";
            PrisonerProxy.Update(prisoners[1], db);
            prisoners = PrisonerProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(prisoners[i]);
            }

            printFunction("3.4 Propustit vezne");
            PrisonerProxy.Release(3, db);
            prisoners = PrisonerProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(prisoners[i]);
            }

            printFunction("3.5 Detail vezne");
            prisoner = PrisonerProxy.Select(43, db);
            printInfo("Vypis detailu vezne s ID(" + 43 + "):");
            Console.WriteLine(prisoner);
            printTableEnd();

            //===== Tabulka PrisonerCellHistory =====//
            printTable("PrisonerCellHistory");
            printInfo("Pocet zaznamu: " + PrisonerCellHistoryTable.Select(db).Count);

            printFunction("4.1 Pridani zaznamu");
            PrisonerCellHistory prisonerCellHistory = new PrisonerCellHistory()
            {
                PrisonerCellHistory_id = 9,
                StartDate = DateTime.Parse("2020-04-01"),
                EndDate = DateTime.Today,
                Cell = new Cell() { Cell_id = 22 },
                Prisoner = new Prisoner() { Prisoner_id = 43 }
            };
            PrisonerCellHistoryProxy.Insert(prisonerCellHistory, db);
            printInfo("Pocet zaznamu: " + PrisonerCellHistoryTable.Select(db).Count);

            printFunction("4.2 Seznam zaznamu");
            Collection<PrisonerCellHistory> prisonerCellHistories = PrisonerCellHistoryProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(prisonerCellHistories[i]);
            }
            printTableEnd();

            //===== Table Visit =====//
            printTable("Visit");
            printInfo("Pocet zaznamu: " + VisitTable.Select(db).Count);

            printFunction("5.1 Pridani navstevy");
            Visit visit = new Visit()
            {
                Visit_id = 13,
                VisitDate = DateTime.Parse("2020-04-15"),
                Allowed = '2',
                Prisoner = new Prisoner() { Prisoner_id = 43 },
                Visitor = new Visitor() { Visitor_id = 4 }
            };
            VisitProxy.Insert(visit, db);
            printInfo("Pocet zaznamu: " + VisitTable.Select(db).Count);

            printFunction("5.2 Seznam navstev");
            Collection<Visit> visits = VisitProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(visits[i]);
            }

            printFunction("5.3 Aktualizace navstevy");
            visits[3].Allowed = '1';
            VisitProxy.Update(visits[3], db);
            visits = VisitProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(visits[i]);
            }

            printFunction("5.4 Detail navstevy");
            visit = VisitProxy.Select(13, db);
            printInfo("Vypis detailu navstevy s ID(" + 13 + "):");
            Console.WriteLine(visit);
            printTableEnd();

            //===== Tabulka Visitor =====//
            printTable("Visitor");
            printInfo("Pocet zaznamu: " + VisitorTable.Select(db).Count);

            printFunction("6.1 Pridani navstevnika");
            Visitor visitor = new Visitor()
            {
                Visitor_id = 11,
                FirstName = "Zuzana",
                LastName = "Svobodna",
                Gender = 'F',
                BirthDate = DateTime.Parse("1969-09-06"),
                Active = '0',
                Forbidden = '0'
            };
            VisitorProxy.Insert(visitor, db);
            printInfo("Pocet zaznamu: " + VisitorTable.Select(db).Count);

            printFunction("6.2 Seznam navstevniku");
            Collection<Visitor> visitors = VisitorProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(visitors[i]);
            }

            printFunction("6.3 Aktualizace navstevnika");
            visitors[2].LastName = "Nerudova";
            VisitorProxy.Update(visitors[2], db);
            visitors = VisitorProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(visitors[i]);
            }

            printFunction("6.4 Aktualizace aktivity navstevnika");
            VisitorProxy.UpdateActivity(3, db);
            visitors = VisitorProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(visitors[i]);
            }

            printFunction("6.5 Zakazani navstevnika");
            VisitorProxy.Forbid(3, db);
            visitors = VisitorProxy.Select();
            printInfo("Vypis prvnich " + logCountLimit + " zaznamu:");
            for (int i = 0; i < logCountLimit; i++)
            {
                Console.WriteLine(visitors[i]);
            }

            printFunction("6.6 Detail navstevnika");
            visitor = VisitorProxy.Select(11, db);
            printInfo("Vypis detailu navstevnika s ID(" + 11 + "):");
            Console.WriteLine(visitor);
            printTableEnd();

            Console.Read();
            db.Close();
        }

        private static void printTable(string name)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==================================================");
            Console.WriteLine("Tabulka " + name);
            Console.WriteLine("==================================================");
            Console.ResetColor();
        }

        private static void printTableEnd()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==================================================\n");
            Console.ResetColor();
        }

        private static void printFunction(string name)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + name);
            Console.ResetColor();
        }

        private static void printInfo(string info)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(info);
            Console.ResetColor();
        }
    }
}