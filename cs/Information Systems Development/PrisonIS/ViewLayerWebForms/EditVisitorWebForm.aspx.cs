using Common.Class;
using DomainLayer;
using System;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class EditVisitorWebForm : System.Web.UI.Page
    {
        private VisitorLogic visitorLogic = new VisitorLogic();

        private int minVisitorAge = 0;

        private static Visitor visitor;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                visitor = visitorLogic.Select(int.Parse(Request.QueryString["visitorId"]));

                FirstNameInput.Attributes.Add("value", visitor.FirstName);
                LastNameInput.Attributes.Add("value", visitor.LastName);
                if (visitor.Gender == 'M') { GenderMaleRadio.Checked = true; } else { GenderFemaleRadio.Checked = true; }
                DateTime birthDateMax = DateTime.Today.AddYears(-minVisitorAge).AddDays(-1);
                BirthDateInput.Attributes.Add("value", visitor.BirthDate.ToString("yyyy-MM-dd"));
                BirthDateInput.Attributes.Add("max", birthDateMax.ToString("yyyy-MM-dd"));
            }
        }

        protected void EditVisitor_Click(object obj, EventArgs e)
        {
            visitor = new Visitor()
            {
                VisitorId = visitor.VisitorId,
                FirstName = FirstNameInput.Value,
                LastName = LastNameInput.Value,
                Gender = (GenderMaleRadio.Checked ? 'M' : 'F'),
                BirthDate = DateTime.Parse(BirthDateInput.Value),
                Forbidden = visitor.Forbidden
            };

            try
            {
                visitorLogic.Update(visitor);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Návštěvník byl úspěšně upraven.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}