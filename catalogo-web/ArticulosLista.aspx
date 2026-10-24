<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ArticulosLista.aspx.cs" Inherits="catalogo_web.ArticulosLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />

    <div class="bg-body-tertiary text-white rounded-3 p-4 mb-4 shadow-sm border border-secondary">
        <div class="container-fluid py-2">
            <h2 class="fw-bold">Lista de Articulos</h2>
        </div>
    </div>

    <div class="col-6">
        <div class="mb-3">
            <asp:Label Text="Buscar" runat="server" />
            <asp:TextBox ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control" runat="server" />
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end">
            <div class="mb-3">
                <asp:CheckBox ID="chkAvanzado" Text="Busqueda Avanzada" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" CssClass="" runat="server" />
            </div>
        </div>

        <%if (FiltroAvanzado)
            {%>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Campo" runat="server" />
                    <asp:DropDownList ID="ddlCampo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" runat="server">
                        <asp:ListItem Text="Codigo" />
                        <asp:ListItem Text="Nombre" />
                        <asp:ListItem Text="Marca" />
                        <asp:ListItem Text="Categoria" />
                        <asp:ListItem Text="Precio" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Criterio" runat="server" />
                    <asp:DropDownList ID="ddlCriterio" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div-mb-3>
                    <asp:Label Text="Buscar" runat="server" />
                    <asp:TextBox ID="txtFiltroAvanzado" AutoPostBack="true" CssClass="form-control" runat="server" />
                </div-mb-3>
            </div>
        </div>
        <div class="row">
            <div class="col 3">
                <div class="mb-3">
                    <asp:Button ID="btnBuscar" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" runat="server" />
                </div>
            </div>
        </div>
        <%} %>
    </div>

    <div class="mb-3">
        <asp:GridView ID="dgvArticulos" CssClass="table" AutoGenerateColumns="false" DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
            OnRowCommand="dgvArticulos_RowCommand"
            OnPageIndexChanging="dgvArticulos_PageIndexChanging"
            AllowPaging="true" PageSize="5" runat="server">

            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                <asp:BoundField HeaderText="Marca" DataField="MarcaTipo" />
                <asp:BoundField HeaderText="Categoria" DataField="CategoriaTipo" />
                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                <asp:TemplateField HeaderText="Accion">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnFavorito"
                            CssClass='<%#(bool)Eval("EsFavorito") ? "btn btn-primary btn-sm" : "btn btn-outline-danger btn-sm" %>'
                            CommandName="Favorito"
                            CommandArgument='<%#Eval("Id") %>'
                            runat="server">
                            <%#(bool)Eval("EsFavorito") ? "❤ Quitar de Favoritos" : "🤍 Agregar a Favoritos" %>
                        </asp:LinkButton>
                        <asp:HyperLink Text="Ver Detalle"
                            NavigateUrl='<%# "Detalle.aspx?id=" + Eval("Id") + "&origen=grid" %>'
                            class="btn btn-success btn-ms"
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnVer"
                            Text="Modificar/Eliminar"
                            CssClass="btn btn-danger "
                            CommandName="Select"
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField  HeaderText="Ver" ShowSelectButton="true" SelectText="Ver" ControlStyle-CssClass="btn btn-success " />--%>
            </Columns>
        </asp:GridView>
        <asp:HyperLink NavigateUrl="FormularioArticulo.aspx" ID="btnAgregar" Text="Agregar" CssClass="btn btn-primary" runat="server" />
    </div>

</asp:Content>
