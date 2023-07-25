using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class EditEmployeeWinForm : Form
    {
        private EmployeeLogic employeeLogic = new EmployeeLogic();

        private int minEmployeeAge = 18;

        public EditEmployeeWinForm()
        {
            InitializeComponent();

            firstNameInput.Text = MenuWinForm.Employee.FirstName;
            lastNameInput.Text = MenuWinForm.Employee.LastName;
            if (MenuWinForm.Employee.Gender == 'M') { genderRadioMale.Checked = true; } else { genderRadioFemale.Checked = true; }
            birthDateInput.MaxDate = DateTime.Today.AddYears(-minEmployeeAge).AddDays(-1);
            birthDateInput.Value = MenuWinForm.Employee.BirthDate;
            if (MenuWinForm.Employee.Warden == '1') { wardenCheckbox.Checked = true; } else { wardenCheckbox.Checked = false; }
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            // Update employee
            Employee employee = new Employee()
            {
                EmployeeId = MenuWinForm.Employee.EmployeeId,
                FirstName = firstNameInput.Text,
                LastName = lastNameInput.Text,
                Gender = (genderRadioMale.Checked ? 'M' : 'F'),
                BirthDate = birthDateInput.Value,
                Warden = (wardenCheckbox.Checked ? '1' : '0'),
                Fired = MenuWinForm.Employee.Fired
            };

            try
            {
                employeeLogic.Update(employee);

                MessageBox.Show("Zaměstnanec byl úspěšně upraven.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
