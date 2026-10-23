<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaUsuarios.aspx.cs" Inherits="catalogo_web.ListaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Lista de Usuarios</h3>

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
        <asp:Button  ID="btnVolver" Text="Volver" CssClass="btn btn-secondary me-2" runat="server" OnClientClick="javascript:history.back(); return false;" />
    </div>

</asp:Content>
