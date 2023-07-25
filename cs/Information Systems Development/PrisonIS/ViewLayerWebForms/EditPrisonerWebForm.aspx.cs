using Common.Class;
using DomainLayer;
using System;
using System.Web.UI;

namespace ViewLayerWebForms
{
    public partial class EditPrisonerWebForm : System.Web.UI.Page
    {
        private PrisonerLogic prisonerLogic = new PrisonerLogic();

        private int minPrisonerAge = 18;

        private static Prisoner prisoner;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                prisoner = prisonerLogic.Select(int.Parse(Request.QueryString["prisonerId"]));

                FirstNameInput.Attributes.Add("value", prisoner.FirstName);
                LastNameInput.Attributes.Add("value", prisoner.LastName);
                if (prisoner.Gender == 'M') { GenderMaleRadio.Checked = true; } else { GenderFemaleRadio.Checked = true; }
                DateTime birthDateMax = DateTime.Today.AddYears(-minPrisonerAge).AddDays(-1);
                BirthDateInput.Attributes.Add("value", prisoner.BirthDate.ToString("yyyy-MM-dd"));
                BirthDateInput.Attributes.Add("max", birthDateMax.ToString("yyyy-MM-dd"));
                PunishmentStartDateInput.Attributes.Add("value", prisoner.PunishmentStartDate.ToString("yyyy-MM-dd"));
                PunishmentStartDateInput.Attributes.Add("max", birthDateMax.AddYears(minPrisonerAge).ToString("yyyy-MM-dd"));
                PunishmentEndDateInput.Attributes.Add("value", prisoner.PunishmentEndDate.ToString("yyyy-MM-dd"));
                PunishmentEndDateInput.Attributes.Add("min", birthDateMax.AddYears(minPrisonerAge).AddDays(1).ToString("yyyy-MM-dd"));
            }
        }

        protected void EditPrisoner_Click(object obj, EventArgs e)
        {
            prisoner = new Prisoner()
            {
                PrisonerId = prisoner.PrisonerId,
                FirstName = FirstNameInput.Value,
                LastName = LastNameInput.Value,
                Gender = (GenderMaleRadio.Checked ? 'M' : 'F'),
                BirthDate = DateTime.Parse(BirthDateInput.Value),
                PunishmentStartDate = DateTime.Parse(PunishmentStartDateInput.Value),
                PunishmentEndDate = DateTime.Parse(PunishmentEndDateInput.Value),
                Released = prisoner.Released,
                Cell = prisoner.Cell
            };

            try
            {
                prisonerLogic.Update(prisoner);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('Vězeň byl úspěšně upraven.');window.location.href='MenuWebForm.aspx'", true);
            }
            catch (Exception exception) { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" + exception.Message + "')", true); }
        }
    }
}