using Common.Class;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ViewLayerWebForms
{
    public partial class EditCellWebForm : System.Web.UI.Page
    {
        private CellLogic cellLogic = new CellLogic();

        private static Cell cell;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cell = cellLogic.Select(int.Parse(Request.QueryString["cellId"]));

                CapacityInput.Attributes.Add("value", cell.Capacity.ToString());
                CapacityInput.Attributes.Add("min", (cell.Occupied < 1 ? 1 : cell.Occupied).ToString());
            }
        }

        protected void EditCell_Click(object obj, EventArgs e)
        {
            cell = new Cell()
            {
                CellId = cell.CellId,
                Occupied = cell.Occupied,
                Capacity = int.Parse(CapacityInput.Value)
            };

            try
            {
                cellLogic.Update(cell);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Cela byla úspěšně upravena.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}