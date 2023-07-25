using PrisonORM.Database;
using PrisonORM.Database.proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prison.Forms
{
    public partial class FormAddPrisoner : Form
    {
        private int minPrisonerAge = 18;

        public FormAddPrisoner()
        {
            InitializeComponent();

            birthDateInput.MaxDate = DateTime.Today.AddYears(- minPrisonerAge).AddDays(-1);

            punishmentStartDateInput.MinDate = birthDateInput.Value.AddYears(minPrisonerAge);
            punishmentStartDateInput.MaxDate = birthDateInput.MaxDate.AddYears(minPrisonerAge);

            punishmentEndDateInput.MinDate = punishmentStartDateInput.Value.AddDays(1);
        }

        private void birthDateInput_CloseUp(object sender, EventArgs e)
        {
            punishmentStartDateInput.MinDate = birthDateInput.Value.AddYears(minPrisonerAge);
        }

        private void punishmentStartDateInput_ValueChanged(object sender, EventArgs e)
        {
            punishmentEndDateInput.MinDate = punishmentStartDateInput.Value.AddDays(1);
        }

        private void addPrisonerButton_Click(object sender, EventArgs e)
        {
            // Insert prisoner
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
            PrisonerProxy.Insert(prisoner);

            // Close form
            this.Close();
        }
    }
}
