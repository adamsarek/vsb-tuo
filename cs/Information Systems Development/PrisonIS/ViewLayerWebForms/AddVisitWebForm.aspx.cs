using Common.Class;
using DomainLayer;
using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class AddVisitWebForm : System.Web.UI.Page
    {
        private PrisonerLogic prisonerLogic = new PrisonerLogic();
        private VisitLogic visitLogic = new VisitLogic();
        private VisitorLogic visitorLogic = new VisitorLogic();

        private static Collection<Visitor> Visitors = new Collection<Visitor>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                VisitDateInput.Attributes.Add("value", DateTime.Today.ToString("yyyy-MM-dd"));

                try
                {
                    Visitors = visitorLogic.SelectAllowed();

                    VisitorSelect.DataSource = Visitors;
                    VisitorSelect.DataBind();
                    if (Visitors.Count > 0) { VisitorSelect.SelectedIndex = 0; }
                }
                catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
            }
        }

        protected void AddVisit_Click(object obj, EventArgs e)
        {
            Visit visit = new Visit()
            {
                VisitId = 0,
                VisitDate = DateTime.Parse(VisitDateInput.Value),
                Allowed = char.Parse(AllowedSelect.Value),
                Prisoner = prisonerLogic.Select(int.Parse(Request.QueryString["prisonerId"])),
                Visitor = Visitors[VisitorSelect.SelectedIndex]
            };

            try
            {
                visitLogic.Insert(visit);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Návštěva byla úspěšně přidána.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}