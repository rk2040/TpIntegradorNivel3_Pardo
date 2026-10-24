<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaUsuarios.aspx.cs" Inherits="catalogo_web.ListaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bg-body-tertiary text-white rounded-3 p-4 mb-4 shadow-sm border border-secondary">
        <div class="container-fluid py-2">
            <h2 class="fw-bold">Lista de Usuarios</h2>
        </div>
    </div>

    <div class="mb-3">
        <asp:GridView ID="dgvListaUsuarios" CssClass="table" runat="server"
            AutoGenerateColumns="false" DataKeyNames="Id"
            OnPageIndexChanging="dgvListaUsuarios_PageIndexChanging"
            AllowPaging="true" PageSize="10"
            OnRowCommand="dgvListaUsuarios_RowCommand">

            <Columns>
                <asp:BoundField HeaderText="Email" DataField="email" />
                <asp:BoundField HeaderText="Nombre" DataField="nombre" />
                <asp:BoundField HeaderText="Apellido" DataField="apellido" />
                <asp:BoundField HeaderText="Es Admin" DataField="admin" />
                <asp:TemplateField HeaderText="Cambiar Permiso de Admin">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnCambiarAdmin"
                            CssClass='<%#(bool)Eval("Admin") ? "btn btn-outline-danger btn-sm" : "btn btn-primary btn-sm" %>'
                            CommandName="CambiarAdmin"
                            CommandArgument='<%#Eval("Id") %>'
                            runat="server">
                            <%#(bool)Eval("Admin") ? "Quitar Admin" : "Nuevo Admin" %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HyperLink ID="hlkIrHome" Text="ir a Home" NavigateUrl="Default.aspx" CssClass="btn btn-primary me-2" runat="server" />
        <asp:HyperLink ID="hlkVolver" Text="Volver a Perfil" NavigateUrl="MiPerfil.aspx" CssClass="btn btn-secondary me-2" runat="server" />
    </div>

</asp:Content>
