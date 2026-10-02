<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="catalogo_web.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Requerido para usar Update Panel --%>
    <%--<asp:ScriptManager ID="ScripManager1" runat="server" />--%>

    <h3>Formulario de Articulo</h3>
    <div class="row">

        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox ID="txtId" CssClass="form-control " runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Codigo</label>
                <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Id</label>
                <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server" />
            </div>

            <div class="mb-3">
                <asp:Button ID="btnAceptar" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" runat="server" />
                <asp:HyperLink NavigateUrl="ArticulosLista.aspx" Text="Cancelar" CssClass="btn btn-secondary" runat="server" />
                <asp:Button ID="btnEliminar" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" runat="server" />
            </div>
        </div>

        <div class="col-6">
            <div class="mb-3">
                <label for="txtUrlImagen" class="form-label">Url Imagen</label>
                <asp:TextBox ID="txtUrlImagen" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUrlImagen_TextChanged" runat="server" />
            </div>
            <%-- Aca panel control para la imagen --%>
            <asp:Image ID="imgArticulo" ImageUrl="https://media.istockphoto.com/id/1128826884/es/vector/ning%C3%BAn-s%C3%ADmbolo-de-vector-de-imagen-falta-icono-disponible-no-hay-galer%C3%ADa-para-este-momento.jpg?s=612x612&w=0&k=20&c=9vnjI4XI3XQC0VHfuDePO7vNJE7WDM8uzQmZJ1SnQgk=" runat="server" />


        </div>

    </div>

</asp:Content>
