using Common.Class;
using DomainLayer;
using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class MenuWebForm : System.Web.UI.Page
    {
        private CellLogic cellLogic = new CellLogic();
        private PrisonerLogic prisonerLogic = new PrisonerLogic();
        private VisitLogic visitLogic = new VisitLogic();
        private VisitorLogic visitorLogic = new VisitorLogic();

        private static Collection<Cell> Cells = new Collection<Cell>();
        private static Collection<Prisoner> CellPrisoners = new Collection<Prisoner>();
        private static Collection<Visit> CellPrisonerVisits = new Collection<Visit>();
        private static Collection<Visitor> Visitors = new Collection<Visitor>();

        private static int selectedCellRowIndex = -1;
        private static int selectedCellPrisonerRowIndex = -1;
        private static int selectedCellPrisonerVisitRowIndex = -1;
        private static int selectedVisitorRowIndex = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadCells();
                loadVisitors();
            }

            if (Cells.Count <= 0) { selectedCellRowIndex = -1; }
            if (CellPrisoners.Count <= 0) { selectedCellPrisonerRowIndex = -1; }
            if (CellPrisonerVisits.Count <= 0) { selectedCellPrisonerVisitRowIndex = -1; }
            if (Visitors.Count <= 0) { selectedVisitorRowIndex = -1; }
        }

        private void loadCells()
        {
            try { Cells = cellLogic.Select(); }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }

            // Fill list
            CellList.DataSource = Cells;
            CellList.DataBind();

            if (Cells.Count > 0)
            {
                CellList.SelectedIndex = selectedCellRowIndex = 0;

                loadCellPrisoners();
            }
        }

        private void loadCellPrisoners()
        {
            try { CellPrisoners = prisonerLogic.SelectForCell(Cells[selectedCellRowIndex].CellId); }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }

            // Fill list
            PrisonerList.DataSource = CellPrisoners;
            PrisonerList.DataBind();

            if (CellPrisoners.Count > 0)
            {
                PrisonerList.SelectedIndex = selectedCellPrisonerRowIndex = 0;

                loadCellPrisonerVisits();
            }
        }

        private void loadCellPrisonerVisits()
        {
            try { CellPrisonerVisits = visitLogic.SelectForPrisoner(CellPrisoners[selectedCellPrisonerRowIndex].PrisonerId); }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }

            // Fill list
            VisitList.DataSource = CellPrisonerVisits;
            VisitList.DataBind();

            if (CellPrisonerVisits.Count > 0) { VisitList.SelectedIndex = selectedCellPrisonerVisitRowIndex = 0; }
        }

        private void loadVisitors()
        {
            try { Visitors = visitorLogic.SelectAllowed(); }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }

            // Fill list
            VisitorList.DataSource = Visitors;
            VisitorList.DataBind();

            if (Visitors.Count > 0) { VisitorList.SelectedIndex = selectedVisitorRowIndex = 0; }
        }

        protected void CellList_SelectedIndexChanged(object obj, EventArgs e)
        {
            selectedCellRowIndex = CellList.SelectedIndex;

            loadCellPrisoners();
        }

        protected void PrisonerList_SelectedIndexChanged(object obj, EventArgs e)
        {
            selectedCellPrisonerRowIndex = PrisonerList.SelectedIndex;

            loadCellPrisonerVisits();
        }

        protected void VisitList_SelectedIndexChanged(object obj, EventArgs e)
        {
            selectedCellPrisonerVisitRowIndex = VisitList.SelectedIndex;
        }

        protected void VisitorList_SelectedIndexChanged(object obj, EventArgs e)
        {
            selectedVisitorRowIndex = VisitorList.SelectedIndex;
        }

        protected void AddCell_Click(object obj, EventArgs e)
        {
            Response.Redirect("~/AddCellWebForm.aspx");
        }

        protected void EditCell_Click(object obj, EventArgs e)
        {
            if (selectedCellRowIndex > -1)
            {
                Cell cell = Cells[selectedCellRowIndex];
                Response.Redirect("~/EditCellWebForm.aspx?cellId=" + cell.CellId);
            }
        }

        protected void AddPrisoner_Click(object obj, EventArgs e)
        {
            if (selectedCellRowIndex > -1)
            {
                Cell cell = Cells[selectedCellRowIndex];
                if (cell.Occupied < cell.Capacity)
                {
                    Response.Redirect("~/AddPrisonerWebForm.aspx?cellId=" + cell.CellId);
                }
            }
        }

        protected void EditPrisoner_Click(object obj, EventArgs e)
        {
            if (selectedCellPrisonerRowIndex > -1)
            {
                Prisoner prisoner = CellPrisoners[selectedCellPrisonerRowIndex];
                Response.Redirect("~/EditPrisonerWebForm.aspx?prisonerId=" + prisoner.PrisonerId);
            }
        }

        protected void ReleasePrisoner_Click(object obj, EventArgs e)
        {
            if (selectedCellPrisonerRowIndex > -1)
            {
                Prisoner prisoner = CellPrisoners[selectedCellPrisonerRowIndex];

                try
                {
                    prisonerLogic.Release(prisoner);

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Vězeň byl úspěšně propuštěn.')", true);

                    loadCells();
                }
                catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
            }
        }

        protected void AddVisit_Click(object obj, EventArgs e)
        {
            if (selectedCellPrisonerRowIndex > -1 && selectedVisitorRowIndex > -1)
            {
                Prisoner prisoner = CellPrisoners[selectedCellPrisonerRowIndex];
                Response.Redirect("~/AddVisitWebForm.aspx?prisonerId=" + prisoner.PrisonerId);
            }
        }

        protected void EditVisit_Click(object obj, EventArgs e)
        {
            if (selectedCellPrisonerVisitRowIndex > -1)
            {
                Visit visit = CellPrisonerVisits[selectedCellPrisonerVisitRowIndex];

                Response.Redirect("~/EditVisitWebForm.aspx?visitId=" + visit.VisitId);
            }
        }

        protected void AddVisitor_Click(object obj, EventArgs e)
        {
            Response.Redirect("~/AddVisitorWebForm.aspx");
        }

        protected void EditVisitor_Click(object obj, EventArgs e)
        {
            if (selectedVisitorRowIndex > -1)
            {
                Visitor visitor = Visitors[selectedVisitorRowIndex];

                Response.Redirect("~/EditVisitorWebForm.aspx?visitorId=" + visitor.VisitorId);
            }
        }

        protected void ForbidVisitor_Click(object obj, EventArgs e)
        {
            if (selectedVisitorRowIndex > -1)
            {
                Visitor visitor = Visitors[selectedVisitorRowIndex];

                try
                {
                    visitorLogic.Forbid(visitor);

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Návštěvník byl úspěšně zakázán.')", true);

                    loadVisitors();
                }
                catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
            }
        }
    }
}