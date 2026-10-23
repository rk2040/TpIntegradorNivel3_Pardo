<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="catalogo_web.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Favoritos</h3>

    <div class="mb-3">
        <asp:GridView ID="dgvFavoritos" CssClass="table" AutoGenerateColumns="false" DataKeyNames="Id" OnSelectedIndexChanged="dgvFavoritos_SelectedIndexChanged" OnPageIndexChanging="dgvFavoritos_PageIndexChanging" AllowPaging="true" PageSize="10" runat="server">

            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                <asp:BoundField HeaderText="Marca" DataField="MarcaTipo" />
                <asp:BoundField HeaderText="Categoria" DataField="CategoriaTipo" />
                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                <asp:TemplateField HeaderText="Accion">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnVer" Text="Ver" CssClass="btn btn-success " CommandName="Select" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField  HeaderText="Ver" ShowSelectButton="true" SelectText="Ver" ControlStyle-CssClass="btn btn-success " />--%>
            </Columns>
        </asp:GridView>
        <asp:HyperLink Text="Volver" CssClass="btn btn-primary" NavigateUrl="Default.aspx" runat="server" />
    </div>

</asp:Content>
