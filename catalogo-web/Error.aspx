<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="catalogo_web.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Error</h3>
    <div>
        <asp:Label ID="lblError" Text="text" runat="server" />
    </div>
    <div>
        <asp:HyperLink Text="Ir a Home" NavigateUrl="Default.aspx" CssClass="btn btn-primary me-2" runat="server" />
        <asp:Button Text="Volver" CssClass="btn btn-secondary me-2" runat="server"
            OnClientClick="javascript:history.back(); return false;"/>
    </div>

</asp:Content>
