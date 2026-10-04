<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="catalogo_web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <diw class="row">
        <div class="col-6">
            <h3>Login</h3>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" Required="true" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label" >Password</label>
                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Button ID="btnIngresar" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" runat="server" />
                <asp:HyperLink NavigateUrl="Default.aspx" Text="Cancelar" CssClass="btn btn-secondary" runat="server" />
            </div>
        </div>
    </diw>

</asp:Content>
