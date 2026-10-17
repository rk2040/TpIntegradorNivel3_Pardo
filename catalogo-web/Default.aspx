<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="catalogo_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .card-img-container {
            width: 100%;
            height: 200px; /* Cambio según qué tan alta quiero la foto */
            object-fit: contain;
            background-color: #ffff;
        }

        .card-body {
            justify-items: normal;
        }

        /*.card-img-container img {
                width: 100% !important;
                height: 100% !important;
                justify-content: center;
                align-items: center;
                object-fit:cover;*/ /* Ajusta la imagen a su contenedor (ya no muestra solo una parte de la imagen grande) */
        /*object-position: center;*/ /* Centra la imagen */
        /*}*/
    </style>


    <h3>Soy Default</h3>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <%--<%foreach (dominio.Articulo articulo in ListaArticulos)
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
        <%} %>--%>

        <asp:Repeater ID="repRepetido" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <img src="<%#Eval("ImagenUrl") %>" class="card-img-container" alt="Imagen de: <%#Eval("Nombre") %>" />

                        <div class="card-body">
                            <h6 class="card-title"><%#Eval("CategoriaTipo") %></h6>
                            <h6 class="card-title"><%#Eval("MarcaTipo") %></h6>
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <h5 class="card-title">$ <%#Eval("Precio") %></h5>
                            <%--<p class="card-title"><%#Eval("Descripcion") %></p>--%>
                            <asp:Button ID="btnFavorito" Text="Favorito" CssClass="btn btn-primary" CommandArgument='<%#Eval("Id") %>' OnCommand="btnFavorito_Command" OnClick="btnFavorito_Click" runat="server" />
                        </div>


                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
