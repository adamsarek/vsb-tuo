<%@ Page Title="Správa věznice" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuWebForm.aspx.cs" Inherits="ViewLayerWebForms.MenuWebForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Správa cel, vězňů a návštěv</h2>
    <div class="grid-gap v-grid grid">
        <div class="v-grid grid">
            <div class="h-grid grid"><asp:Button id="AddCellButton" onclick="AddCell_Click" Text="Přidat celu" runat="server"></asp:Button><asp:Button id="EditCellButton" onclick="EditCell_Click" Text="Upravit celu" runat="server"></asp:Button></div>
            <asp:ListBox class="grid-item" ID="CellList" runat="server" OnSelectedIndexChanged="CellList_SelectedIndexChanged" SelectionMode="Single" AutoPostBack="true" Rows="10"></asp:ListBox>
        </div>
        <div class="v-grid grid">
            <div class="h-grid grid"><asp:Button id="AddPrisonerButton" onclick="AddPrisoner_Click" Text="Přidat vězně" runat="server"></asp:Button><asp:Button id="EditPrisonerButton" onclick="EditPrisoner_Click" Text="Upravit vězně" runat="server"></asp:Button><asp:Button id="ReleasePrisonerButton" onclick="ReleasePrisoner_Click" Text="Propustit vězně" runat="server"></asp:Button></div>
            <asp:ListBox class="grid-item" ID="PrisonerList" runat="server" OnSelectedIndexChanged="PrisonerList_SelectedIndexChanged" SelectionMode="Single" AutoPostBack="true" Rows="10"></asp:ListBox>
        </div>
        <div class="v-grid grid">
            <div class="h-grid grid"><asp:Button id="AddVisitButton" onclick="AddVisit_Click" Text="Přidat návštěvu" runat="server"></asp:Button><asp:Button id="EditVisitButton" onclick="EditVisit_Click" Text="Upravit návštěvu" runat="server"></asp:Button></div>
            <asp:ListBox class="grid-item" ID="VisitList" runat="server" OnSelectedIndexChanged="VisitList_SelectedIndexChanged" SelectionMode="Single" AutoPostBack="true" Rows="10"></asp:ListBox>
        </div>
    </div>
    <h2>Správa návštěvníků</h2>
    <div class="grid-gap v-grid grid">
        <div class="v-grid grid">
            <div class="h-grid grid"><asp:Button id="AddVisitorButton" onclick="AddVisitor_Click" Text="Přidat návštěvníka" runat="server"></asp:Button><asp:Button id="EditVisitorButton" onclick="EditVisitor_Click" Text="Upravit návštěvníka" runat="server"></asp:Button><asp:Button id="ForbidVisitorButton" onclick="ForbidVisitor_Click" Text="Zakázat návštěvníka" runat="server"></asp:Button></div>
            <asp:ListBox class="grid-item" ID="VisitorList" runat="server" OnSelectedIndexChanged="VisitorList_SelectedIndexChanged" SelectionMode="Single" AutoPostBack="true" Rows="10"></asp:ListBox>
        </div>
    </div>
</asp:Content>
