<%@ Page Title="Upravit návštěvu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditVisitWebForm.aspx.cs" Inherits="ViewLayerWebForms.EditVisitWebForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Upravit návštěvu</h2>
    <div class="center">
        <div class="grid-gap v-grid grid-item">
            <div class="grid-gap h-grid grid"><b class="grid-item">Datum návštěvy</b><input type="date" class="grid-item" id="VisitDateInput" runat="server"></div>
            <div class="grid-gap h-grid grid">
                <b class="grid-item">Povolení</b>
                <select class="grid-item" id="AllowedSelect" runat="server">
                    <option value="0">Nepovolená</option>
                    <option value="1">Povolená</option>
                    <option value="2">Nerozhodnutá</option>
                </select>
            </div>
            <div class="grid-gap h-grid grid"><b class="grid-item">Návštěvník</b><label class="grid-item" id="VisitorLabel" runat="server"></label></div>
            <div class="v-grid grid">
                <asp:Button id="EditVisitButton" OnClick="EditVisit_Click" Text="Upravit návštěvu" runat="server"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>
