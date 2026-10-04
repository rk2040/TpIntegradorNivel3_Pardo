<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ArticulosLista.aspx.cs" Inherits="catalogo_web.ArticulosLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />

    <h3>Lista de Articulos</h3>

    <div class="col-6">
        <div class="mb-3">
            <asp:Label Text="Buscar" runat="server" />
            <asp:TextBox ID="txtFiltro" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control" runat="server" />
        </div>
        <div class="col-6" style="display:flex; flex-direction: column; justify-content:flex-end">
            <div class="mb-3">
                <asp:CheckBox id="chkAvanzado" Text="Busqueda Avanzada" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" CssClass="" runat="server" />
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
                    <asp:TextBox ID="txtFiltroAvanzado" CssClass="form-control" runat="server" />
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
        <asp:GridView ID="dgvArticulos" CssClass="table" AutoGenerateColumns="false" DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" OnPageIndexChanging="dgvArticulos_PageIndexChanging" AllowPaging="true" PageSize="3" runat="server">

            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                <asp:BoundField HeaderText="Marca" DataField="MarcaTipo" />
                <asp:BoundField HeaderText="Categoria" DataField="CategoriaTipo" />
                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                <asp:CommandField HeaderText="Ver" ShowSelectButton="true" SelectText="Ver" ControlStyle-CssClass="btn btn-success " />
             </Columns>
        </asp:GridView>
        <asp:HyperLink NavigateUrl="FormularioArticulo.aspx" Text="Agregar" CssClass="btn btn-primary" runat="server" />
    </div>

</asp:Content>
