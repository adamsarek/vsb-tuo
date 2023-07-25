<%@ Page Title="Přidat návštěvníka" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddVisitorWebForm.aspx.cs" Inherits="ViewLayerWebForms.AddVisitorWebForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Přidat návštěvníka</h2>
    <div class="center">
        <div class="grid-gap v-grid grid-item">
            <div class="grid-gap h-grid grid"><b class="grid-item">Jméno</b><input type="text" class="grid-item" id="FirstNameInput" runat="server"></div>
            <div class="grid-gap h-grid grid"><b class="grid-item">Příjmení</b><input type="text" class="grid-item" id="LastNameInput" runat="server"></div>
            <div class="grid-gap h-grid grid">
                <b class="grid-item">Pohlaví</b>
                <div class="grid-gap h-grid grid">
                    <div class="grid-gap h-grid grid"><input type="radio" class="grid-item" id="GenderMaleRadio" runat="server" checked><label>Muž</label></div>
                    <div class="grid-gap h-grid grid"><input type="radio" class="grid-item" id="GenderFemaleRadio" runat="server"><label>Žena</label></div>
                </div>
            </div>
            <div class="grid-gap h-grid grid"><b class="grid-item">Datum narození</b><input type="date" class="grid-item" id="BirthDateInput" runat="server"></div>
            <div class="v-grid grid">
                <asp:Button id="AddVisitorButton" OnClick="AddVisitor_Click" Text="Přidat návštěvníka" runat="server"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>
