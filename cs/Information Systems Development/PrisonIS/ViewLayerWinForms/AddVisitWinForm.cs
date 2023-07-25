using Common.Class;
using DomainLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class AddVisitWinForm : Form
    {
        private VisitLogic visitLogic = new VisitLogic();
        private VisitorLogic visitorLogic = new VisitorLogic();

        private Collection<Visitor> Visitors = new Collection<Visitor>();

        public AddVisitWinForm()
        {
            InitializeComponent();

            allowedSelect.Items.Add("Povolená");
            allowedSelect.Items.Add("Nerozhodnutá");
            allowedSelect.SelectedIndex = 1;

            try
            {
                Visitors = visitorLogic.SelectAllowed();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            visitorSelect.DataSource = null;
            visitorSelect.Items.Clear();

            visitorSelect.DataSource = Visitors;
            if (visitorSelect.Items.Count > 0) { visitorSelect.SelectedIndex = 0; }
        }

        private void addPrisonerButton_Click(object sender, EventArgs e)
        {
            // Insert visit
            Visit visit = new Visit()
            {
                VisitId = MenuWinForm.Visit.VisitId,
                VisitDate = visitDateInput.Value,
                Allowed = char.Parse((allowedSelect.SelectedIndex + 1).ToString()),
                Prisoner = MenuWinForm.Visit.Prisoner,
                Visitor = Visitors[visitorSelect.SelectedIndex]
            };

            try
            {
                visitLogic.Insert(visit);

                MessageBox.Show("Návštěva byla úspěšně přidána.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
