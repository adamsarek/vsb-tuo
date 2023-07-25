using Common.Class;
using DomainLayer;
using System;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class AddPrisonerWebForm : System.Web.UI.Page
    {
        private CellLogic cellLogic = new CellLogic();
        private PrisonerLogic prisonerLogic = new PrisonerLogic();

        private int minPrisonerAge = 18;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime birthDateMax = DateTime.Today.AddYears(-minPrisonerAge).AddDays(-1);

                BirthDateInput.Attributes.Add("value", birthDateMax.ToString("yyyy-MM-dd"));
                BirthDateInput.Attributes.Add("max", birthDateMax.ToString("yyyy-MM-dd"));

                PunishmentStartDateInput.Attributes.Add("value", birthDateMax.AddYears(minPrisonerAge).ToString("yyyy-MM-dd"));
                PunishmentStartDateInput.Attributes.Add("max", birthDateMax.AddYears(minPrisonerAge).ToString("yyyy-MM-dd"));

                PunishmentEndDateInput.Attributes.Add("value", birthDateMax.AddYears(minPrisonerAge).AddDays(1).ToString("yyyy-MM-dd"));
                PunishmentEndDateInput.Attributes.Add("min", birthDateMax.AddYears(minPrisonerAge).AddDays(1).ToString("yyyy-MM-dd"));
            }
        }

        protected void AddPrisoner_Click(object obj, EventArgs e)
        {
            Prisoner prisoner = new Prisoner()
            {
                PrisonerId = 0,
                FirstName = FirstNameInput.Value,
                LastName = LastNameInput.Value,
                Gender = (GenderMaleRadio.Checked ? 'M' : 'F'),
                BirthDate = DateTime.Parse(BirthDateInput.Value),
                PunishmentStartDate = DateTime.Parse(PunishmentStartDateInput.Value),
                PunishmentEndDate = DateTime.Parse(PunishmentEndDateInput.Value),
                Released = '0',
                Cell = cellLogic.Select(int.Parse(Request.QueryString["cellId"]))
            };

            try
            {
                prisonerLogic.Insert(prisoner);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Vězeň byl úspěšně přidán.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}