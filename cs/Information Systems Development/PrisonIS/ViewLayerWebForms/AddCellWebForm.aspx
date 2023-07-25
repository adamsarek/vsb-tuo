<%@ Page Title="Přidat celu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCellWebForm.aspx.cs" Inherits="ViewLayerWebForms.AddCellWebForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Přidat celu</h2>
    <div class="center">
        <div class="grid-gap v-grid grid-item">
            <div class="grid-gap h-grid grid">
                <b class="grid-item">Kapacita</b>
                <input type="number" class="grid-item" id="CapacityInput" runat="server">
            </div>
            <div class="v-grid grid">
                <asp:Button id="AddCellButton" OnClick="AddCell_Click" Text="Přidat celu" runat="server"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>