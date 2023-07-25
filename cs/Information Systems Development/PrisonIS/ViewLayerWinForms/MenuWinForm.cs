using Common.Class;
using DomainLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class MenuWinForm : Form
    {
        private CellLogic cellLogic = new CellLogic();
        private PrisonerLogic prisonerLogic = new PrisonerLogic();
        private VisitLogic visitLogic = new VisitLogic();
        private VisitorLogic visitorLogic = new VisitorLogic();
        private EmployeeLogic employeeLogic = new EmployeeLogic();

        private Collection<Cell> Cells = new Collection<Cell>();
        private Collection<Prisoner> CellPrisoners = new Collection<Prisoner>();
        private Collection<Visit> CellPrisonerVisits = new Collection<Visit>();
        private Collection<Visitor> Visitors = new Collection<Visitor>();
        private Collection<Employee> Employees = new Collection<Employee>();

        private int selectedCellRowIndex = 0;
        private int selectedCellPrisonerRowIndex = 0;
        private int selectedCellPrisonerVisitRowIndex = 0;
        private int selectedVisitorRowIndex = 0;
        private int selectedEmployeeRowIndex = 0;

        public static Cell Cell = null;
        public static Prisoner Prisoner = null;
        public static Visit Visit = null;
        public static Visitor Visitor = null;
        public static Employee Employee = null;

        public MenuWinForm()
        {
            InitializeComponent();

            // Setup data grids
            cellDataGrid.AutoGenerateColumns = false;
            cellDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            cellDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "CellId", DataPropertyName = "CellId" });
            cellDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Obsazeno", Name = "Occupied", DataPropertyName = "Occupied" });
            cellDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Kapacita", Name = "Capacity", DataPropertyName = "Capacity" });
            cellDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            prisonerDataGrid.AutoGenerateColumns = false;
            prisonerDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "PrisonerId", DataPropertyName = "PrisonerId" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Jméno", Name = "FirstName", DataPropertyName = "FirstName" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Příjmení", Name = "LastName", DataPropertyName = "LastName" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Pohlaví", Name = "FullGender", DataPropertyName = "FullGender" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Datum narození", Name = "BirthDate", DataPropertyName = "BirthDate" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Trest od", Name = "PunishmentStartDate", DataPropertyName = "PunishmentStartDate" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Trest do", Name = "PunishmentEndDate", DataPropertyName = "PunishmentEndDate" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Propuštěn", Name = "FullReleased", DataPropertyName = "FullReleased" });
            prisonerDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            visitDataGrid.AutoGenerateColumns = false;
            visitDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "VisitId", DataPropertyName = "VisitId" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Datum návštěvy", Name = "VisitDate", DataPropertyName = "VisitDate" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Povolení", Name = "FullAllowed", DataPropertyName = "FullAllowed" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Jméno návštěvníka", Name = "Visitor_FullName", DataPropertyName = "Visitor_FullName" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Pohlaví", Name = "Visitor_FullGender", DataPropertyName = "Visitor_FullGender" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Věk", Name = "Visitor_Age", DataPropertyName = "Visitor_Age" });
            visitDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            visitorDataGrid.AutoGenerateColumns = false;
            visitorDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            visitorDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "VisitorId", DataPropertyName = "VisitorId" });
            visitorDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Jméno", Name = "FirstName", DataPropertyName = "FirstName" });
            visitorDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Příjmení", Name = "LastName", DataPropertyName = "LastName" });
            visitorDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Pohlaví", Name = "FullGender", DataPropertyName = "FullGender" });
            visitorDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Datum narození", Name = "BirthDate", DataPropertyName = "BirthDate" });
            visitorDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Zakázán", Name = "FullForbidden", DataPropertyName = "FullForbidden" });
            visitorDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            employeeDataGrid.AutoGenerateColumns = false;
            employeeDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            employeeDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "EmployeeId", DataPropertyName = "EmployeeId" });
            employeeDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Jméno", Name = "FirstName", DataPropertyName = "FirstName" });
            employeeDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Příjmení", Name = "LastName", DataPropertyName = "LastName" });
            employeeDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Pohlaví", Name = "FullGender", DataPropertyName = "FullGender" });
            employeeDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Datum narození", Name = "BirthDate", DataPropertyName = "BirthDate" });
            employeeDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Správce", Name = "FullWarden", DataPropertyName = "FullWarden" });
            employeeDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Propuštěn", Name = "FullFired", DataPropertyName = "FullFired" });
            employeeDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            addVisitorButton.Enabled = true;
            addEmployeeButton.Enabled = true;

            loadCells();
            loadVisitors();
            loadEmployees();
        }

        private void loadCells()
        {
            // Clear data grid
            cellDataGrid.DataSource = null;
            cellDataGrid.Rows.Clear();

            editCellButton.Enabled = false;
            addPrisonerButton.Enabled = false;
            editPrisonerButton.Enabled = false;
            releasePrisonerButton.Enabled = false;
            addVisitButton.Enabled = false;
            editVisitButton.Enabled = false;

            Cell = null;

            try
            {
                Cells = cellLogic.Select();

                // Fill data grid
                cellDataGrid.DataSource = Cells;
                if (cellDataGrid.Rows.Count > 0)
                {
                    cellDataGrid.Rows[0].Selected = true;

                    editCellButton.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCellPrisoners()
        {
            // Clear data grid
            prisonerDataGrid.DataSource = null;
            prisonerDataGrid.Rows.Clear();

            editPrisonerButton.Enabled = false;
            releasePrisonerButton.Enabled = false;
            addVisitButton.Enabled = false;
            editVisitButton.Enabled = false;

            Prisoner = null;

            try
            {
                CellPrisoners = prisonerLogic.SelectForCell(Cells[selectedCellRowIndex].CellId);

                // Fill data grid
                prisonerDataGrid.DataSource = CellPrisoners;
                if (prisonerDataGrid.Rows.Count > 0)
                {
                    prisonerDataGrid.Rows[0].Selected = true;

                    editPrisonerButton.Enabled = true;
                    releasePrisonerButton.Enabled = true;
                    if (Visitors.Count > 0)
                    {
                        addVisitButton.Enabled = true;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCellPrisonerVisits()
        {
            // Clear data grid
            visitDataGrid.DataSource = null;
            visitDataGrid.Rows.Clear();

            editVisitButton.Enabled = false;

            Visit = null;

            try
            {
                CellPrisonerVisits = visitLogic.SelectForPrisoner(CellPrisoners[selectedCellPrisonerRowIndex].PrisonerId);

                // Fill data grid
                visitDataGrid.DataSource = CellPrisonerVisits;
                if (visitDataGrid.Rows.Count > 0)
                {
                    visitDataGrid.Rows[0].Selected = true;

                    editVisitButton.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadVisitors()
        {
            // Clear data grid
            visitorDataGrid.DataSource = null;
            visitorDataGrid.Rows.Clear();

            addVisitButton.Enabled = false;
            editVisitorButton.Enabled = false;
            forbidVisitorButton.Enabled = false;

            Visitor = null;

            try
            {
                Visitors = visitorLogic.SelectAllowed();

                // Fill data grid
                visitorDataGrid.DataSource = Visitors;
                if (visitorDataGrid.Rows.Count > 0)
                {
                    visitorDataGrid.Rows[0].Selected = true;

                    if (CellPrisoners.Count > 0)
                    {
                        addVisitButton.Enabled = true;
                    }
                    editVisitorButton.Enabled = true;
                    forbidVisitorButton.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadEmployees()
        {
            // Clear data grid
            employeeDataGrid.DataSource = null;
            employeeDataGrid.Rows.Clear();

            editEmployeeButton.Enabled = false;
            fireEmployeeButton.Enabled = false;

            Employee = null;

            try
            {
                Employees = employeeLogic.Select();

                // Fill data grid
                employeeDataGrid.DataSource = Employees;
                if (employeeDataGrid.Rows.Count > 0)
                {
                    employeeDataGrid.Rows[0].Selected = true;

                    editEmployeeButton.Enabled = true;
                    fireEmployeeButton.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cellDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (cellDataGrid.SelectedRows.Count > 0 && cellDataGrid.SelectedRows[0] != null && cellDataGrid.SelectedRows[0].Index != -1)
            {
                selectedCellRowIndex = cellDataGrid.SelectedRows[0].Index;

                Cell = Cells[selectedCellRowIndex];

                if (Cell.Occupied < Cell.Capacity)
                {
                    addPrisonerButton.Enabled = true;
                }
                else
                {
                    addPrisonerButton.Enabled = false;
                }
            }

            loadCellPrisoners();
        }

        private void prisonerDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (prisonerDataGrid.SelectedRows.Count > 0 && prisonerDataGrid.SelectedRows[0] != null && prisonerDataGrid.SelectedRows[0].Index != -1)
            {
                selectedCellPrisonerRowIndex = prisonerDataGrid.SelectedRows[0].Index;

                Prisoner = CellPrisoners[selectedCellPrisonerRowIndex];
            }

            loadCellPrisonerVisits();
        }

        private void visitDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (visitDataGrid.SelectedRows.Count > 0 && visitDataGrid.SelectedRows[0] != null && visitDataGrid.SelectedRows[0].Index != -1)
            {
                selectedCellPrisonerVisitRowIndex = visitDataGrid.SelectedRows[0].Index;

                Visit = CellPrisonerVisits[selectedCellPrisonerVisitRowIndex];
            }
        }

        private void visitorDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (visitorDataGrid.SelectedRows.Count > 0 && visitorDataGrid.SelectedRows[0] != null && visitorDataGrid.SelectedRows[0].Index != -1)
            {
                selectedVisitorRowIndex = visitorDataGrid.SelectedRows[0].Index;

                Visitor = Visitors[selectedVisitorRowIndex];
            }
        }

        private void employeeDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (employeeDataGrid.SelectedRows.Count > 0 && employeeDataGrid.SelectedRows[0] != null && employeeDataGrid.SelectedRows[0].Index != -1)
            {
                selectedEmployeeRowIndex = employeeDataGrid.SelectedRows[0].Index;

                Employee = Employees[selectedEmployeeRowIndex];
            }
        }

        private void addCellButton_Click(object sender, EventArgs e)
        {
            Cell = new Cell() { CellId = 0, Occupied = 0, Capacity = 1 };

            AddCellWinForm formAddCell = new AddCellWinForm();
            formAddCell.FormClosed += (s, args) => {
                loadCells();
            };
            formAddCell.ShowDialog();
        }

        private void editCellButton_Click(object sender, EventArgs e)
        {
            EditCellWinForm formEditCell = new EditCellWinForm();
            formEditCell.FormClosed += (s, args) => {
                loadCells();
            };
            formEditCell.ShowDialog();
        }

        private void addPrisonerButton_Click(object sender, EventArgs e)
        {
            Prisoner = new Prisoner() { PrisonerId = 0, FirstName = "", LastName = "", Gender = 'M', BirthDate = new DateTime(), PunishmentStartDate = new DateTime(), PunishmentEndDate = new DateTime(), Released = '0', Cell = Cell };

            AddPrisonerWinForm formAddPrisoner = new AddPrisonerWinForm();
            formAddPrisoner.FormClosed += (s, args) => {
                loadCells();
            };
            formAddPrisoner.ShowDialog();
        }

        private void editPrisonerButton_Click(object sender, EventArgs e)
        {
            EditPrisonerWinForm formEditPrisoner = new EditPrisonerWinForm();
            formEditPrisoner.FormClosed += (s, args) => {
                loadCellPrisoners();
            };
            formEditPrisoner.ShowDialog();
        }

        private void releasePrisonerButton_Click(object sender, EventArgs e)
        {
            try
            {
                prisonerLogic.Release(Prisoner);

                MessageBox.Show("Vězeň byl úspěšně propuštěn.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadCells();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addVisitButton_Click(object sender, EventArgs e)
        {
            Visit = new Visit() { VisitId = 0, VisitDate = new DateTime(), Allowed = '2', Prisoner = Prisoner };

            AddVisitWinForm formAddVisit = new AddVisitWinForm();
            formAddVisit.FormClosed += (s, args) => {
                loadCellPrisonerVisits();
            };
            formAddVisit.ShowDialog();
        }

        private void editVisitButton_Click(object sender, EventArgs e)
        {
            EditVisitWinForm formEditVisit = new EditVisitWinForm();
            formEditVisit.FormClosed += (s, args) => {
                loadCellPrisonerVisits();
            };
            formEditVisit.ShowDialog();
        }

        private void addVisitorButton_Click(object sender, EventArgs e)
        {
            Visitor = new Visitor() { VisitorId = 0, FirstName = "", LastName = "", Gender = 'M', BirthDate = new DateTime(), Forbidden = '0' };

            AddVisitorWinForm formAddVisitor = new AddVisitorWinForm();
            formAddVisitor.FormClosed += (s, args) => {
                loadVisitors();
                loadCellPrisonerVisits();
            };
            formAddVisitor.ShowDialog();
        }

        private void editVisitorButton_Click(object sender, EventArgs e)
        {
            EditVisitorWinForm formEditVisitor = new EditVisitorWinForm();
            formEditVisitor.FormClosed += (s, args) => {
                loadVisitors();
                loadCellPrisonerVisits();
            };
            formEditVisitor.ShowDialog();
        }

        private void forbidVisitorButton_Click(object sender, EventArgs e)
        {
            try
            {
                visitorLogic.Forbid(Visitor);

                MessageBox.Show("Návštěvník byl úspěšně zakázán.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadVisitors();
                loadCellPrisonerVisits();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            Employee = new Employee() { EmployeeId = 0, FirstName = "", LastName = "", Gender = 'M', BirthDate = new DateTime(), Warden = '0', Fired = '0' };

            AddEmployeeWinForm formAddEmployee = new AddEmployeeWinForm();
            formAddEmployee.FormClosed += (s, args) => {
                loadEmployees();
            };
            formAddEmployee.ShowDialog();
        }

        private void editEmployeeButton_Click(object sender, EventArgs e)
        {
            EditEmployeeWinForm formEditEmployee = new EditEmployeeWinForm();
            formEditEmployee.FormClosed += (s, args) => {
                loadEmployees();
            };
            formEditEmployee.ShowDialog();
        }

        private void fireEmployeeButton_Click(object sender, EventArgs e)
        {
            try
            {
                employeeLogic.Fire(Employee);

                MessageBox.Show("Zaměstnanec byl úspěšně propuštěn.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadEmployees();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
