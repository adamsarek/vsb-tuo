using Common.Class;
using DomainLayer;
using System;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class AddVisitorWebForm : System.Web.UI.Page
    {
        private VisitorLogic visitorLogic = new VisitorLogic();

        private int minVisitorAge = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime birthDateMax = DateTime.Today.AddYears(-minVisitorAge).AddDays(-1);

                BirthDateInput.Attributes.Add("value", birthDateMax.ToString("yyyy-MM-dd"));
                BirthDateInput.Attributes.Add("max", birthDateMax.ToString("yyyy-MM-dd"));
            }
        }

        protected void AddVisitor_Click(object obj, EventArgs e)
        {
            Visitor visitor = new Visitor()
            {
                VisitorId = 0,
                FirstName = FirstNameInput.Value,
                LastName = LastNameInput.Value,
                Gender = (GenderMaleRadio.Checked ? 'M' : 'F'),
                BirthDate = DateTime.Parse(BirthDateInput.Value),
                Forbidden = '0'
            };

            try
            {
                visitorLogic.Insert(visitor);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Návštěvník byl úspěšně přidán.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}