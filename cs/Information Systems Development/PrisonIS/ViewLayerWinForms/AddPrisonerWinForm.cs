using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class AddPrisonerWinForm : Form
    {
        private PrisonerLogic prisonerLogic = new PrisonerLogic();

        private int minPrisonerAge = 18;

        public AddPrisonerWinForm()
        {
            InitializeComponent();

            birthDateInput.MaxDate = DateTime.Today.AddYears(-minPrisonerAge).AddDays(-1);

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
                prisonerLogic.Insert(prisoner);

                MessageBox.Show("Vězeň byl úspěšně přidán.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
