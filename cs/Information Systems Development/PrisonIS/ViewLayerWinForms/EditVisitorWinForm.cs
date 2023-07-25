using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class EditVisitorWinForm : Form
    {
        private VisitorLogic visitorLogic = new VisitorLogic();

        private int minVisitorAge = 0;

        public EditVisitorWinForm()
        {
            InitializeComponent();

            firstNameInput.Text = MenuWinForm.Visitor.FirstName;
            lastNameInput.Text = MenuWinForm.Visitor.LastName;
            if (MenuWinForm.Visitor.Gender == 'M') { genderRadioMale.Checked = true; } else { genderRadioFemale.Checked = true; }
            birthDateInput.MaxDate = DateTime.Today.AddYears(-minVisitorAge).AddDays(-1);
            birthDateInput.Value = MenuWinForm.Visitor.BirthDate;
        }

        private void editVisitorButton_Click(object sender, EventArgs e)
        {
            // Update visitor
            Visitor visitor = new Visitor()
            {
                VisitorId = MenuWinForm.Visitor.VisitorId,
                FirstName = firstNameInput.Text,
                LastName = lastNameInput.Text,
                Gender = (genderRadioMale.Checked ? 'M' : 'F'),
                BirthDate = birthDateInput.Value,
                Forbidden = '0'
            };

            try
            {
                visitorLogic.Update(visitor);

                MessageBox.Show("Návštěvník byl úspěšně upraven.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
