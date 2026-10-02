<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="catalogo_web.Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
    .card-img-container {
        width: 100%;
        height: 200px;         /* Cambio según qué tan alta quiero la foto */
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


    <h3>Soy Default</h3>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <%foreach (dominio.Articulo articulo in ListaArticulos)
            { %>
        <div class="col">
            <div class="card h-100">
                <div class="card-img-container">
                    <img src="<%: articulo.ImagenUrl %>" class="card-img-top" alt="Imagen de <%: articulo.Nombre %>">
                </div>
                <div class="card-body">
                    <h6 class="card-title"><%: articulo.MarcaTipo %></h6>
                    <h5 class="card-title"><%: articulo.Nombre %></h5>
                    <h6 class="card-title"><%: articulo.CategoriaTipo %></h6>
                    <h5 class="card-title">$ <%: articulo.Precio %></h5>
                    <p class="card-text"><%: articulo.Descripcion %></p>
                </div>
            </div>
        </div>
            <%} %>
    </div>
</asp:Content>
