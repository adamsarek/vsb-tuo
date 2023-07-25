using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class AddVisitorWinForm : Form
    {
        private VisitorLogic visitorLogic = new VisitorLogic();

        private int minVisitorAge = 0;

        public AddVisitorWinForm()
        {
            InitializeComponent();

            birthDateInput.MaxDate = DateTime.Today.AddYears(-minVisitorAge).AddDays(-1);
        }

        private void addVisitorButton_Click(object sender, EventArgs e)
        {
            // Insert visitor
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
                visitorLogic.Insert(visitor);

                MessageBox.Show("Návštěvník byl úspěšně přidán.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
