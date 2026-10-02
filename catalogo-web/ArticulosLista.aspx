<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ArticulosLista.aspx.cs" Inherits="catalogo_web.ArticulosLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Lista de Articulos</h3>

    <div class="mb-3">
        <asp:GridView ID="dgvArticulos" CssClass="table" AutoGenerateColumns="false" DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" OnPageIndexChanging="dgvArticulos_PageIndexChanging" AllowPaging="true" PageSize="5" runat="server">

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
