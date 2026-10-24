<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="catalogo_web.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bg-body-tertiary text-white rounded-3 p-4 mb-4 shadow-sm border border-secondary">
        <div class="container-fluid py-2">
            <h2 class="fw-bold">Favoritos</h2>
        </div>
    </div>

    <div class="mb-3">
        <asp:GridView ID="dgvFavoritos" CssClass="table" AutoGenerateColumns="false" DataKeyNames="Id"
            OnPageIndexChanging="dgvFavoritos_PageIndexChanging" AllowPaging="true" PageSize="10" runat="server">

            <Columns>
                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                <asp:BoundField HeaderText="Marca" DataField="MarcaTipo" />
                <asp:BoundField HeaderText="Categoria" DataField="CategoriaTipo" />
                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                <asp:TemplateField HeaderText="Accion">
                    <ItemTemplate>
                        <asp:HyperLink ID="btnVer" NavigateUrl='<%# "Detalle.aspx?id=" + Eval("Id") + "&origen=favorito"  %>'
                            Text="Ver Detalle" CssClass="btn btn-success "
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField  HeaderText="Ver" ShowSelectButton="true" SelectText="Ver" ControlStyle-CssClass="btn btn-success " />--%>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnVolver" Text="Volver" CssClass="btn btn-secondary me-2" runat="server" OnClientClick="javascript:history.back(); return false;" />
    </div>

</asp:Content>
