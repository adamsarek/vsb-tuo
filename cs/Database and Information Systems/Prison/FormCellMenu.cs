using System;
using PrisonORM.Database.mssql;
using PrisonORM.Database.proxy;
using PrisonORM.Database;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Prison.Forms;
using Prison.Classes;

namespace Prison
{
    public partial class FormCellMenu : Form
    {
        private Collection<Cell> Cells = new Collection<Cell>();
        private Collection<Cell> PrisonCells = new Collection<Cell>();
        private Collection<Prisoner> Prisoners = new Collection<Prisoner>();
        private List<Collection<Prisoner>> CellPrisoners = new List<Collection<Prisoner>>();

        public static Cell Cell = new Cell();
        public static Prisoner Prisoner = new Prisoner();

        public FormCellMenu()
        {
            InitializeComponent();

            // Prepare buttons
            if(Session.LoggedEmployee.Warden == '1') {
                addCellButton.Enabled = true;
                editCellButton.Enabled = true;
            }

            // Prepare data grids
            cellDataGrid.AutoGenerateColumns = false;
            cellDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            cellDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "Cell_id", DataPropertyName = "Cell_id" });
            cellDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Obsazeno", Name = "Occupied", DataPropertyName = "Occupied" });
            cellDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Kapacita", Name = "Capacity", DataPropertyName = "Capacity" });
            cellDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            prisonerDataGrid.AutoGenerateColumns = false;
            prisonerDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "Prisoner_id", DataPropertyName = "Prisoner_id" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Jméno", Name = "FirstName", DataPropertyName = "FirstName" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Příjmení", Name = "LastName", DataPropertyName = "LastName" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Pohlaví", Name = "FullGender", DataPropertyName = "FullGender" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Datum narození", Name = "BirthDate", DataPropertyName = "BirthDate" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Trest od", Name = "PunishmentStartDate", DataPropertyName = "PunishmentStartDate" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Trest do", Name = "PunishmentEndDate", DataPropertyName = "PunishmentEndDate" });
            prisonerDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Propuštěn", Name = "FullReleased", DataPropertyName = "FullReleased" });
            prisonerDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            loadCells();
            loadPrisoners();

            displayCells();
        }

        private void loadCells()
        {
            Cells = CellTable.Select();

            // Map cells
            PrisonCells.Clear();
            for (int i = 0; i < Cells.Count; i++)
            {
                if (Cells[i].Prison.Prison_id == Session.LoggedEmployee.Prison.Prison_id)
                {
                    PrisonCells.Add(Cells[i]);
                }
            }
        }

        private void loadPrisoners()
        {
            Prisoners = PrisonerTable.Select();

            // Map cell prisoners
            CellPrisoners.Clear();
            for (int i = 0; i < PrisonCells.Count; i++)
            {
                CellPrisoners.Add(new Collection<Prisoner>());
                for (int j = 0; j < Prisoners.Count; j++)
                {
                    if (PrisonCells[i].Cell_id == Prisoners[j].Cell.Cell_id && Prisoners[j].Released == '0')
                    {
                        CellPrisoners[i].Add(Prisoners[j]);
                    }
                }
            }
        }
        
        private void displayCells(int selectedCellRowIndex = 0)
        {
            cellDataGrid.DataSource = null;
            cellDataGrid.Rows.Clear();

            cellDataGrid.DataSource = PrisonCells;
            if (cellDataGrid.Rows.Count > 0) { cellDataGrid.Rows[selectedCellRowIndex].Selected = true; }
        }

        private void displayCellPrisoners(int selectedCellRowIndex = 0)
        {
            prisonerDataGrid.DataSource = null;
            prisonerDataGrid.Rows.Clear();

            if (cellDataGrid.SelectedRows.Count > 0 && cellDataGrid.SelectedRows[0] != null && cellDataGrid.SelectedRows[0].Index != -1)
            {
                prisonerDataGrid.DataSource = CellPrisoners[cellDataGrid.SelectedRows[0].Index];
                if (PrisonCells[cellDataGrid.SelectedRows[0].Index].Occupied < PrisonCells[cellDataGrid.SelectedRows[0].Index].Capacity)
                {
                    addPrisonerButton.Enabled = true;
                }
                else
                {
                    addPrisonerButton.Enabled = false;
                }
                if (prisonerDataGrid.Rows.Count > 0)
                {
                    prisonerDataGrid.Rows[selectedCellRowIndex].Selected = true;
                    editPrisonerButton.Enabled = true;
                    releasePrisonerButton.Enabled = (Session.LoggedEmployee.Warden == '1' ? true : false);
                }
                else
                {
                    editPrisonerButton.Enabled = false;
                    releasePrisonerButton.Enabled = false;
                }
            }
        }

        private void cellDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            displayCellPrisoners();
        }

        private void addCellButton_Click(object sender, EventArgs e)
        {
            Cell = new Cell() { Cell_id = Cells.Count + 1, Occupied = 0, Capacity = 1, Prison = Session.LoggedEmployee.Prison };

            FormAddCell formAddCell = new FormAddCell();
            formAddCell.FormClosed += (s, args) => {
                loadCells();
                loadPrisoners();

                displayCells(PrisonCells.Count - 1);
            };
            formAddCell.ShowDialog();
        }

        private void editCellButton_Click(object sender, EventArgs e)
        {
            Cell = PrisonCells[cellDataGrid.SelectedRows[0].Index];

            FormEditCell formEditCell = new FormEditCell();
            formEditCell.FormClosing += (s, args) => {
                int selectedCellRowIndex = cellDataGrid.SelectedRows[0].Index;

                loadCells();
                loadPrisoners();

                displayCells(selectedCellRowIndex);
            };
            formEditCell.ShowDialog();
        }

        private void addPrisonerButton_Click(object sender, EventArgs e)
        {
            Prisoner = new Prisoner() { Prisoner_id = Prisoners.Count + 1, FirstName = "", LastName = "", Gender = 'M', BirthDate = new DateTime(), PunishmentStartDate = new DateTime(), PunishmentEndDate = new DateTime(), Released = '0', Cell = PrisonCells[cellDataGrid.SelectedRows[0].Index] };

            FormAddPrisoner formAddPrisoner = new FormAddPrisoner();
            formAddPrisoner.FormClosing += (s, args) => {
                int selectedCellRowIndex = cellDataGrid.SelectedRows[0].Index;

                loadCells();
                loadPrisoners();
                
                displayCells(selectedCellRowIndex);
                displayCellPrisoners(CellPrisoners[selectedCellRowIndex].Count - 1);
            };
            formAddPrisoner.ShowDialog();
        }

        private void editPrisonerButton_Click(object sender, EventArgs e)
        {
            Prisoner = CellPrisoners[cellDataGrid.SelectedRows[0].Index][prisonerDataGrid.SelectedRows[0].Index];

            FormEditPrisoner formEditPrisoner = new FormEditPrisoner();
            formEditPrisoner.FormClosing += (s, args) => {
                int selectedCellRowIndex = cellDataGrid.SelectedRows[0].Index;

                loadCells();
                loadPrisoners();

                displayCells(selectedCellRowIndex);
            };
            formEditPrisoner.ShowDialog();
        }

        private void releasePrisonerButton_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = CellPrisoners[cellDataGrid.SelectedRows[0].Index][prisonerDataGrid.SelectedRows[0].Index];
            PrisonerProxy.Release(prisoner.Prisoner_id);

            int selectedCellRowIndex = cellDataGrid.SelectedRows[0].Index;

            loadCells();
            loadPrisoners();

            displayCells(selectedCellRowIndex);
        }
    }
}
