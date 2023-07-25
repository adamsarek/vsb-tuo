using Common.Class;
using DomainLayer;
using System;
using System.Windows.Forms;

namespace ViewLayerWinForms
{
    public partial class AddCellWinForm : Form
    {
        private CellLogic cellLogic = new CellLogic();

        public AddCellWinForm()
        {
            InitializeComponent();
        }

        private void addCellButton_Click(object sender, EventArgs e)
        {
            // Insert cell
            Cell cell = new Cell()
            {
                CellId = MenuWinForm.Cell.CellId,
                Occupied = 0,
                Capacity = int.Parse(capacityInput.Value.ToString())
            };

            try
            {
                cellLogic.Insert(cell);

                MessageBox.Show("Cela byla úspěšně přidána.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
