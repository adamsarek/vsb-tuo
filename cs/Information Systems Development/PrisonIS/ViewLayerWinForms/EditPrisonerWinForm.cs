using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class EditPrisonerWinForm : Form
    {
        private PrisonerLogic prisonerLogic = new PrisonerLogic();

        private int minPrisonerAge = 18;

        public EditPrisonerWinForm()
        {
            InitializeComponent();

            firstNameInput.Text = MenuWinForm.Prisoner.FirstName;
            lastNameInput.Text = MenuWinForm.Prisoner.LastName;
            if (MenuWinForm.Prisoner.Gender == 'M') { genderRadioMale.Checked = true; } else { genderRadioFemale.Checked = true; }
            birthDateInput.MaxDate = DateTime.Today.AddYears(-minPrisonerAge).AddDays(-1);
            birthDateInput.Value = MenuWinForm.Prisoner.BirthDate;
            punishmentStartDateInput.MinDate = birthDateInput.Value.AddYears(minPrisonerAge);
            punishmentStartDateInput.MaxDate = birthDateInput.MaxDate.AddYears(minPrisonerAge);
            punishmentStartDateInput.Value = MenuWinForm.Prisoner.PunishmentStartDate;
            punishmentEndDateInput.MinDate = punishmentStartDateInput.Value.AddDays(1);
            punishmentEndDateInput.Value = MenuWinForm.Prisoner.PunishmentEndDate;
        }

        private void birthDateInput_CloseUp(object sender, EventArgs e)
        {
            punishmentStartDateInput.MinDate = birthDateInput.Value.AddYears(minPrisonerAge);
        }

        private void punishmentStartDateInput_ValueChanged(object sender, EventArgs e)
        {
            punishmentEndDateInput.MinDate = punishmentStartDateInput.Value.AddDays(1);
        }

        private void editPrisonerButton_Click_1(object sender, EventArgs e)
        {
            // Update prisoner
            Prisoner prisoner = new Prisoner()
            {
                PrisonerId = MenuWinForm.Prisoner.PrisonerId,
                FirstName = firstNameInput.Text,
                LastName = lastNameInput.Text,
                Gender = (genderRadioMale.Checked ? 'M' : 'F'),
                BirthDate = birthDateInput.Value,
                PunishmentStartDate = punishmentStartDateInput.Value,
                PunishmentEndDate = punishmentEndDateInput.Value,
                Released = '0',
                Cell = MenuWinForm.Prisoner.Cell
            };

            try
            {
                prisonerLogic.Update(prisoner);

                MessageBox.Show("Vězeň byl úspěšně upraven.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close form
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
