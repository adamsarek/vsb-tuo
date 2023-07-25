using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class AddEmployeeWinForm : Form
    {
        private EmployeeLogic employeeLogic = new EmployeeLogic();

        private int minEmployeeAge = 18;

        public AddEmployeeWinForm()
        {
            InitializeComponent();

            birthDateInput.MaxDate = DateTime.Today.AddYears(-minEmployeeAge).AddDays(-1);
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            // Insert employee
            Employee employee = new Employee()
            {
                EmployeeId = MenuWinForm.Employee.EmployeeId,
                FirstName = firstNameInput.Text,
                LastName = lastNameInput.Text,
                Gender = (genderRadioMale.Checked ? 'M' : 'F'),
                BirthDate = birthDateInput.Value,
                Warden = (wardenCheckbox.Checked ? '1' : '0'),
                Fired = '0'
            };

            try
            {
                employeeLogic.Insert(employee);

                MessageBox.Show("Zaměstnanec byl úspěšně přidán.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
