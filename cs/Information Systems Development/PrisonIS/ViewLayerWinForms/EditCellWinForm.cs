using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class EditCellWinForm : Form
    {
        private CellLogic cellLogic = new CellLogic();

        public EditCellWinForm()
        {
            InitializeComponent();

            capacityInput.Minimum = (MenuWinForm.Cell.Occupied >= 1 ? MenuWinForm.Cell.Occupied : 1);
            capacityInput.Value = MenuWinForm.Cell.Capacity;
        }

        private void editCellButton_Click(object sender, EventArgs e)
        {
            // Update cell
            Cell cell = MenuWinForm.Cell;
            cell.Capacity = int.Parse(capacityInput.Value.ToString());

            try
            {
                cellLogic.Update(cell);

                MessageBox.Show("Cela byla úspěšně upravena.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
