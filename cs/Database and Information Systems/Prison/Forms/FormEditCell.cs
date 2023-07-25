using PrisonORM.Database;
using PrisonORM.Database.proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prison.Forms
{
    public partial class FormEditCell : Form
    {
        public FormEditCell()
        {
            InitializeComponent();

            capacityInput.Minimum = (FormCellMenu.Cell.Occupied >= 1 ? FormCellMenu.Cell.Occupied : 1);
            capacityInput.Value = FormCellMenu.Cell.Capacity;
        }

        private void editCellButton_Click(object sender, EventArgs e)
        {
            // Update cell
            Cell cell = FormCellMenu.Cell;
            cell.Capacity = int.Parse(capacityInput.Value.ToString());
            CellProxy.Update(cell);

            // Close form
            this.Close();
        }
    }
}
