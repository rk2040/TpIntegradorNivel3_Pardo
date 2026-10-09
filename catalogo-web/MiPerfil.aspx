<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="catalogo_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-img-container {
            width: 100%;
            height: auto; /* Cambio según qué tan alta quiero la foto */
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

    <h3>Mi Perfil</h3>

    <%-- Requerido para usar Update Panel --%>
    <asp:ScriptManager ID="ScripManager1" runat="server" />

    <div class="row">

        <div class="col-md-6">
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
            <div class="col-md-6">
                <asp:Button Text="Guardar" ID="btnGuardae" CssClass="btn btn-primary m-1" OnClick="btnGuardae_Click" runat="server" />
                <asp:HyperLink NavigateUrl="/" Text="Cancelar" CssClass="btn btn-danger m-1" runat="server" />
            </div>
        </div>

        <div class="col-md-6">
            <%-- Aca poner panel control para la imagen --%>
            <asp:UpdatePanel ID="upImagenFormulario" runat="server">
                <ContentTemplate>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label class="form-label">Imagen Perfil</label>
                            <input type="file" id="txtImagenPerfil" class="form-control" runat="server" />
                        </div>
                    </div>
                    <div class="mb-3 card-img-container">
                        <asp:Image ID="imgPerfil" CssClass="img-fluid mb-3" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>



</asp:Content>
