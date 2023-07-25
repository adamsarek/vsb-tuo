using Prison.Classes;
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
    public partial class FormAddCell : Form
    {
        public FormAddCell()
        {
            InitializeComponent();
        }

        private void addCellButton_Click(object sender, EventArgs e)
        {
            // Insert cell
            Cell cell = new Cell() {
                Cell_id = FormCellMenu.Cell.Cell_id,
                Occupied = 0,
                Capacity = int.Parse(capacityInput.Value.ToString()),
                Prison = Session.LoggedEmployee.Prison
            };
            CellProxy.Insert(cell);

            // Close form
            this.Close();
        }
    }
}
