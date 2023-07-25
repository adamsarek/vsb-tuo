<%@ Page Title="Přidat návštěvu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddVisitWebForm.aspx.cs" Inherits="ViewLayerWebForms.AddVisitWebForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Přidat návštěvu</h2>
    <div class="center">
        <div class="grid-gap v-grid grid-item">
            <div class="grid-gap h-grid grid"><b class="grid-item">Datum návštěvy</b><input type="date" class="grid-item" id="VisitDateInput" runat="server"></div>
            <div class="grid-gap h-grid grid">
                <b class="grid-item">Povolení</b>
                <select class="grid-item" id="AllowedSelect" runat="server">
                    <option value="1">Povolená</option>
                    <option value="2" selected>Nerozhodnutá</option>
                </select>
            </div>
            <div class="grid-gap h-grid grid"><b class="grid-item">Návštěvník</b><asp:DropDownList class="grid-item" id="VisitorSelect" runat="server"></asp:DropDownList></div>
            <div class="v-grid grid">
                <asp:Button id="AddVisitButton" OnClick="AddVisit_Click" Text="Přidat návštěvu" runat="server"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>
