using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class EditVisitWinForm : Form
    {
        private VisitLogic visitLogic = new VisitLogic();
        private VisitorLogic visitorLogic = new VisitorLogic();

        public EditVisitWinForm()
        {
            InitializeComponent();

            visitDateInput.Value = MenuWinForm.Visit.VisitDate;
            allowedSelect.Items.Add("Nepovolená");
            allowedSelect.Items.Add("Povolená");
            allowedSelect.Items.Add("Nerozhodnutá");
            allowedSelect.SelectedIndex = int.Parse(MenuWinForm.Visit.Allowed.ToString());
            visitorLabel.Text = MenuWinForm.Visit.Visitor.ToString();
        }

        private void editVisitButton_Click(object sender, EventArgs e)
        {
            // Update visit
            Visit visit = new Visit()
            {
                VisitId = MenuWinForm.Visit.VisitId,
                VisitDate = visitDateInput.Value,
                Allowed = char.Parse(allowedSelect.SelectedIndex.ToString()),
                Prisoner = MenuWinForm.Visit.Prisoner,
                Visitor = MenuWinForm.Visit.Visitor
            };

            try
            {
                visitLogic.Update(visit);

                MessageBox.Show("Návštěva byla úspěšně upravena.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
