<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="catalogo_web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
    .card-img-container {
        width: 100%;
        height: auto;         /* Cambio según qué tan alta quiero la foto */
        overflow: hidden;      
        background-color: #f0f0f0; 
    }

    .card-img-container img {
        width: 100% !important;
        height: 100% !important;
        display: block;
        object-fit: contain; /* Ajusta la imagen a su contenedor (ya no muestra solo una parte de la imagen grande) */
        object-position: center; /* Centra la imagen */ 
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Requerido para usar Update Panel --%>
    <asp:ScriptManager ID="ScripManager1" runat="server" />

    <h3>Registro</h3>

    <div class="row">

        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox ID="txtId" CssClass="form-control" Enabled="false" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                <asp:RequiredFieldValidator CssClass="validator"
                    ControlToValidate="txtNombre"
                    ErrorMessage="Debe ingresar un nombre."
                    ValidationGroup="articuloGroup"
                    runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" />
            </div>

            <asp:Button Text="Registrarse" ID="btnRegistrarse" CssClass="btn btn-primary" OnClick="btnRegistrarse_Click" runat="server" />
            <asp:HyperLink NavigateUrl="/" Text="Cancelar" CssClass="btn btn-danger" runat="server" />

        </div>

        <div class="col-6">
            <%-- Aca poner panel control para la imagen --%>
            <asp:UpdatePanel ID="upImagenFormulario" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtUrlImagen" class="form-label">Url Imagen</label>
                        <asp:TextBox ID="txtUrlImagen" CssClass="form-control" AutoPostBack="true" runat="server" />
                    </div>
                    <div class="mb-3 card-img-container">
                        <asp:Image ID="imgArticulo" ImageUrl="https://media.istockphoto.com/id/1128826884/es/vector/ning%C3%BAn-s%C3%ADmbolo-de-vector-de-imagen-falta-icono-disponible-no-hay-galer%C3%ADa-para-este-momento.jpg?s=612x612&w=0&k=20&c=9vnjI4XI3XQC0VHfuDePO7vNJE7WDM8uzQmZJ1SnQgk=" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>

</asp:Content>
