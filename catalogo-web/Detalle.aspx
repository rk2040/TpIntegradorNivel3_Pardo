<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="catalogo_web.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-img-container {
            width: 100%;
            max-width: 600px;
            height: 400px; /* Cambio según qué tan alta quiero la foto */
            overflow: hidden;
            background-color: #ffff;
        }

            .card-img-container img {
                width: 100% !important;
                height: 100% !important;
                display: block;
                object-fit: contain; /* Ajusta la imagen a su contenedor (ya no muestra solo una parte de la imagen grande) */
                object-position: center; /* Centra la imagen */
            }

        .validator {
            color: red;
            font-size: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Requerido para usar Update Panel --%>
    <asp:ScriptManager ID="ScripManager1" runat="server" />

    <h3>Detalle</h3>
    <div class="row">

        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox ID="txtId" CssClass="form-control" ReadOnly="true" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Codigo</label>
                <asp:TextBox ID="txtCodigo" CssClass="form-control" ReadOnly="true" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" ReadOnly="true" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion</label>
                <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" CssClass="form-control" ReadOnly="true" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" ReadOnly="true" runat="server" Enabled="false" ></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" ReadOnly="true" runat="server" Enabled="false" ></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio</label>
                <asp:TextBox ID="txtPrecio" CssClass="form-control" ReadOnly="true" runat="server" />
            </div>

            <div class="mb-3">
                <asp:LinkButton ID="btnVolver" Text="Volver" CssClass="btn btn-secondary"
                    OnClick="btnVolver_Click" runat="server" />
            </div>
        </div>

        <div class="col-6">
            <%-- Aca poner panel control para la imagen --%>
            <asp:UpdatePanel ID="upImagenFormulario" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtUrlImagen" class="form-label">Imagen</label>
                    </div>
                    <div class="card-img-container">
                        <asp:Image ID="imgArticulo" 
                            ImageUrl="https://media.istockphoto.com/id/1128826884/es/vector/ning%C3%BAn-s%C3%ADmbolo-de-vector-de-imagen-falta-icono-disponible-no-hay-galer%C3%ADa-para-este-momento.jpg?s=612x612&w=0&k=20&c=9vnjI4XI3XQC0VHfuDePO7vNJE7WDM8uzQmZJ1SnQgk="
                            runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>