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
    public partial class FormAddVisit : Form
    {
        private Collection<Visitor> Visitors = new Collection<Visitor>();
        private Collection<Visitor> AllowedVisitors = new Collection<Visitor>();

        public FormAddVisit()
        {
            InitializeComponent();

            allowedSelect.Items.Add("Povolená");
            allowedSelect.Items.Add("Nerozhodnutá");
            allowedSelect.SelectedIndex = 1;

            loadVisitors();

            displayVisitors();
        }

        private void loadVisitors()
        {
            Visitors = VisitorTable.Select();

            // Map allowed visitors
            AllowedVisitors.Clear();
            for (int i = 0; i < Visitors.Count; i++)
            {
                if (Visitors[i].Forbidden == '0')
                {
                    AllowedVisitors.Add(Visitors[i]);
                }
            }
        }

        private void displayVisitors(int selectedVisitorRowIndex = 0)
        {
            visitorSelect.DataSource = null;
            visitorSelect.Items.Clear();

            visitorSelect.DataSource = AllowedVisitors;
            if (visitorSelect.Items.Count > 0) { visitorSelect.SelectedIndex = selectedVisitorRowIndex; }
        }

        private void addPrisonerButton_Click(object sender, EventArgs e)
        {
            // Insert visit
            Visit visit = new Visit()
            {
                Visit_id = FormEditPrisoner.Visit.Visit_id,
                VisitDate = visitDateInput.Value,
                Allowed = char.Parse((allowedSelect.SelectedIndex + 1).ToString()),
                Prisoner = FormEditPrisoner.Visit.Prisoner,
                Visitor = AllowedVisitors[visitorSelect.SelectedIndex]
            };
            VisitProxy.Insert(visit);

            // Close form
            this.Close();
        }
    }
}
