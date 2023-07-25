using PrisonORM.Database;
using PrisonORM.Database.mssql;
using PrisonORM.Database.proxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prison.Forms
{
    public partial class FormEditPrisoner : Form
    {
        private class PrisonerVisit
        {
            public int Visit_id { get; set; }
            public DateTime VisitDate { get; set; }
            public char Allowed { get; set; }
            public Prisoner Prisoner { get; set; }
            public Visitor Visitor { get; set; }
            public string FullAllowed { get; set; }
            public string Visitor_FullName { get; set; }
            public string Visitor_FullGender { get; set; }
            public int Visitor_Age { get; set; }
        };

        private int minPrisonerAge = 18;
        private Collection<Visit> Visits = new Collection<Visit>();
        private Collection<PrisonerVisit> PrisonerVisits = new Collection<PrisonerVisit>();

        public static Visit Visit = new Visit();

        public FormEditPrisoner()
        {
            InitializeComponent();

            firstNameInput.Text = FormCellMenu.Prisoner.FirstName;
            lastNameInput.Text = FormCellMenu.Prisoner.LastName;
            if (FormCellMenu.Prisoner.Gender == 'M') { genderRadioMale.Checked = true; } else { genderRadioFemale.Checked = true; }
            birthDateInput.MaxDate = DateTime.Today.AddYears(-minPrisonerAge).AddDays(-1);
            birthDateInput.Value = FormCellMenu.Prisoner.BirthDate;
            punishmentStartDateInput.MinDate = birthDateInput.Value.AddYears(minPrisonerAge);
            punishmentStartDateInput.MaxDate = birthDateInput.MaxDate.AddYears(minPrisonerAge);
            punishmentStartDateInput.Value = FormCellMenu.Prisoner.PunishmentStartDate;
            punishmentEndDateInput.MinDate = punishmentStartDateInput.Value.AddDays(1);
            punishmentEndDateInput.Value = FormCellMenu.Prisoner.PunishmentEndDate;

            firstNameLabel.Text = FormCellMenu.Prisoner.FirstName;
            lastNameLabel.Text = FormCellMenu.Prisoner.LastName;
            genderLabel.Text = FormCellMenu.Prisoner.FullGender;
            birthDateLabel.Text = FormCellMenu.Prisoner.BirthDate.ToShortDateString();
            punishmentStartDateLabel.Text = FormCellMenu.Prisoner.PunishmentStartDate.ToShortDateString();
            punishmentEndDateLabel.Text = FormCellMenu.Prisoner.PunishmentEndDate.ToShortDateString();

            // Prepare data grids
            visitDataGrid.AutoGenerateColumns = false;
            visitDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "#", Name = "Visit_id", DataPropertyName = "Visit_id" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Datum návštěvy", Name = "VisitDate", DataPropertyName = "VisitDate" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Povolení", Name = "FullAllowed", DataPropertyName = "FullAllowed" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Jméno návštěvníka", Name = "Visitor_FullName", DataPropertyName = "Visitor_FullName" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Pohlaví", Name = "Visitor_FullGender", DataPropertyName = "Visitor_FullGender" });
            visitDataGrid.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Věk", Name = "Visitor_Age", DataPropertyName = "Visitor_Age" });
            visitDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            loadVisits();

            displayVisits();
        }

        private void birthDateInput_CloseUp(object sender, EventArgs e)
        {
            punishmentStartDateInput.MinDate = birthDateInput.Value.AddYears(minPrisonerAge);
        }

        private void punishmentStartDateInput_ValueChanged(object sender, EventArgs e)
        {
            punishmentEndDateInput.MinDate = punishmentStartDateInput.Value.AddDays(1);
        }

        private void editPrisonerButton_Click(object sender, EventArgs e)
        {
            // Update prisoner
            Prisoner prisoner = new Prisoner()
            {
                Prisoner_id = FormCellMenu.Prisoner.Prisoner_id,
                FirstName = firstNameInput.Text,
                LastName = lastNameInput.Text,
                Gender = (genderRadioMale.Checked ? 'M' : 'F'),
                BirthDate = birthDateInput.Value,
                PunishmentStartDate = punishmentStartDateInput.Value,
                PunishmentEndDate = punishmentEndDateInput.Value,
                Released = '0',
                Cell = FormCellMenu.Prisoner.Cell
            };
            PrisonerProxy.Update(prisoner);

            // Close form
            this.Close();
        }

        private void releasePrisonerButton_Click(object sender, EventArgs e)
        {
            // Release prisoner
            PrisonerProxy.Release(FormCellMenu.Prisoner.Prisoner_id);

            // Close form
            this.Close();
        }

        private void loadVisits()
        {
            Visits = VisitTable.Select();

            // Map visits
            PrisonerVisits.Clear();
            for (int i = 0; i < Visits.Count; i++)
            {
                if (Visits[i].Prisoner.Prisoner_id == FormCellMenu.Prisoner.Prisoner_id && Visits[i].Allowed != '0')
                {
                    PrisonerVisit prisonerVisit = new PrisonerVisit() {
                        Visit_id = Visits[i].Visit_id,
                        VisitDate = Visits[i].VisitDate,
                        Allowed = Visits[i].Allowed,
                        Prisoner = Visits[i].Prisoner,
                        Visitor = Visits[i].Visitor,
                        FullAllowed = Visits[i].FullAllowed,
                        Visitor_FullName = Visits[i].Visitor.FullName,
                        Visitor_FullGender = Visits[i].Visitor.FullGender,
                        Visitor_Age = Visits[i].Visitor.Age
                    };
                    PrisonerVisits.Add(prisonerVisit);
                }
            }
        }

        private void displayVisits(int selectedVisitRowIndex = 0)
        {
            visitDataGrid.DataSource = null;
            visitDataGrid.Rows.Clear();

            visitDataGrid.DataSource = PrisonerVisits;
            if (visitDataGrid.Rows.Count > 0) { visitDataGrid.Rows[selectedVisitRowIndex].Selected = true; }
        }

        private void visitDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (visitDataGrid.SelectedRows.Count > 0 && visitDataGrid.SelectedRows[0] != null && visitDataGrid.SelectedRows[0].Index != -1)
            {
                editVisitButton.Enabled = true;
            }
        }

        private void addVisitButton_Click(object sender, EventArgs e)
        {
            Visit = new Visit() {
                Visit_id = Visits.Count + 1,
                VisitDate = DateTime.Today,
                Allowed = '2',
                Prisoner = FormCellMenu.Prisoner,
                Visitor = new Visitor()
            };

            FormAddVisit formAddVisit = new FormAddVisit();
            formAddVisit.FormClosed += (s, args) => {
                loadVisits();
                
                displayVisits(PrisonerVisits.Count - 1);
            };
            formAddVisit.ShowDialog();
        }

        private void editVisitButton_Click(object sender, EventArgs e)
        {
            Visit = new Visit() {
                Visit_id = PrisonerVisits[visitDataGrid.SelectedRows[0].Index].Visit_id,
                VisitDate = PrisonerVisits[visitDataGrid.SelectedRows[0].Index].VisitDate,
                Allowed = PrisonerVisits[visitDataGrid.SelectedRows[0].Index].Allowed,
                Prisoner = PrisonerVisits[visitDataGrid.SelectedRows[0].Index].Prisoner,
                Visitor = PrisonerVisits[visitDataGrid.SelectedRows[0].Index].Visitor
            };

            FormEditVisit formEditVisit = new FormEditVisit();
            formEditVisit.FormClosing += (s, args) => {
                loadVisits();
                
                displayVisits();
            };
            formEditVisit.ShowDialog();
        }
    }
}
