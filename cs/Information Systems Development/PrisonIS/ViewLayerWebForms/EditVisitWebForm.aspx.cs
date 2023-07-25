using Common.Class;
using DomainLayer;
using System;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class EditVisitWebForm : System.Web.UI.Page
    {
        private PrisonerLogic prisonerLogic = new PrisonerLogic();
        private VisitLogic visitLogic = new VisitLogic();
        private VisitorLogic visitorLogic = new VisitorLogic();

        private static Visit visit = new Visit();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                visit = visitLogic.Select(int.Parse(Request.QueryString["visitId"]));

                VisitDateInput.Attributes.Add("value", DateTime.Today.ToString("yyyy-MM-dd"));
                AllowedSelect.SelectedIndex = int.Parse(visit.Allowed.ToString());
                VisitorLabel.InnerText = visit.Visitor.ToString();
            }
        }

        protected void EditVisit_Click(object obj, EventArgs e)
        {
            visit = new Visit()
            {
                VisitId = visit.VisitId,
                VisitDate = DateTime.Parse(VisitDateInput.Value),
                Allowed = char.Parse(AllowedSelect.Value),
                Prisoner = visit.Prisoner,
                Visitor = visit.Visitor
            };

            try
            {
                visitLogic.Update(visit);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Návštěva byla úspěšně upravena.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}