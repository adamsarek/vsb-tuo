using Common.Class;
using DomainLayer;
using System;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class AddCellWebForm : System.Web.UI.Page
    {
        private CellLogic cellLogic = new CellLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CapacityInput.Attributes.Add("value", "1");
                CapacityInput.Attributes.Add("min", "1");
            }
        }

        protected void AddCell_Click(object obj, EventArgs e)
        {
            Cell cell = new Cell()
            {
                CellId = 0,
                Occupied = 0,
                Capacity = int.Parse(CapacityInput.Value)
            };

            try
            {
                cellLogic.Insert(cell);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Cela byla úspěšně přidána.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}